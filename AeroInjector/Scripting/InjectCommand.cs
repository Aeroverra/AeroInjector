using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting
{
    [ScriptName("I")]
    internal class InjectCommand : ScriptCommand
    {
        public List<object> Arguments { get; set; }



        public void Execute()
        {
            throw new NotImplementedException();
        }

    }
}
