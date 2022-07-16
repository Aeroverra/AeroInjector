using System.Reflection.PortableExecutable;
using Tech.Aerove.AeroInjector.Gui.Services;

namespace Tech.Aerove.AeroInjector.Gui
{
    public static class BlazorServer
    {

        private static WebApplication? WebApplication;
        public static async Task StopAsync(string[] args)
        {
            await WebApplication?.StopAsync();
        }
        public static async Task StartAsync(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            await app.RunAsync();
        }
    }
}
