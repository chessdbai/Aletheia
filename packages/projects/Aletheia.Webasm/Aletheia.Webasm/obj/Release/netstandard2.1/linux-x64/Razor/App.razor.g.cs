#pragma checksum "/Users/john/aletheia/packages/projects/Aletheia.Webasm/Aletheia.Webasm/App.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5abef2f267194abc0a3616acb252dcd5ca5f505e"
// <auto-generated/>
#pragma warning disable 1591
namespace Aletheia.Webasm
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Users/john/aletheia/packages/projects/Aletheia.Webasm/Aletheia.Webasm/App.razor"
using Aletheia.Pgn;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/john/aletheia/packages/projects/Aletheia.Webasm/Aletheia.Webasm/App.razor"
using Aletheia.Pgn.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/john/aletheia/packages/projects/Aletheia.Webasm/Aletheia.Webasm/App.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
    public partial class App : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 4 "/Users/john/aletheia/packages/projects/Aletheia.Webasm/Aletheia.Webasm/App.razor"
       

    [JSInvokable]
    public static PgnGame ParsePgn(string pgnText)
    {
      var parser = new PgnGameParser();
      return parser.ParseGame(pgnText);
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
