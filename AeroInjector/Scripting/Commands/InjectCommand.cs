using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{

    /// <summary>
    /// Injects a dll into the process set to the processid var and launches it with remote thread execution
    /// [Inject] {dllpath} {params}
    /// </summary>
    [ScriptName("Inject")]
    internal class InjectCommand : IScriptCommand
    {
        public List<string> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }

        public void Execute()
        {
            var processId = Variables["processid"];
            var dllPath = Arguments[0];
            var args = String.Join(" ", Arguments.Skip(1).ToList());
            throw new NotImplementedException();
        }

    }
}
