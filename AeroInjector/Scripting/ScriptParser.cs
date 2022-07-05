using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;
using Tech.Aerove.AeroInjector.Scripting.Commands;

namespace Tech.Aerove.AeroInjector.Scripting
{
    public static class ScriptParser
    {
        public static List<IScriptCommand> Parse(string script)
        {
            List<IScriptCommand> commands = new List<IScriptCommand>();
            if(script == null) { return commands; }
            Dictionary<string, string> Variables = new Dictionary<string, string>();
            foreach (var line in script.Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var command = GetCommand(line);
                command.Variables = Variables;
                commands.Add(command);
            }
            return commands;
        }

        private static List<KeyValuePair<string, string>> ParseArgs(string commandLine)
        {



            List<string> values = new List<string>();
            var pos = 0;
            while (true)
            {
                var ss = commandLine.Substring(pos);
                var start = ss.IndexOf('{');
                if (start == -1) { break; } else { start += pos; }
                var ss2 = commandLine.Substring(start + 1);
                var end = ss2.IndexOf('}') + start + 1;
                var value = commandLine.Substring(start + 1, end - (start + 1));
                values.Add(value);
                pos = end + 1;
            }

            var args = new List<KeyValuePair<string, string>>();
            foreach (var value in values)
            {
                var splitPos = value.IndexOf(':');
                var key = value.Substring(0, splitPos);
                var val = value.Substring(splitPos + 1);
                args.Add(new KeyValuePair<string, string>(key, val));
            }
            return args;
        }

        private static IScriptCommand GetCommand(string commandLine)
        {

            var commandName = commandLine.Split(" ").First().ToLower().Replace("[", "").Replace("]", "");
            var commands = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsAssignableTo(typeof(IScriptCommand)))
                .Where(x => x.GetCustomAttribute(typeof(ScriptNameAttribute)) != null)
                .ToList();

            var commandType = commands
                     .Where(x => x.GetCustomAttributes<ScriptNameAttribute>().Select(x => x.Name.ToLower()).Contains(commandName))
                     .FirstOrDefault();

            var command = (IScriptCommand)Activator.CreateInstance(commandType);
            command.Arguments = ParseArgs(commandLine);
            return command;

        }
        public static List<Type> GetKnownCommands()
        {
            var commands = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsAssignableTo(typeof(IScriptCommand)))
                .Where(x => x.GetCustomAttribute(typeof(ScriptNameAttribute)) != null)
                .ToList();
            return commands;
        }

    }
}
