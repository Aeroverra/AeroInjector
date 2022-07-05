using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Tech.Aerove.AeroInjector.Gui;
using Tech.Aerove.AeroInjector.Gui.Shared;
using Tech.Aerove.AeroInjector.Gui.Services;
using Tech.Aerove.AeroInjector.Gui.Models;
using Tech.Aerove.AeroInjector.Scripting.Commands;
using Tech.Aerove.Lib.Public.WindowsNative;
using System.Reflection;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Gui.Pages
{
    public partial class Index
    {
        [Inject] public ConfigService ConfigService { get; set; }
        public async Task OnLaunch()
        {

        }
        private  string GetImage(Script script)
        {
            try
            {
                var app = script.GetAsCommands().FirstOrDefault(x => x.GetType() == typeof(LaunchAppCommand));

                var path = app.Arguments.FirstOrDefault(x => x.Key.ToLower() == "path").Value;

                return IconTools.GetIconAsImageUri(path);
            }
            catch
            {
                return "";
            }
        }
        private string  GetDescriptions(Script script)
        {
            var description = "";
            foreach(var command in script.GetAsCommands())
            {
                var commandName = command.GetType().GetCustomAttribute<ScriptNameAttribute>().Name;
                var commandValue = command.Arguments.First().Value;
                if(command.Arguments.First().Key.ToLower() == "path")
                {
                    commandValue = commandValue.Split("\\").Last();
                }
                description += $" {{{commandName}:{commandValue}}}";
            }
            return description;
        }
    }
}