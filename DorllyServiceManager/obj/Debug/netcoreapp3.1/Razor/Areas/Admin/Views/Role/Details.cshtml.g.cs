#pragma checksum "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ab818a85478898096c2e32e255417c668333e07"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Role_Details), @"mvc.1.0.view", @"/Areas/Admin/Views/Role/Details.cshtml")]
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
#line 1 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/_ViewImports.cshtml"
using DorllyServiceManager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/_ViewImports.cshtml"
using DorllyServiceManager.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ab818a85478898096c2e32e255417c668333e07", @"/Areas/Admin/Views/Role/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0da43acf1e260f9fd2a6eebacf995ec47ae92c33", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Role_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DorllyService.Domain.Role>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
  
    ViewBag.Title= "角色详情";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>角色详情</h1>\n\n<div>\n    <h4>");
#nullable restore
#line 10 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
   Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\n    <hr />\n    <dl class=\"row\">\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 14 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 17 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayFor(model => model.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 21 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 24 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 28 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 31 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n\n        <dt class=\"col-sm-12\">\n            ");
#nullable restore
#line 35 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-12\">\n            ");
#nullable restore
#line 38 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n\n        <dt class=\"col-sm-12\">\n            ");
#nullable restore
#line 42 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.RolePermissions));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-12\">\n            <table class=\"table\">\n                <tr>\n                    <th>权限编码</th>\n                    <th>权限名</th>\n                </tr>\n");
#nullable restore
#line 50 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                 foreach (var item in Model.RolePermissions)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td>\n                            ");
#nullable restore
#line 54 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Permission.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </td>\n                        <td>\n                            ");
#nullable restore
#line 57 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Permission.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </td>\n                    </tr>\n");
#nullable restore
#line 60 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\n        </dd>\n\n        <dt class=\"col-sm-12\">\n            ");
#nullable restore
#line 65 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.UserRoles));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-12\">\n            <table class=\"table\">\n                <tr>\n                    <th>用户名</th>\n                    <th>用户性别</th>\n                </tr>\n");
#nullable restore
#line 73 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                 foreach (var item in Model.UserRoles)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td>\n                            ");
#nullable restore
#line 77 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                       Write(Html.DisplayFor(modelItem => item.User.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </td>\n                        <td>\n                            ");
#nullable restore
#line 80 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                       Write(Html.DisplayFor(modelItem => item.User.Sex));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </td>\n                    </tr>\n");
#nullable restore
#line 83 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\n        </dd>\n    </dl>\n</div>\n<div>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0ab818a85478898096c2e32e255417c668333e0710956", async() => {
                WriteLiteral("编辑");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 89 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/Details.cshtml"
                           WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0ab818a85478898096c2e32e255417c668333e0713097", async() => {
                WriteLiteral("返回列表");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DorllyService.Domain.Role> Html { get; private set; }
    }
}
#pragma warning restore 1591