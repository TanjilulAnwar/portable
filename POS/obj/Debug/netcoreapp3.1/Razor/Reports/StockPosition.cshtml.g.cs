#pragma checksum "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68bd7d7a158f14cf2ef3da4874a8a7ddd2d2e8f7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Reports_StockPosition), @"mvc.1.0.view", @"/Reports/StockPosition.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"68bd7d7a158f14cf2ef3da4874a8a7ddd2d2e8f7", @"/Reports/StockPosition.cshtml")]
    public class Reports_StockPosition : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "68bd7d7a158f14cf2ef3da4874a8a7ddd2d2e8f72701", async() => {
                WriteLiteral(@"

    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=IBM+Plex+Sans&display=swap"" rel=""stylesheet"">
    <style>
        body {
            font-family: 'IBM Plex Sans', sans-serif;
        }

        #dataTable {
            /*   border-collapse: collapse;*/
            border-collapse: collapse;
            width: 100%;
            table-layout: auto;
        }

            #dataTable th {
                /* border: 1px  solid #373737;*/ /* solid #373737*/
                border-bottom: 1px solid #373737;
                padding: 3px 3px 3px 3px;
                text-align: left;
            }

            #dataTable td {
                /* border: 1px  solid #373737;*/ /* solid #373737*/
                border-bottom: 1px solid #373737;
                padding: 3px 3px 3px 3px;
                text-align: left;
            }

        img {
            width: 70px;
            height: 70px;
        }

       ");
                WriteLiteral(@" .center {
            margin: auto;
            /*  margin: 3rem 5rem 2rem 0rem;*/
            /*margin-right: auto;*/
        }

        body {
            counter-reset: item-counter;
        }

        .item {
            counter-increment: item-counter;
        }

            .item:before {
                content: counter(item-counter, lower-roman) "".""; /* by specifying the upper-roman as style the output would be in roman numbers */
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "68bd7d7a158f14cf2ef3da4874a8a7ddd2d2e8f75480", async() => {
                WriteLiteral("\r\n\r\n    <div>\r\n        <div class=\"center\">\r\n            <table class=\"center\">\r\n\r\n                <thead>\r\n                    <tr style=\"text-align:center;\">\r\n                        <td style=\"padding: 8px 8px 8px 8px;\"><img");
                BeginWriteAttribute("src", " src=\"", 2043, "\"", 2084, 1);
#nullable restore
#line 75 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
WriteAttributeValue("", 2049, Model.Server + Model.Client.logo, 2049, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Image\" /></td>\r\n                        <td style=\" text-align: center;padding: 8px 8px 8px 8px;\">\r\n                            <div style=\"text-align:center;  \">\r\n                                <h1>");
#nullable restore
#line 78 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
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
#line 85 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                                                                Write(Model.Client.address);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 85 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                                                                                       Write(Model.Client.thana);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 85 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                                                                                                            Write(Model.Client.district);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - ");
#nullable restore
#line 85 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                                                                                                                                     Write(Model.Client.zipcode);

#line default
#line hidden
#nullable disable
                WriteLiteral("  </td></tr>\r\n            </table>\r\n\r\n\r\n        </div>\r\n        <div style=\" text-align: right;\r\n        margin: 2rem 0rem 0rem 0rem;\">\r\n            <strong> Date : </strong> ");
#nullable restore
#line 92 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                                 Write(Model.Date);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        </div>\r\n\r\n\r\n     \r\n        <table id=\"head_table\">\r\n            <tr>\r\n            </tr>\r\n            <tr>\r\n                <td>Stock Position Report at <strong> ");
#nullable restore
#line 101 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                                                 Write(Model.Date);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
#nullable restore
#line 101 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                                                             Write(Model.Time);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong>  </td>


            </tr>


        </table>

        <table id=""dataTable"">
            <thead>
                <tr>
                    <th>SL </th>
                    <th>Code </th>
                    <th>Name</th>

                    <th>Qty</th>
                    <th>MRP</th>
                    <th>Unit Price</th>
                    <th>Total Value (BDT)</th>


                </tr>
            <thead>
");
#nullable restore
#line 124 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                   int i = 1; 

#line default
#line hidden
#nullable disable
#nullable restore
#line 125 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                 foreach (var ph in Model.productList)
                {
                    double total_value = ph.unit_price * ph.quantity; 

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 129 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                        Write(i++);

#line default
#line hidden
#nullable disable
                WriteLiteral(". </td>\r\n                        <td>");
#nullable restore
#line 130 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                       Write(ph.product_code);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 131 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                       Write(ph.product_name);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 132 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                       Write(ph.quantity);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 133 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                       Write(ph.mrp_price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 134 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                       Write(ph.unit_price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 135 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"
                       Write(total_value);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 137 "D:\Portable\Back-end\POS\Reports\StockPosition.cshtml"



                }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n\r\n</table>\r\n\r\n    </div>\r\n\r\n");
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
