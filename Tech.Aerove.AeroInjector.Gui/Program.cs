using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PhotinoNET;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tech.Aerove.AeroInjector.Gui.Data;

namespace Tech.Aerove.AeroInjector.Gui
{
    /// <summary>
    /// This gui is build with Blazor and Photino from this example repo
    /// https://github.com/tryphotino/photino.Blazor
    /// This is now its own version running on Blazor Server because the example repo had too many issues
    /// Eventually it would be nice to use Maui once Microsoft allows you to easily publish a self contained version
    /// Build in Icons https://useiconic.com/open#icons
    /// </summary>
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {

            _ = BlazorServer.StartAsync(args);
            Thread.Sleep(1000);
            Photino.Start();

        }


    }
}