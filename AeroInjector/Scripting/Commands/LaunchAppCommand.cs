using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{
    /// <summary>
    /// launch app 
    /// [LaunchApp] {Executable Path} {args...}
    /// </summary>
    [ScriptName("LaunchApp")]
    [ScriptArg("Path", "Specifies the path to the executable you want to Launch")]
    [ScriptArg("Args", "Specifies the arguments you want to pass to the executable on launch.")]
    public class LaunchAppCommand : IScriptCommand
    {
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }


        public void Execute()
        {

            var exePath = Arguments.First(x => x.Key.ToLower() == "path").Value;
            var args = Arguments.First(x => x.Key.ToLower() == "args").Value;
            throw new NotImplementedException();
        }
      
    }
}
