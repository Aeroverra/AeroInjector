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
using Tech.Aerove.AeroInjector.Scripting;
using Tech.Aerove.AeroInjector.Scripting.Attribute;
using System.Reflection;
using Tech.Aerove.AeroInjector.Gui.Services;
using Tech.Aerove.AeroInjector.Scripting.Commands;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Tech.Aerove.AeroInjector.Gui.Components.Scripts
{
    public partial class ModifyCommand
    {

        [Parameter] public Script Script { get; set; }
        [Parameter] public int LineNumber { get; set; }
        [Parameter] public Func<Task> Update { get; set; }
        [Inject] public ConfigService ConfigService { get; set; }
        public List<KeyValuePair<string, string>> Arguments { get; set; } = new List<KeyValuePair<string, string>>();
        public Type CommandType { get; set; } = typeof(Scripting.Commands.LaunchAppCommand);
        public IScriptCommand Command = new LaunchAppCommand();
        Dictionary<string, string> KnownCommandArgs { get { return Command.GetKnownArgs(); } }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                ScriptParser.GetKnownCommands();
                var line = Script.GetLine(LineNumber);
                Command = ScriptParser.Parse(line)[0];
                Arguments = Command.Arguments;
                CommandType = Command.GetType();
                await Task.Delay(1);
            }
            catch (Exception e)
            {

            }
        }
        private string GetCommandName(Type type)
        {
            var name = type.GetCustomAttribute<ScriptNameAttribute>();
            return name.Name;
        }
        private async Task OnDeleteArg(KeyValuePair<string, string> arg)
        {
            Arguments.Remove(arg);
            await UpdateLine();
        }
        private async Task OnArgTypeChange(ChangeEventArgs args, KeyValuePair<string, string> arg)
        {
            var type = args.Value.ToString();
            var newArg = new KeyValuePair<string, string>(type, arg.Value);
            Arguments[Arguments.IndexOf(arg)] = newArg;
            await UpdateLine();
        }
        private async Task OnArgValueChange(ChangeEventArgs args, KeyValuePair<string, string> arg)
        {
            var value = args.Value.ToString();
            var newArg = new KeyValuePair<string, string>(arg.Key, value);
            Arguments[Arguments.IndexOf(arg)] = newArg;
            await UpdateLine();
        }
        private async Task OnCommandTypeChange(ChangeEventArgs args)
        {
            var typename = args.Value.ToString();
            CommandType = ScriptParser.GetKnownCommands().SingleOrDefault(x => x.ToString() == typename);
            await UpdateLine();
        }
        private async Task UpdateLine()
        {
            var line = $"[{GetCommandName(CommandType)}]";
            foreach (var arg in Arguments)
            {
                line += $" {{{arg.Key}:{arg.Value}}}";
            }
            var lines = Script.GetLines();
            lines[LineNumber] = line;
            Script.SetLines(lines);

            Command = ScriptParser.Parse(line)[0];
            await ConfigService.SaveScript(Script);
            await Update();
        }
        private async Task OnNewArg()
        {
            Arguments.Add(new KeyValuePair<string, string>(KnownCommandArgs.First().Key, ""));
            await UpdateLine();
        }


    }
}