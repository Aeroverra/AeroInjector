using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Scripting
{
    public interface ScriptCommand
    {

        public List<object> Arguments { get; set; }

        public void Execute();

        public static void GetScript()
        {
        }
    }
}
