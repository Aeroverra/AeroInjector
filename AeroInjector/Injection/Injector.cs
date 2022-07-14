using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Injection.Native;

namespace Tech.Aerove.AeroInjector.Injection
{
    public class Injector
    {
        public readonly int ProcessId;
        public readonly string DllInjecteePath;
        public readonly string? ManagedNamespace;
        public readonly string? ManagedMethod;
        public readonly AssemblyFramework AssemblyFramework;
        public Injector(int processId, string dllInjecteePath, string? managedNamespace = null, string? managedMethod = null)
        {
            ProcessId = processId;
            DllInjecteePath = dllInjecteePath;
            ManagedNamespace = managedNamespace;
            ManagedMethod = managedMethod;
            AssemblyFramework = DLLUtils.GetFramework(DllInjecteePath);
        }


        public bool Inject(string? args = null)
        {
            if (AssemblyFramework == AssemblyFramework.Native)
            {
                return InjectDLL(DllInjecteePath);
            }

            //copy InjecteeCPP to temp folder
            var tempDir = FileUtils.GetTempDirectory();
            var injecteeCPP = FileUtils.CopyInjecteeCPP(tempDir);
            FileUtils.WriteManagedArgs(tempDir, AssemblyFramework, DllInjecteePath, ManagedNamespace, ManagedMethod, args);
            if (AssemblyFramework == AssemblyFramework.NetCore)
            {
                FileUtils.CopyNetCore(tempDir);
            }
           return InjectDLL(injecteeCPP);
        }
        private bool InjectDLL(string dllInjecteePath)
        {

            uint dwSize = (uint)((dllInjecteePath.Length + 1) * Marshal.SizeOf(typeof(char)));

            //open host process
            var flags = ProcessAccessFlags.CreateThread | ProcessAccessFlags.VirtualMemoryOperation | ProcessAccessFlags.VirtualMemoryRead | ProcessAccessFlags.VirtualMemoryWrite | ProcessAccessFlags.QueryInformation;
            IntPtr intPtr = Win32Calls.OpenProcess(flags, bInheritHandle: false, (uint)ProcessId);
            if (intPtr == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }

            //getting address of LoadLibraryA function
            IntPtr procAddress = Win32Calls.GetProcAddress(Win32Calls.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            if (procAddress == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }

            //Allocating memory in the host process for the injectee filename
            IntPtr intPtr2 = Win32Calls.VirtualAllocEx(intPtr, IntPtr.Zero, dwSize, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ReadWrite);
            if (intPtr2 == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }
            Thread.Sleep(500);

            //Writing dll filename into memory allocated in host process
            byte[] bytes = Encoding.Default.GetBytes(dllInjecteePath);
            if (!Win32Calls.WriteProcessMemory(intPtr, intPtr2, bytes, dwSize, out var lpNumberOfBytesWritten) || lpNumberOfBytesWritten.ToInt32() != bytes.Length + 1)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }

            //Create a thread in the host process to load the injectee
            if (Win32Calls.CreateRemoteThread(intPtr, IntPtr.Zero, 0u, procAddress, intPtr2, 0u, IntPtr.Zero) == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }

            if (!Win32Calls.CloseHandle(intPtr))
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }
            return true;

        }
    }
}
