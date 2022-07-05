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
using Tech.Aerove.AeroInjector.Gui.Models;
using Tech.Aerove.AeroInjector.Scripting.Commands;
using Tech.Aerove.AeroInjector.Scripting;
using Tech.Aerove.AeroInjector.Scripting.Attribute;
using System.Reflection;

namespace Tech.Aerove.AeroInjector.Gui.Components.Scripts
{
    public partial class VisualBuilder
    {
        [Parameter] public Script Script { get; set; }
        [Parameter] public Func<Task> Update { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        public List<IScriptCommand> Commands { get; set; } = new List<IScriptCommand>();
        public string ParseError = "";
        public int SelectedCommandLineNumber = 0;
        protected override void OnParametersSet()
        {
            try
            {
                Commands = ScriptParser.Parse(Script.Content);
                ParseError = "";
            }
            catch (Exception e)
            {
                ParseError = e.ToString();
            }

        }
        private string GetName(IScriptCommand scriptCommand)
        {
            return scriptCommand.GetType().GetCustomAttributes<ScriptNameAttribute>().First().Name;
        }
        private async Task AddCommand()
        {
            Script.Content += "\n[LaunchApp]";
            await ModifyCommand(Script.ContentLineCount - 1);
        }
        private async Task ModifyCommand(int line)
        {
            SelectedCommandLineNumber = line;
            StateHasChanged();
            await Task.Delay(1);
            await JS.InvokeVoidAsync("openModal", "modifyModal");
        }

        private async Task OnDeleteCommand(int line)
        {
            var lines = Script.GetLines();
            lines.RemoveAt(line);
            Script.SetLines(lines);
            await Task.Delay(1);
            await UpdateUI();
        }
        private async Task UpdateUI()
        {
            await Update();
        }
    }
}