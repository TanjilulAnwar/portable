#pragma checksum "K:\toufiq\backend\POS\Reports\Receipt.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d98106e4ab5bff293c742da6db0b0996f92828eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Reports_Receipt), @"mvc.1.0.view", @"/Reports/Receipt.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d98106e4ab5bff293c742da6db0b0996f92828eb", @"/Reports/Receipt.cshtml")]
    public class Reports_Receipt : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d98106e4ab5bff293c742da6db0b0996f92828eb2668", async() => {
                WriteLiteral("\r\n\r\n    <link rel=\"preconnect\" href=\"https://fonts.gstatic.com\">\r\n    <link href=\"https://fonts.googleapis.com/css2?family=Inconsolata:wght@300&display=swap\" rel=\"stylesheet\">\r\n    <style>\r\n\r\n        ");
                WriteLiteral("@media print {\r\n            h1 {\r\n                page-break-before: always;\r\n            }\r\n        }\r\n        ");
                WriteLiteral("@font-face {\r\n            font-family: IDAutomationHC39M;\r\n            src: url(");
#nullable restore
#line 16 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                Write(Model.Server);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/IDAutomationHC39M.woff);
        }

        body {
            width: 98%;
    /*        background: #fbfbeb;*/
            font-family: 'Inconsolata', monospace;
        }

        #dataTable {

            font-family: 'Inconsolata', monospace;
            border-collapse: collapse;
            width: 100%;
        }

            #dataTable th {
                border-top: 1px solid #000000;
                border-bottom: 1px dashed #000000;
                padding: 4px;
            }

            #dataTable td {
                border-bottom: 1px solid #000000;
                padding: 2px 8px 2px 8px;
            }

            /*#customers tr:nth-child(even){background-color: #f2f2f2;}*/

            /*#customers tr:hover {background-color: #ddd;}*/

            #dataTable th {
                text-align: left;
                color: #000000;
            }

        img {
            width: 150px;
            height: 150px;
        }

        #head_table {

    ");
                WriteLiteral(@"     /* top=1em, right=2em, bottom=3em, left=2em */
        }

            #head_table td {
                padding: 2px 2px 2px 2px;
            }

        .centerImg {
            text-align: center;
            display: block;
            margin-left: auto;
            margin-right: auto;
            margin: auto;
            width: 50%;
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d98106e4ab5bff293c742da6db0b0996f92828eb5768", async() => {
                WriteLiteral("\r\n\r\n    <div>\r\n\r\n        <div>\r\n\r\n");
                WriteLiteral("            <div class=\"centerImg\" style=\"padding-top:40px;margin-bottom:0px;padding-bottom:0px;\">\r\n                <img");
                BeginWriteAttribute("src", " src=\"", 2149, "\"", 2188, 1);
#nullable restore
#line 84 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
WriteAttributeValue("", 2155, Model.Server+Model.Client.logo, 2155, 33, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Image\" />\r\n            </div>\r\n            <div style=\"text-align:center; font-size:30px ;\">\r\n");
                WriteLiteral("                <strong>");
#nullable restore
#line 88 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                   Write(Model.Client.name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong>\r\n\r\n            </div>\r\n            <div style=\"text-align:center; font-size:12px ;\">\r\n                ");
#nullable restore
#line 92 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
           Write(Model.Client.address);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 92 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                  Write(Model.Client.thana);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 92 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                       Write(Model.Client.district);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - ");
#nullable restore
#line 92 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                                                Write(Model.Client.zipcode);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n            </div>\r\n\r\n\r\n\r\n            <!--<table align=\"center\">-->\r\n");
                WriteLiteral(@"            <!--<tr><td style=""text-align:center; font-size:12px ;""> 49 WEST VIEW ROAD, KAWRAN BAZAR, DHAKA - 1215  </td></tr>
        </table>-->

            <table id=""head_table"" style="" border-top: 1px dashed #000;  width: 100%; color: black;"">
                <tr>
                    <td><strong> Invoice No.: </strong>     ");
#nullable restore
#line 104 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                       Write(Model.SaleList.invoice);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n\r\n                </tr>\r\n            </table>\r\n\r\n        </div>\r\n        <div style=\" text-align: right;\">\r\n            <strong> Date : </strong> ");
#nullable restore
#line 112 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                 Write(Model.SaleList.entry_date.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        </div>\r\n        <div style=\" text-align: right;\">\r\n            <strong> Time : </strong> ");
#nullable restore
#line 115 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                 Write(Model.EntryTime);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        </div>\r\n\r\n        <table id=\"head_table\">\r\n            <tr>\r\n                <td><strong> Name:</strong></td>\r\n                <td>");
#nullable restore
#line 121 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
               Write(Model.User.first_name);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</td>

            </tr>
        </table>



        <table id=""dataTable"" style=""font-size:15px;"">

            <tr>

                <th>Item Name</th>

                <th style=""text-align:right;"">Price</th>
                <th style=""text-align:right;"">Qty.</th>
                <th style=""text-align:right;"">Disc.(%)</th>
                <th style=""text-align:right;"">T.Price</th>


            </tr>

");
#nullable restore
#line 142 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
             foreach (var s in @Model.SaleList.sales_list)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <tr style=\"font-size:16px;\">\r\n                    <td>");
#nullable restore
#line 145 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                   Write(s.product_name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td style=\"text-align:right;\">");
#nullable restore
#line 146 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                             Write(s.mrp_price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td style=\"text-align:right;\">");
#nullable restore
#line 147 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                             Write(s.quantity);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td style=\"text-align:right;\">");
#nullable restore
#line 148 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                             Write(s.discount);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td style=\"text-align:right;\">");
#nullable restore
#line 149 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                             Write(s.total_price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 152 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n            <tr style=\"border-top:2px double; border-bottom:1px dashed;\">\r\n                <td style=\"text-align:left\" colspan=\"4\"><strong>GROSS TOTAL : </strong></td>\r\n\r\n\r\n                <td style=\"text-align:right;\" colspan=\"1\"><strong>");
#nullable restore
#line 159 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                             Write(Model.SaleList.total);

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong></td>\r\n\r\n            </tr>\r\n            <tr style=\"border-top:1px dashed; border-bottom:1px dashed;\">\r\n                <td style=\"text-align:left\" colspan=\"4\"><strong>DISCOUNT(");
#nullable restore
#line 163 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                                    Write(Model.SaleList.discount_p);

