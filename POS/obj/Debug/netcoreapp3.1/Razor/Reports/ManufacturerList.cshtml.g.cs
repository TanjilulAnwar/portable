#pragma checksum "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "06d3e1d8b9487242ab62d742c5ed7b6f3e3218a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Reports_ManufacturerList), @"mvc.1.0.view", @"/Reports/ManufacturerList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"06d3e1d8b9487242ab62d742c5ed7b6f3e3218a4", @"/Reports/ManufacturerList.cshtml")]
    public class Reports_ManufacturerList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "06d3e1d8b9487242ab62d742c5ed7b6f3e3218a42717", async() => {
                WriteLiteral(@"
    
        <link rel=""preconnect"" href=""https://fonts.gstatic.com"" >
        <link href=""https://fonts.googleapis.com/css2?family=IBM+Plex+Sans&display=swap"" rel=""stylesheet"" >
        <style >
        body {
            font-family: 'IBM Plex Sans', sans-serif;
        }
            #dataTable {
                font-family: 'IBM Plex Sans', sans-serif;
                border-collapse: collapse;
                width: 100%;
            }

                #dataTable td, #dataTable th {
                    border: 2px solid #000000;
                    padding: 8px 8px 8px 8px;
                }

            /*#customers tr:nth-child(even){background-color: #f2f2f2;}*/

            /*#customers tr:hover {background-color: #ddd;}*/

                #dataTable th {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    text-align: left;
                    /*  background-color: #373737;
                     color: white;*/
               ");
                WriteLiteral(@"     color: #373737;
                }

        img {
            width: 70px;
            height: 70px;
        }

        .center {
            margin: auto;
            
            /*  margin: 3rem 5rem 2rem 0rem;*/
            /*margin-right: auto;*/
        }

        #head_table {
       
            border-collapse: collapse;
            margin: 2rem 5rem 2rem 0rem; /* top=1em, right=2em, bottom=3em, left=2em */
        }

        #head_table td {
             
                padding: 8px 8px 8px 8px;
        }
    </style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "06d3e1d8b9487242ab62d742c5ed7b6f3e3218a45313", async() => {
                WriteLiteral("\r\n\r\n    <div>\r\n\r\n        <div class=\"center\">\r\n            <table class=\"center\">\r\n\r\n                <thead>\r\n                    <tr style=\"text-align:center;\">\r\n                        <td style=\"padding: 8px 8px 8px 8px;\"><img");
                BeginWriteAttribute("src", " src=\"", 1866, "\"", 1907, 1);
#nullable restore
#line 69 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
WriteAttributeValue("", 1872, Model.Server + Model.Client.logo, 1872, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Image\" /></td>\r\n                        <td style=\" text-align: center;padding: 8px 8px 8px 8px;\">\r\n                            <div style=\"text-align:center;  \">\r\n                                <h1>");
#nullable restore
#line 72 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
                               Write(Model.Client.name);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h1>
                            </div>
                        </td>
                    </tr>
                </thead>
            </table>
            <table class=""center"" width=70%>
                <tr><td style=""text-align:center; font-size:15px ;""> ");
#nullable restore
#line 79 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
                                                                Write(Model.Client.address);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 79 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
                                                                                       Write(Model.Client.thana);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 79 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
                                                                                                            Write(Model.Client.district);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - ");
#nullable restore
#line 79 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
                                                                                                                                     Write(Model.Client.zipcode);

#line default
#line hidden
#nullable disable
                WriteLiteral("  </td></tr>\r\n            </table>\r\n\r\n\r\n        </div>\r\n        <div style=\" text-align: right;\r\n        margin: 2rem 0rem 0rem 0rem;\">\r\n            <strong> Date : </strong> ");
#nullable restore
#line 86 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
                                 Write(Model.Date);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        </div>\r\n\r\n\r\n        <table id=\"head_table\">\r\n            <tr>\r\n                <td><strong> Ref : </strong></td>\r\n                <td>2021/08/75/Manufacturer</td>\r\n\r\n            </tr>\r\n\r\n            <tr>\r\n");
                WriteLiteral("            </tr>\r\n");
                WriteLiteral(@"
        </table>
        <table id=""dataTable"">
            <thead>
                <tr>

                    <th>SL</th>
                    <th>NAME</th>
                    <th>CONTACT PERSON</th>
                    <th>BRAND</th>
                    <th>ADDRESS</th>
                    <th>PHONE</th>
                    <th>EMAIL</th>

                </tr>
            </thead>
");
#nullable restore
#line 127 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
               int i = 1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 128 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
             foreach (var p in Model.ManufacturerList)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 131 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
            Write(i++);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 132 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
           Write(p.name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 133 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
           Write(p.contact_person);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 134 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
           Write(p.brand);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 135 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
           Write(p.address);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 136 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
           Write(p.phone);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 137 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"
           Write(p.email);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 139 "K:\toufiq\backend\POS\Reports\ManufacturerList.cshtml"


            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </table>\r\n\r\n        </div>\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
