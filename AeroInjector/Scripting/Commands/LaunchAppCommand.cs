using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{
    [ScriptName("LaunchApp")]
    internal class LaunchAppCommand : IScriptCommand
    {
        public List<string> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }


        public void Execute()
        {
           
            var exePath = Arguments[0];
            var args = String.Join(" ", Arguments.Skip(1).ToList());
            throw new NotImplementedException();
        }
    }
}
