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
    public partial class DLLs
    {
        [Inject] public ConfigService ConfigService { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        public Application ApplicationNew { get; set; } = new Application { IsInjectee = true};

        public async Task PickApplication()
        {
            //Needs STA thread or it throws errors....
            Thread thread = new Thread(() =>
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.ShowDialog();
                ApplicationNew.Path = openFileDialog.FileName;
            });
            thread.SetApartmentState(ApartmentState.STA);

            thread.Start();
            while (true)
            {
                if (thread.ThreadState == ThreadState.Running)
                {
                    await Task.Delay(250);
                    continue;
                }
                break;
            }
        }

        public async Task AddApplication()
        {
            await ConfigService.AddApplication(ApplicationNew);
            ApplicationNew = new Application();
            await JS.InvokeVoidAsync("closeModal", "addModal");
        }

    }
}