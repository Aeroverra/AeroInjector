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

namespace Tech.Aerove.AeroInjector.Gui.Pages
{
    public partial class Scripts : IDisposable
    {
        [Inject] public ConfigService ConfigService { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public Script? Selected { get; set; }
        public Script NewScript { get; set; } = new Script();

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (ConfigService.GetScripts().Any(x => x.IsLastModified))
                {
                    Selected = ConfigService.GetScripts().FirstOrDefault(x => x.IsLastModified);
                }
                NavigationManager.LocationChanged += OnLeavePage;
                StateHasChanged();
            }
    
        }

        private void OnLeavePage(object? sender, LocationChangedEventArgs args)
        {
            _= ConfigService.SaveScript(Selected);
        }
        public async Task OnSave()
        {
            await ConfigService.SaveScript(Selected);
        }
        public async Task OnDelete()
        {
            await ConfigService.RemoveScript(Selected);
            Selected = null;
            await JS.InvokeVoidAsync("closeModal", "deleteModal");
        }
        public async Task OnScriptChange(ChangeEventArgs args)
        {
            var id = Guid.Parse($"{args.Value}");

            if (id == Guid.Empty)
            {
                Selected = null;
                return;
            }
            if (id == NewScript.Id)
            {
                Selected = NewScript;
                NewScript = new Script();
                StateHasChanged();
                await ConfigService.SaveScript(Selected);
                await JS.InvokeVoidAsync("focusElement", "scriptName");
            }

            Selected = ConfigService.GetScripts().FirstOrDefault(x => x.Id == id);
        }
        private async Task UpdateUI()
        {
            StateHasChanged();
            await Task.Delay(1);
        }
        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLeavePage;
        }

    }
}