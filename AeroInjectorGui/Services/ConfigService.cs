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
                    Path = @"C:\Users\Nicholas\Desktop\Fortnite Research\Sources\Rift-Lawin\Lawin\Launch\LawinServer.exe",
                    Image = "https://static-cdn.jtvnw.net/jtv_user_pictures/asmongold-profile_image-f7ddcbd0332f5d28-70x70.png"
                }
            };

        }

        internal async Task AddApplication(Application applicationNew)
        {
            throw new NotImplementedException();
        }
    }
}
