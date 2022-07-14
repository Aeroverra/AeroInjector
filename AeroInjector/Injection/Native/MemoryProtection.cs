using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Injection.Native
{
    [Flags]
    public enum MemoryProtection : uint
    {
        Execute = 0x10u,
        ExecuteRead = 0x20u,
        ExecuteReadWrite = 0x40u,
        ExecuteWriteCopy = 0x80u,
        NoAccess = 1u,
        ReadOnly = 2u,
        ReadWrite = 4u,
        WriteCopy = 8u,
        GuardModifierflag = 0x100u,
        NoCacheModifierflag = 0x200u,
        WriteCombineModifierflag = 0x400u
    }
}
