using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{
    /// <summary>
    /// Sleeps for specified milliseconds
    /// [Sleep] {mstime}
    /// </summary>
    [ScriptName("Sleep")]
    [ScriptArg("Length", "The time in milliseconds to sleep")]
    public class SleepCommand : IScriptCommand
    {
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }


        public void Execute()
        {
            var milliseconds = int.Parse(Arguments.First(x=>x.Key.ToLower() == "length").Value);
            Thread.Sleep(milliseconds);
        }

    }
}
