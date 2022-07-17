using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
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
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static AssemblyFramework GetFramework(string dllPathNameToInject)
        {
            // finally fixed this not unloading the file. Simply using a context and filestream works.
            var fileStream = File.OpenRead(dllPathNameToInject);
            var loaderContext = new AssemblyLoadContext(null, true);
            WeakReference wr = new WeakReference(loaderContext, trackResurrection: true);
            try
            {
                var assembly = loaderContext.LoadFromStream(fileStream);

                var tar = (TargetFrameworkAttribute)assembly
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
            finally
            {
                loaderContext.Unload();
                fileStream.Dispose();
            }

            return AssemblyFramework.Native;
        }
    }
}
