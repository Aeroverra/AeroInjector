using Microsoft.AspNetCore.Components;
using Tech.Aerove.AeroInjector.Gui.Services;
using Tech.Aerove.AeroInjector.Gui.Models;
using Microsoft.JSInterop;

namespace Tech.Aerove.AeroInjector.Gui.Pages
{

    public partial class Applications
    {
        [Inject] public ConfigService ConfigService { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        public Application ApplicationNew { get; set; } = new Application();




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
                if(thread.ThreadState == ThreadState.Running)
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