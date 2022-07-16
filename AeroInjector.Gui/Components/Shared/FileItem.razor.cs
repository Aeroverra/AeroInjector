using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Tech.Aerove.AeroInjector.Gui;
using Microsoft.AspNetCore.Components;
using Tech.Aerove.AeroInjector.Gui.Services;
using Tech.Aerove.AeroInjector.Gui.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.JSInterop;

namespace Tech.Aerove.AeroInjector.Gui.Components.Shared
{


    public partial class FileItem
    {
        [Inject] public ConfigService ConfigService { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        [Parameter] public Application Application { get; set; }
        public string ModalId = $"Edit{Guid.NewGuid()}";

        public async Task OpenModal()
        {
            await JS.InvokeVoidAsync("openModal", ModalId);
        }
        public async Task PickApplication()
        {
            //Needs STA thread or it throws errors....
            Thread thread = new Thread(() =>
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.ShowDialog();
                Application.Path = openFileDialog.FileName;
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

        public async Task UpdateApplication()
        {
            await ConfigService.UpdateApplication(Application);
            await JS.InvokeVoidAsync("closeModal", ModalId);
        }
        public async Task DeleteApplication()
        {
            await ConfigService.RemoveApplication(Application);
            await JS.InvokeVoidAsync("closeModal", ModalId);
            Application = null;
    
        }
    }
}