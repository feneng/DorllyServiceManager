#pragma checksum "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee8d95e369dbbaa3723ff2ced8180bd532b89c6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Role_EditPermission), @"mvc.1.0.view", @"/Areas/Admin/Views/Role/EditPermission.cshtml")]
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
#nullable restore
#line 1 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
using DorllyServiceManager.Areas.Admin.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee8d95e369dbbaa3723ff2ced8180bd532b89c6b", @"/Areas/Admin/Views/Role/EditPermission.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0da43acf1e260f9fd2a6eebacf995ec47ae92c33", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Role_EditPermission : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<RolePermissionData>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Role", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditPermission", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("editform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ee8d95e369dbbaa3723ff2ced8180bd532b89c6b5808", async() => {
                WriteLiteral("\r\n            <div class=\"form-group\">\r\n                <div class=\"col-lg-12 col-sm-12 col-xs-12\">\r\n                    <input type=\"hidden\" name=\"roleId\"");
                BeginWriteAttribute("value", " value=\"", 439, "\"", 463, 1);
#nullable restore
#line 12 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
WriteAttributeValue("", 447, Model[0].RoleId, 447, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                    <table class=\"table table-striped table-bordered table-hover\" id=\"editFormTable\">\r\n                        <thead>\r\n                            <tr>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 17 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                               Write(Html.DisplayNameFor(m => Model.FirstOrDefault().PermissionName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 20 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                               Write(Html.DisplayNameFor(m => Model.FirstOrDefault().PermissionCode));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                </th>\r\n                                <th class=\"hidden-xs\">\r\n                                    ");
#nullable restore
#line 23 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                               Write(Html.DisplayNameFor(m => Model.FirstOrDefault().Status));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                                </th>
                                <th>
                                    操作
                                </th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 31 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                             foreach (var item in Model)
                            {
                                if (!item.Status) continue;

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <tr>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 36 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                                   Write(Html.DisplayFor(m => item.PermissionName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 39 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                                   Write(Html.DisplayFor(m => item.PermissionCode));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    </td>\r\n                                    <td class=\"hidden-xs\">\r\n                                        ");
#nullable restore
#line 42 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                                   Write(Html.DisplayFor(m => item.Status));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        <input type=\"checkbox\"");
                BeginWriteAttribute("value", " value=\"", 2214, "\"", 2240, 1);
#nullable restore
#line 45 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
WriteAttributeValue("", 2222, item.PermissionId, 2222, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" ");
#nullable restore
#line 45 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                                                                                      Write(item.Assigned ? "checked=checked" : "");

#line default
#line hidden
#nullable disable
                WriteLiteral(" name=\"PermissionCheck\" style=\"display:none\" />\r\n");
#nullable restore
#line 46 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                                         if (item.Assigned)
                                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                            <a href=\"#\" class=\"btn btn-default btn-xs alterRolePermissionItem\">\r\n                                                <i class=\"fa fa-trash-o\"></i> 删除\r\n                                            </a>\r\n");
#nullable restore
#line 51 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                            <a href=""#"" class=""btn btn-default btn-xs blue alterRolePermissionItem"">
                                                <i class=""fa fa-plus-square-o""></i> 添加
                                            </a>
");
#nullable restore
#line 57 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 60 "/Users/feneng/Documents/projects/DorllyServiceManager/DorllyServiceManager/Areas/Admin/Views/Role/EditPermission.cshtml"
                            }

#line default
#line hidden
#nullable disable
                WriteLiteral("                    </table>\r\n                </div>\r\n            </div>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n<script>\r\n    InitiateEditFormTable.init();\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<RolePermissionData>> Html { get; private set; }
    }
}
#pragma warning restore 1591