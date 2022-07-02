using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;
using Tech.Aerove.AeroInjector.Gui.Services;

namespace Tech.Aerove.AeroInjector.Gui
{

    /// <summary>
    /// This gui is build with Blazor and Photino from this example repo
    ///https://github.com/tryphotino/photino.Blazor
    ///Eventually it would be nice to use Maui once Microsoft allows you to easily publish a self contained version
    ///Build in Icons https://useiconic.com/open#icons
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

            appBuilder.Services
                .AddLogging();
            appBuilder.Services.AddSingleton<ConfigService>();
            
            // register root component and selector
            appBuilder.RootComponents.Add<App>("app");

            var app = appBuilder.Build();

            // customize window
            app.MainWindow
                .SetWidth(1200)
                .SetHeight(600)
                .SetUseOsDefaultSize(false)
                .SetIconFile("favicon.ico")
                .SetTitle("AeroInjector")
                .SetGrantBrowserPermissions(true);
            
            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                app.MainWindow.OpenAlertWindow("Fatal exception", error.ExceptionObject.ToString());
            };

            app.Run();

        }
    }
}
