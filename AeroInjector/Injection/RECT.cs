using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Injection
{
    public struct RECT
    {
        public int left;

        public int top;

        public int right;

        public int bottom;

        public int Width => right - left;

        public int Height => bottom - top;

        public static void CopyRect(RECT rcSrc, ref RECT rcDest)
        {
            rcDest.left = rcSrc.left;
            rcDest.top = rcSrc.top;
            rcDest.right = rcSrc.right;
            rcDest.bottom = rcSrc.bottom;
        }
    }
}
