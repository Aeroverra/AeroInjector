using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Injection;

namespace Tech.Aerove.AeroInjector
{
    internal static class FileUtils
    {

        /// <summary>
        /// Gets a temp directory within the data directory to be used for injection
        /// </summary>
        /// <returns></returns>
        public static DirectoryInfo GetTempDirectory()
        {
            var tempFolder = new DirectoryInfo(Path.Combine(Constants.TempDirectory.FullName, $"{Guid.NewGuid()}"));
            tempFolder.Create();
            return tempFolder;
        }

        /// <summary>
        /// Copies InjecteeCPP.dll to a directory
        /// </summary>
        /// <param name="destination"></param>
        public static string CopyInjecteeCPP(DirectoryInfo destination)
        {
            var assemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
            var injecteeCPP = Path.Combine(assemblyLocation.FullName, "InjecteeCPP.dll");
            var injecteeCPPDest = Path.Combine(destination.FullName, "InjecteeCPP.dll");
            File.Copy(injecteeCPP, injecteeCPPDest);

            var injecteeCPPPDB = Path.Combine(assemblyLocation.FullName, "InjecteeCPP.pdb");
            var injecteeCPPDestPDB = Path.Combine(destination.FullName, "InjecteeCPP.pdb");
            File.Copy(injecteeCPPPDB, injecteeCPPDestPDB);

            return injecteeCPPDest;
        }

        /// <summary>
        /// Copies NetCore Runtime to a directory
        /// </summary>
        /// <param name="destination"></param>
        public static void CopyNetCore(DirectoryInfo destination)
        {
            var assemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
            var zipSource = Path.Combine(assemblyLocation.FullName, "NetCore6.zip");
            //var zipDest = Path.Combine(destination.FullName, "NetCore6.dll");
            //File.Copy(zipSource, zipDest);
            ZipFile.ExtractToDirectory(zipSource, destination.FullName);
        }
     
        /// <summary>
        /// Copies all other files in the same folder of the injectee.
        /// This is likely needed if it has dependants.
        /// </summary>
        /// <param name="destination"></param>
        public static void CopyInjecteeFiles(string managedDLLFilePath, DirectoryInfo destination)
        {
            var file = new FileInfo(managedDLLFilePath);
            var dir = file.Directory;
            foreach (var f in dir.GetFiles())
            {
                f.CopyTo(Path.Combine(destination.FullName, f.Name), true);
            }

        }
       
        /// <summary>
        /// writes text file to directory with arguments for Injectee.cpp to read and pass to the 
        /// managed runtime
        /// </summary>
        /// <param name="destination"></param>
        public static void WriteManagedArgs(DirectoryInfo destination, AssemblyFramework assemblyFramework, string managedDLLPath, string? managedNamespace, string? managedMethod, string? managedArgs)
        {
            FileInfo argsFile = new FileInfo(Path.Combine(destination.FullName, "InjecteeCPP.dll.txt"));
            var args = $"{assemblyFramework}\r\n" +
                $"{managedDLLPath}\r\n" +
                $"{managedNamespace}\r\n" +
                $"{managedMethod}\r\n" +
                $"{managedArgs}";

            File.WriteAllText(argsFile.FullName, args);
        }

    }
}
