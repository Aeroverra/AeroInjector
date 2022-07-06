using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Injection
{
    [Flags]
    public enum AllocationType : uint
    {
        Commit = 0x1000u,
        Reserve = 0x2000u,
        Decommit = 0x4000u,
        Release = 0x8000u,
        Reset = 0x80000u,
        Physical = 0x400000u,
        TopDown = 0x100000u,
        WriteWatch = 0x200000u,
        LargePages = 0x20000000u
    }
}
