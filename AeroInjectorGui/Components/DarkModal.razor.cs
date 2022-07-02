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

namespace Tech.Aerove.AeroInjector.Gui.Components
{
    public partial class DarkModal
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string ButtonName { get; set; }
        [Parameter] public Func<Task> ButtonPressed { get; set; }
    }
}