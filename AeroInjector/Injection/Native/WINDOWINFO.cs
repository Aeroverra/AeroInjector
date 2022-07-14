using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Injection.Native
{
    public struct WINDOWINFO
    {
        public uint cbSize;

        public RECT rcWindow;

        public RECT rcClient;

        public uint dwStyle;

        public uint dwExStyle;

        public uint dwWindowStatus;

        public uint cxWindowBorders;

        public uint cyWindowBorders;

        public ushort atomWindowType;

        public ushort wCreatorVersion;
    }
}
