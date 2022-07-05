using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Scripting.Attribute
{
    /// <summary>
    /// Defines an argument for a scriptcommand
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    public class ScriptArgAttribute : System.Attribute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ScriptArgAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
