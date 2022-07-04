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
    internal class SleepCommand : IScriptCommand
    {
        public List<string> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }


        public void Execute()
        {
            var milliseconds = int.Parse(Arguments[0]);
            Thread.Sleep(milliseconds);
        }
    }
}
