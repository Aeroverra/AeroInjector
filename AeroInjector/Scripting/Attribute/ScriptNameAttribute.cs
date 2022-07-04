using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Scripting.Attribute
{
    /// <summary>
    /// Defines a name for a scriptcommand script name
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ScriptNameAttribute : System.Attribute
    {
        private string Name;

        public ScriptNameAttribute(string name)
        {
            Name = name;
       
        }
    }
}
