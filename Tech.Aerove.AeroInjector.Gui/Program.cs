using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PhotinoNET;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tech.Aerove.AeroInjector.Gui.Data;

namespace Tech.Aerove.AeroInjector.Gui
{
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