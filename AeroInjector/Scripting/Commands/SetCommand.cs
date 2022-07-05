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
    [ScriptArg("Name", "The variable name you want to set")]
    [ScriptArg("Value", "The value you want to set")]
    public class SetCommand : IScriptCommand
    {
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string,string> Variables { get; set; }


        public void Execute()
        {
            var key = Arguments.First(x=>x.Key.ToLower()=="name").Value;
            var value = Arguments.First(x => x.Key.ToLower() == "value").Value;
            Variables[key] = value;
        }
      
    }
}
