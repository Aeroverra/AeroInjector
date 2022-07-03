using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Aerove.AeroInjector.Gui.Models
{
    public class Application 
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Image { get; set; }
        public bool IsInjectee { get; set; }
    }
}
