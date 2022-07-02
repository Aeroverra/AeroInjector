using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Tech.Aerove.AeroInjector.Gui;
using Microsoft.AspNetCore.Components;
using Tech.Aerove.AeroInjector.Gui.Services;
namespace Tech.Aerove.AeroInjector.Gui.Pages
{
    public partial class Applications
    {
        [Inject] public ConfigService ConfigService { get;set;}

    }
}