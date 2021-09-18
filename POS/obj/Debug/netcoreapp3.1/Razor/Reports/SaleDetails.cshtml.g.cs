#pragma checksum "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "10d839c066a5fdbbf7eb7b70b189e41e6b627a5a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Reports_SaleDetails), @"mvc.1.0.view", @"/Reports/SaleDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"10d839c066a5fdbbf7eb7b70b189e41e6b627a5a", @"/Reports/SaleDetails.cshtml")]
    public class Reports_SaleDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "10d839c066a5fdbbf7eb7b70b189e41e6b627a5a2691", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "10d839c066a5fdbbf7eb7b70b189e41e6b627a5a5470", async() => {
                WriteLiteral("\r\n\r\n    <div>\r\n        <div class=\"center\">\r\n            <table class=\"center\">\r\n\r\n                <thead>\r\n                    <tr style=\"text-align:center;\">\r\n                        <td style=\"padding: 8px 8px 8px 8px;\"><img");
                BeginWriteAttribute("src", " src=\"", 2043, "\"", 2084, 1);
#nullable restore
#line 75 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
WriteAttributeValue("", 2049, Model.Server + Model.Client.logo, 2049, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Image\" /></td>\r\n                        <td style=\" text-align: center;padding: 8px 8px 8px 8px;\">\r\n                            <div style=\"text-align:center;  \">\r\n                                <h1>");
#nullable restore
#line 78 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
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
#line 85 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                                                Write(Model.Client.address);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 85 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                                                                       Write(Model.Client.thana);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 85 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                                                                                            Write(Model.Client.district);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - ");
#nullable restore
#line 85 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                                                                                                                     Write(Model.Client.zipcode);

#line default
#line hidden
#nullable disable
                WriteLiteral("  </td></tr>\r\n            </table>\r\n\r\n\r\n        </div>\r\n        <div style=\" text-align: right;\r\n        margin: 2rem 0rem 0rem 0rem;\">\r\n            <strong> Date : </strong> ");
#nullable restore
#line 92 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                 Write(Model.Date);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
        </div>


        <table id=""head_table"">
            <tr>
                <td><strong> Ref : </strong></td>
                <td>2020/ 78/ 96/ 47</td>

            </tr>
        </table>
        <table id=""head_table"">
            <tr>
            </tr>
            <tr>
                <td>Sales Report From <strong>");
#nullable restore
#line 107 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                         Write(Model.StartDate.ToString("dd/MM/yy"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong> to <strong>");
#nullable restore
#line 107 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                                                                                   Write(Model.EndDate.ToString("dd/MM/yy"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong> </td>


            </tr>


        </table>
        <table id=""dataTable"">
            <thead>
                <tr>
                    <th>SL </th>
                    <th>Date </th>
                    <th>Product</th>
                    <th>Customer</th>
                    <th>Manufacturer</th>
                    <th>Quantity</th>
                    <th>MRP</th>
                    <th>Discount</th>
                    <th>Total</th>

                </tr>
            <thead>
");
#nullable restore
#line 129 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                   int i = 1; 

#line default
#line hidden
#nullable disable
#nullable restore
#line 130 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                 foreach (var ph in Model.SaleDetail)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 133 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                        Write(i++);

#line default
#line hidden
#nullable disable
                WriteLiteral(". </td>\r\n                        <td>");
#nullable restore
#line 134 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                       Write(ph.entry_date.ToString("dd/MM/yy"));

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 135 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                       Write(ph.product);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 136 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                       Write(ph.customer_name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 137 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                       Write(ph.manufacturer);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 138 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                       Write(ph.quantity);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 139 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                       Write(ph.mrp);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 140 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                       Write(ph.discount);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 141 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                       Write(ph.total);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n\r\n\r\n                    </tr>\r\n");
#nullable restore
#line 146 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"



                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                <tr>
                    
                    <td colspan=""5"" style=""font-size:18px; border-bottom: 0px; text-align: right;""> Total: </td>
                    <td colspan=""1"" style=""font-size:18px; border-bottom: 0px; text-align: left;""> <strong><u>");
#nullable restore
#line 153 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                                                                                         Write(Model.Quantity_total);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</u></strong> </td>
                 
                    <td colspan=""2"" style=""font-size:18px; border-bottom: 0px; text-align: right;""> Grand Total:</td>
                    <td colspan=""1"" style=""font-size:18px; border-bottom: 0px; text-align: left;""><u>");
#nullable restore
#line 156 "D:\Portable\Back-end\POS\Reports\SaleDetails.cshtml"
                                                                                                Write(Model.GrandTotal);

#line default
#line hidden
#nullable disable
                WriteLiteral("</u></td>\r\n                </tr>\r\n\r\n\r\n\r\n</table>\r\n\r\n    </div>\r\n\r\n");
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
