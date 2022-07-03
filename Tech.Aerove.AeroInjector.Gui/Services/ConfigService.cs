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

namespace Tech.Aerove.AeroInjector.Gui.Services
{
    public class ConfigService
    {
        public static string StoragePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AeroInjector");
        public Settings Settings = new Settings();
        private void LoadSettings()
        {
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
        public async Task Save()
        {
            await SaveSettings();
        }
        public List<Application> GetApplications()
        {
            LoadSettings();
            return Settings.Applications;
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
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        internal async Task AddApplication(Application applicationNew)
        {
            Bitmap bitmap = Icon.ExtractAssociatedIcon(applicationNew.Path).ToBitmap();
            var image = ImageToBase64(bitmap, ImageFormat.Icon);
            Settings.Applications.Add(applicationNew);
            await Save();
        }
    }
}
