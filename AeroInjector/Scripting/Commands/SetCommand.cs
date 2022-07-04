using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{

    /// <summary>
    /// Sets a variable
    /// [Set] {variable name} {value}
    /// </summary>
    [ScriptName("Set")]
    internal class SetCommand : IScriptCommand
    {
        public List<string> Arguments { get; set; }
        public Dictionary<string,string> Variables { get; set; }


        public void Execute()
        {
            var key = Arguments[0];
            var value = Arguments[1];
            Variables[key] = value;
        }
    }
}
