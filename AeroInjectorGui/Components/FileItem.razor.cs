using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Tech.Aerove.AeroInjector.Gui;
using Microsoft.AspNetCore.Components;

namespace Tech.Aerove.AeroInjector.Gui.Components
{

    
    public partial class FileItem
    {
        [Parameter] public string Name { get; set; }
        [Parameter] public string Image { get; set; }
        [Parameter] public string Description { get; set; }
    }
}