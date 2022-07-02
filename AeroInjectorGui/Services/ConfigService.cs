using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Gui.Models;

namespace Tech.Aerove.AeroInjector.Gui.Services
{
    public class ConfigService
    {
        public List<Application> GetApplications()
        {
            return new List<Application>
            {
                new Application
                {
                    Name = "LawinServer",
                    Path = @"C:\Users\Nicholas\Desktop\Fortnite Research\Sources\Rift-Lawin\Lawin\Launch\LawinServer.exe"
                }
            };

        }
    }
}
