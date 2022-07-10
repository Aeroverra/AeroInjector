using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Injection
{
    [Flags]
    public enum FreeType : uint
    {
        Decommit = 0x4000u,
        Release = 0x8000u
    }
}
