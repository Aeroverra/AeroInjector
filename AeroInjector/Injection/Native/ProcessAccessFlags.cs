using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Injection.Native
{
    public enum ProcessAccessFlags : uint
    {
        All = 0x1F0FFFu,
        Terminate = 1u,
        CreateThread = 2u,
        VirtualMemoryOperation = 8u,
        VirtualMemoryRead = 0x10u,
        VirtualMemoryWrite = 0x20u,
        DuplicateHandle = 0x40u,
        CreateProcess = 0x80u,
        SetQuota = 0x100u,
        SetInformation = 0x200u,
        QueryInformation = 0x400u,
        QueryLimitedInformation = 0x1000u,
        Synchronize = 0x100000u
    }
}
