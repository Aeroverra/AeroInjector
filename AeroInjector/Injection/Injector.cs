using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Injection
{
    public static class Injector
    {
        public static bool Inject(int processId, string dllPathNameToInject)
        {
            uint dwSize = (uint)((dllPathNameToInject.Length + 1) * Marshal.SizeOf(typeof(char)));

            IntPtr intPtr = Win32Calls.OpenProcess(ProcessAccessFlags.CreateThread | ProcessAccessFlags.VirtualMemoryOperation | ProcessAccessFlags.VirtualMemoryRead | ProcessAccessFlags.VirtualMemoryWrite | ProcessAccessFlags.QueryInformation, bInheritHandle: false, (uint)processId);
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
            byte[] bytes = Encoding.Default.GetBytes(dllPathNameToInject);
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
