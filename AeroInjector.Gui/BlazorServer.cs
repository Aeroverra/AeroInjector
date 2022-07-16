using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;
using System.Reflection.PortableExecutable;
using Tech.Aerove.AeroInjector.Gui.Services;

namespace Tech.Aerove.AeroInjector.Gui
{
    public class BlazorServer
    {

        private WebApplication WebApplication;
        public async Task StopAsync(string[] args)
        {
            await WebApplication?.StopAsync();
        }
        public async Task StartAsync(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:0");


            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<ConfigService>();

            var app = builder.Build();
            WebApplication = app;
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            await app.StartAsync();
           

        }
        public int GetPort()
        {
            foreach (var address in WebApplication.Urls)
            {
                var uri = new Uri(address);
                var port = uri.Port;
                return port;
            }
            return -1;
        }
        public Task WaitForShutdownAsync()
        {
            return WebApplication.WaitForShutdownAsync();
        }
    }
}
