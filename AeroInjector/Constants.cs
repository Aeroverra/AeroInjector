using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector
{
    public static class Constants
    {
        public static readonly string AppName = "AeroInjector";
        public static readonly DirectoryInfo DataDirectory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName));
        public static readonly DirectoryInfo TempDirectory = new DirectoryInfo(Path.Combine(DataDirectory.FullName,"Temp"));
    }
}
