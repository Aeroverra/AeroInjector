using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Injection;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{

    /// <summary>
    /// Injects a dll into the process set to the processid var and launches it with remote thread execution
    /// [Inject] {DLL Path} {args...}
    /// </summary>
    [ScriptName("Inject")]
    [ScriptArg("Path", "Specifies the path to the DLL you want to Inject")]
    [ScriptArg("Args", "Specifies the arguments you want to pass to the dll on injection.")]
    public class InjectCommand : IScriptCommand
    {
        public static string StoragePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AeroInjector");
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }
        public readonly DirectoryInfo DLLPath;
        public InjectCommand()
        {
            var path = Path.Combine(StoragePath, "Temp");
            path = Path.Combine(path, $"{Guid.NewGuid()}");
            var dir = new DirectoryInfo(path);
            dir.Create();
            DLLPath = dir;
        }

        public void Execute()
        {
            var processId = int.Parse(Variables["processid"]);
            var dllPath = Arguments.First(x => x.Key.ToLower() == "path").Value;
            var originalPath = new FileInfo(dllPath);
            var newPath = Path.Combine(DLLPath.FullName, originalPath.Name);
            var argsPath = Path.Combine(DLLPath.FullName, originalPath.Name) + ".txt";
            File.Copy(dllPath, newPath);
            var args = "";
            if (Arguments.Any(x => x.Key.ToLower() == "args"))
            {
                args = Arguments.FirstOrDefault(x => x.Key.ToLower() == "args").Value;
            }
            File.WriteAllText(argsPath, args);
            var injector = new Injector(processId, newPath);
            injector.Attach();
            injector.Inject();
        }



    }
}
