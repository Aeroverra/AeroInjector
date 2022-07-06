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
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }


        public void Execute()
        {
            var processId = int.Parse(Variables["processid"]);
            var dllPath = Arguments.First(x => x.Key.ToLower() == "path").Value;
            var args = "";
            if (Arguments.Any(x => x.Key.ToLower() == "args"))
            {
                args = Arguments.FirstOrDefault(x => x.Key.ToLower() == "args").Value;
            }
            Injector.Inject(processId, dllPath);
        }



    }
}
