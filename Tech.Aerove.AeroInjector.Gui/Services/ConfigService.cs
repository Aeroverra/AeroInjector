using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Gui.Models;
using Tech.Aerove.Lib.Public.WindowsNative;

namespace Tech.Aerove.AeroInjector.Gui.Services
{
    public class ConfigService
    {
        public static string StoragePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AeroInjector");

        public Settings Settings = null;
    
        private void LoadSettings()
        {
            if(Settings != null)
            {
                return;
            }
            FileInfo fileInfo = new FileInfo(Path.Combine(StoragePath, "settings.json"));
            if (fileInfo.Exists)
            {
                var jsonSettings = File.ReadAllText(fileInfo.FullName);
                Settings = JsonConvert.DeserializeObject<Settings>(jsonSettings);
            }
        }
   
        private async Task SaveSettings()
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(StoragePath, "settings.json"));
            fileInfo.Directory.Create();
            var jsonSettings = JsonConvert.SerializeObject(Settings);
            await File.WriteAllTextAsync(fileInfo.FullName, jsonSettings);
        }
   
 
        public List<Application> GetApplications()
        {
            LoadSettings();
            return Settings.Applications.Where(x=>!x.IsInjectee).ToList();
        }
        public List<Application> GetDlls()
        {
            LoadSettings();
            return Settings.Applications.Where(x => x.IsInjectee).ToList();
        }

        internal async Task AddApplication(Application applicationNew)
        {
            applicationNew.Image = IconTools.GetIconAsImageUri(applicationNew.Path);
            Settings.Applications.Add(applicationNew);
            await SaveSettings();
        }
        internal async Task UpdateApplication(Application applicationNew)
        {
            applicationNew.Image = IconTools.GetIconAsImageUri(applicationNew.Path);
            await SaveSettings();
        }
        internal async Task RemoveApplication(Application applicationNew)
        {
            Settings.Applications.Remove(applicationNew);
            await SaveSettings();
        }
    }
}
