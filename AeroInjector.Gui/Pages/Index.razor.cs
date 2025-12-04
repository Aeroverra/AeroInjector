using Microsoft.AspNetCore.Components;
using System.Reflection;
using Tech.Aerove.AeroInjector.Gui.Models;
using Tech.Aerove.AeroInjector.Gui.Services;
using Tech.Aerove.AeroInjector.Gui.WindowsNative;
using Tech.Aerove.AeroInjector.Scripting.Attribute;
using Tech.Aerove.AeroInjector.Scripting.Commands;

namespace Tech.Aerove.AeroInjector.Gui.Pages
{
    public partial class Index
    {
        [Inject] public ConfigService ConfigService { get; set; }
        public void OnLaunch(Script script)
        {
            AeroInjector.Program.RunScript(script.Content);
        }
        private string GetImage(Script script)
        {
            var app = script.GetAsCommands().FirstOrDefault(x => x.GetType() == typeof(LaunchAppCommand));

            if (app != null && app.Arguments.Any(x => x.Key.ToLower() == "path"))
            {
                var path = app.Arguments.LastOrDefault(x => x.Key.ToLower() == "path").Value;
                return IconTools.GetIconAsImageUri(path);
            }
            return "";
        }
        private string GetDescriptions(Script script)
        {
            var description = "";
            foreach (var command in script.GetAsCommands())
            {
                var commandName = command.GetType().GetCustomAttribute<ScriptNameAttribute>().Name;
                if (command.Arguments.Count() == 0)
                {
                    continue;
                }
                var commandValue = command.Arguments.First().Value;
                if (command.Arguments.First().Key.ToLower() == "path")
                {
                    commandValue = commandValue.Split("\\").Last();
                }
                description += $" {{{commandName}:{commandValue}}}";
            }
            return description;
        }
    }
}