#line default
#line hidden
#nullable disable
                WriteLiteral("%) : </strong></td>\r\n\r\n\r\n                <td style=\"text-align:right;\" colspan=\"1\"><strong>");
#nullable restore
#line 166 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                             Write(Model.SaleList.discount);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong></td>

            </tr>
            <tr style=""border-top:1px dashed; border-bottom: 0px;"">
                <td style=""text-align:left; border-bottom: 0px;"" colspan=""4""><strong>GRAND TOTAL : </strong></td>

                <td style=""text-align:right;  border-bottom: 0px;"" colspan=""1""><strong>");
#nullable restore
#line 172 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                                                  Write(Model.SaleList.grand_total);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong></td>

            </tr>
            <tr style=""border-top:1px dashed; border-bottom: 0px;"">
                <td style=""text-align:left; border-bottom: 0px;"" colspan=""4""><strong>PAID: </strong></td>


                <td style=""text-align:right;  border-bottom: 0px;"" colspan=""1""><strong>");
#nullable restore
#line 179 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                                                  Write(Model.SaleList.payment);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong></td>

            </tr>
            <tr style=""border-top:1px dashed; border-bottom: 0px;"">
                <td style=""text-align:left; border-bottom: 0px;"" colspan=""4""><strong>DUE: </strong></td>


                <td style=""text-align:right;  border-bottom: 0px;"" colspan=""1""><strong>");
#nullable restore
#line 186 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                                                  Write(Model.SaleList.due);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</strong></td>

            </tr>
            <tr style=""border-top:1px dashed; border-bottom: 0px;"">
                <td style=""text-align:left; border-bottom: 0px;"" colspan=""4""><strong>PAYMENT METHOD: </strong></td>


                <td style=""text-align:right;  border-bottom: 0px;"" colspan=""1""><strong>");
#nullable restore
#line 193 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                                                  Write(Model.Method.transaction_type);

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong></td>\r\n\r\n            </tr>\r\n        </table>\r\n        <p style=\"font-size:22px ; font-family: IDAutomationHC39M; text-align:center; align-content:center;\">*");
#nullable restore
#line 197 "K:\toufiq\backend\POS\Reports\Receipt.cshtml"
                                                                                                         Write(Model.SaleList.invoice);

#line default
#line hidden
#nullable disable
                WriteLiteral("*</p>\r\n\r\n        <hr style=\"width:100%; border-top:dashed 1px;\" />\r\n\r\n        <br />\r\n    </div>\r\n\r\n");
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
