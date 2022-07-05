using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{
    /// <summary>
    /// Finds a running process id be exe name and sets the processid var
    /// [FindApp] {Executable Name}
    /// </summary>
    [ScriptName("FindApp")]
    [ScriptArg("Executable","Finds executable with specified name and sets the processid variable.")]
    public class FindAppCommand : IScriptCommand
    {
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }


        public void Execute()
        {

            throw new NotImplementedException();
        }

       
    }
}
