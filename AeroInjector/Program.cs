using System.Text;
using Tech.Aerove.AeroInjector.Scripting;

namespace Tech.Aerove.AeroInjector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mode = args[0];
            var value = args[1];
            switch (mode)
            {
                case "script":
                    byte[] data = Convert.FromBase64String(value);
                    string decodedString = Encoding.UTF8.GetString(data);
                    RunScript(decodedString);
                    break;
                case "scriptpath":
                    //todo implement auto reading file
                    break;
            }
        }
        public static void RunScript(string script)
        {
            var commands = ScriptParser.Parse(script);
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
    }
}