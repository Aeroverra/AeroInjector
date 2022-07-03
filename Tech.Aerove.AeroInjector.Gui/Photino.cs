using Microsoft.AspNetCore.Components.WebView;
using PhotinoNET;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Tech.Aerove.AeroInjector.Gui
{
    public static class Photino
    {
        public static void Start()
        {


            // Creating a new PhotinoWindow instance with the fluent API
            var window = new PhotinoWindow()
                .SetWidth(1200)
                .SetHeight(600)
                .SetUseOsDefaultSize(false)
                .SetIconFile("favicon.ico")
                .SetTitle("AeroInjector")
                .SetGrantBrowserPermissions(true)
                .SetResizable(true)
                .SetDevToolsEnabled(true)
                .RegisterCustomSchemeHandler("app", (object sender, string scheme, string url, out string contentType) =>
                {
                    contentType = "text/javascript";
                    return new MemoryStream(Encoding.UTF8.GetBytes(@"
                        (() =>{
                            window.setTimeout(() => {
                                alert(`🎉 Dynamically inserted JavaScript.`);
                            }, 1000);
                        })();
                    "));
                })
                .RegisterWebMessageReceivedHandler((object sender, string message) =>
                {
                    var window = (PhotinoWindow)sender;

                    string response = $"Received message: \"{message}\"";

                    window.SendWebMessage(response);
                })
                .Load("http://localhost:5109/"); // Can be used with relative path strings or "new URI()" instance to load a website.

            window.WaitForClose(); // Starts the application event loop

        }
    }
}