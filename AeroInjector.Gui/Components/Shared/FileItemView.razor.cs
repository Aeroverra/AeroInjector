using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using Tech.Aerove.AeroInjector.Gui;

namespace Tech.Aerove.AeroInjector.Gui.Components.Shared
{
    
    public partial class FileItemView
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}