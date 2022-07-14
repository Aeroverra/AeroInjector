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
        public readonly AssemblyFramework AssemblyFramework;
        public Injector(int processId, string dllInjecteePath)
        {
            ProcessId = processId;
            DllInjecteePath = dllInjecteePath;
            AssemblyFramework = DLLUtils.GetFramework(DllInjecteePath);
        }

        public bool Attach()
        {
            return true;
        }
        public bool Inject()
        {
            DLLUtils.GetFramework(DllInjecteePath);
            uint dwSize = (uint)((DllInjecteePath.Length + 1) * Marshal.SizeOf(typeof(char)));

            IntPtr intPtr = Win32Calls.OpenProcess(ProcessAccessFlags.CreateThread | ProcessAccessFlags.VirtualMemoryOperation | ProcessAccessFlags.VirtualMemoryRead | ProcessAccessFlags.VirtualMemoryWrite | ProcessAccessFlags.QueryInformation, bInheritHandle: false, (uint)ProcessId);
            if (intPtr == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }
            IntPtr procAddress = Win32Calls.GetProcAddress(Win32Calls.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            if (procAddress == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }
            IntPtr intPtr2 = Win32Calls.VirtualAllocEx(intPtr, IntPtr.Zero, dwSize, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ReadWrite);
            if (intPtr2 == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }
            Thread.Sleep(500);
            byte[] bytes = Encoding.Default.GetBytes(DllInjecteePath);
            if (!Win32Calls.WriteProcessMemory(intPtr, intPtr2, bytes, dwSize, out var lpNumberOfBytesWritten) || lpNumberOfBytesWritten.ToInt32() != bytes.Length + 1)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return false;
            }
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
