using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{
    public interface IScriptCommand
    {
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }

        public void Execute();


    }
    public static class IScriptCommandExtensions
    {
        /// <summary>
        /// Gets the known arguments for this command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKnownArgs(this IScriptCommand command)
        {
            var dictionary = new Dictionary<string, string>();
            var argDefinitions = command.GetType().GetCustomAttributes<ScriptArgAttribute>();
            foreach (var def in argDefinitions)
            {
                dictionary.Add(def.Name, def.Description);
            }
            return dictionary;
        }
    }
}
