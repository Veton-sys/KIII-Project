#pragma checksum "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e008ec948de318315622d9be9edc719aec109fb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\_ViewImports.cshtml"
using Project.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\_ViewImports.cshtml"
using Project.Domain.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e008ec948de318315622d9be9edc719aec109fb", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"831c686cfb8fb4d1960e56fb2fcc8182079c42a2", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Project.Domain.DomainModels.Product>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
  
	ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
	<h2 class=""my-5"">Products in Shop</h2>
	<section>
		<div class=""container table-responsive"">
			<table class=""table table-striped table-hover table_in_home"">
				<thead>
					<tr>
						<td>Product Image</td>
						<td>Product Name</td>
						<td>Product Description</td>
						<td>Product Price</td>
						<td>Product Rating</td>
						<td>Add To Cart</td>
					</tr>
				</thead>
				<tbody>
");
#nullable restore
#line 23 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
                     foreach (var item in Model)
					{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t<td><img");
            BeginWriteAttribute("src", " src=\"", 610, "\"", 634, 1);
#nullable restore
#line 26 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
WriteAttributeValue("", 616, item.ProductImage, 616, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /></td>\r\n\t\t\t\t\t\t\t<td>");
#nullable restore
#line 27 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
                           Write(item.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t<td>");
#nullable restore
#line 28 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
                           Write(item.ProductDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t<td>");
#nullable restore
#line 29 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
                           Write(item.ProductPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t<td>");
#nullable restore
#line 30 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
                           Write(item.Rating);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t\t<a class=\"btn btn-primary\"");
            BeginWriteAttribute("href", " href=\"", 835, "\"", 888, 2);
            WriteAttributeValue("", 842, "/Products/AddToShoppingCart?productId=", 842, 38, true);
#nullable restore
#line 32 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
WriteAttributeValue("", 880, item.Id, 880, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Add To Cart</a>\r\n\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t</tr>\r\n");
#nullable restore
#line 35 "D:\Semestar 8\Kiii\Project\GitLabProject\kiii-final-update\Project_181504\Project\Project.Web\Views\Home\Index.cshtml"
					}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t</tbody>\r\n\t\t\t</table>\r\n\r\n\t\t</div>\r\n\t</section>\r\n\r\n\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Project.Domain.DomainModels.Product>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
