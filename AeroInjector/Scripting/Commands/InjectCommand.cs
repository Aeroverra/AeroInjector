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
    [ScriptArg("Namespace", "Namespace within a managed DLL where your first method will be called.")]
    [ScriptArg("Method", "Method to call within a managed DLL.")]
    public class InjectCommand : IScriptCommand
    {
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }



        public void Execute()
        {
            var processId = int.Parse(Variables["processid"]);
            var dllPath = Arguments.First(x => x.Key.ToLower() == "path").Value;
            var originalPath = new FileInfo(dllPath);


            var args = "";
            if (Arguments.Any(x => x.Key.ToLower() == "args"))
            {
                args = Arguments.FirstOrDefault(x => x.Key.ToLower() == "args").Value;
            }
            var managedNamespace = "";
            if (Arguments.Any(x => x.Key.ToLower() == "namespace"))
            {
                managedNamespace = Arguments.FirstOrDefault(x => x.Key.ToLower() == "namespace").Value;
            }
            var managedMethod = "";
            if (Arguments.Any(x => x.Key.ToLower() == "method"))
            {
                managedMethod = Arguments.FirstOrDefault(x => x.Key.ToLower() == "method").Value;
            }

            var injector = new Injector(processId, dllPath, managedNamespace, managedMethod);
            injector.Inject(args);
        }



    }
}
