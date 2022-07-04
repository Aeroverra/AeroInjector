using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{
    public interface IScriptCommand
    {
        public List<string> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }

        public void Execute();

        public static void GetScript()
        {
        }
    }
}
