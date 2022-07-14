using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Injection
{
    internal static class DLLUtils
    {
        /// <summary>
        /// Detects the Assembly framework of a dll
        /// </summary>
        /// <param name="dllPathNameToInject"></param>
        /// <returns></returns>
        public static AssemblyFramework GetFramework(string dllPathNameToInject)
        {
            try
            {
                var tar = (TargetFrameworkAttribute)Assembly
                    .LoadFrom(dllPathNameToInject)
                    .GetCustomAttributes(typeof(TargetFrameworkAttribute))
                    .First();
                if (tar.FrameworkName.ToLower().Contains("netcore"))
                {
                    return AssemblyFramework.NetCore;
                }
                if (tar.FrameworkName.ToLower().Contains("netframework"))
                {
                    return AssemblyFramework.NetFramework;
                }
            }
            catch
            {

            }
            return AssemblyFramework.Native;
        }
    }
}
