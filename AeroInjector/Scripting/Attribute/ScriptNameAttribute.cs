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
        public string Name { get; private set; }

        public ScriptNameAttribute(string name)
        {
            Name = name;
       
        }
    }
}
