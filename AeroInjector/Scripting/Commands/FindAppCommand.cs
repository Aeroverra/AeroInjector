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
    /// [FindApp] {exename}
    /// </summary>
    [ScriptName("FindApp")]
    public class FindAppCommand : IScriptCommand
    {
        public List<string> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }


        public void Execute()
        {

            throw new NotImplementedException();
        }
    }
}
