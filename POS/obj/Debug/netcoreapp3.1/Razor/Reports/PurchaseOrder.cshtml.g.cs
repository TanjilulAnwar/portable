#pragma checksum "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c1e46094126828984272709e31ef3de507bd7743"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Reports_PurchaseOrder), @"mvc.1.0.view", @"/Reports/PurchaseOrder.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1e46094126828984272709e31ef3de507bd7743", @"/Reports/PurchaseOrder.cshtml")]
    public class Reports_PurchaseOrder : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c1e46094126828984272709e31ef3de507bd77432698", async() => {
                WriteLiteral(@"

    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=IBM+Plex+Sans&display=swap"" rel=""stylesheet"">
    <style>
        body {
            font-family: 'IBM Plex Sans', sans-serif;
        }
        ");
                WriteLiteral("@media print {\r\n            h1 {\r\n                page-break-before: always;\r\n            }\r\n        }\r\n\r\n        ");
                WriteLiteral("@font-face {\r\n            font-family: IDAutomationHC39M;\r\n            src: url(");
#nullable restore
#line 19 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                Write(Model.Server);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/IDAutomationHC39M.woff);
        }

        #students {
            font-family: 'IBM Plex Sans', sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #students td, #students th {
                border: 1px solid #000000;
                border-collapse: collapse;

                padding: 8px 8px 8px 8px;
            }

            /*#customers tr:nth-child(even){background-color: #f2f2f2;}*/

            /*#customers tr:hover {background-color: #ddd;}*/

            #students th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                /*  background-color: #373737;
                color: white;*/
                color: #373737;
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
   ");
                WriteLiteral(@"     .header {

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


        #scissors {
            height: 100px; /* image height */
            width: 100%;

            background-image: url('http://i.stack.imgur.com/cXciH.png');
            background-size:20px;
            background-repeat: no-repeat;
            background-position: center;
            position: relative;
            overflow: hidden;
        }

            #scissors:after {
                content: """";
                position: relative;
                top: 50%;
                display: block;
                border-top: 1px dashed black;
                margin-top: -1px;
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c1e46094126828984272709e31ef3de507bd77436497", async() => {
                WriteLiteral(@"

    <div>
        <div style=""overflow:hidden;"">
            <div class=""header"" style=""width:50%; float:left;"">
                <table class=""header"">

                    <thead>
                        <tr style=""text-align:center;"">
                            <th style=""padding: 8px 8px 8px 8px;""><img");
                BeginWriteAttribute("src", " src=\"", 2877, "\"", 2916, 1);
#nullable restore
#line 104 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
WriteAttributeValue("", 2883, Model.Server+Model.Client.logo, 2883, 33, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Image\" /></th>\r\n                            <th style=\" text-align: center;padding: 8px 8px 8px 8px;\">\r\n                                <div style=\"text-align:center;  \">\r\n                                    <h1>");
#nullable restore
#line 107 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                   Write(Model.Client.name);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h1>
                                </div>
                            </th>
                        </tr>

                    </thead>
                </table>
                <table class=""header"">
                    <tr><td style=""text-align:center; font-size:12px ; "">   ");
#nullable restore
#line 115 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                                       Write(Model.Client.address);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 115 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                                                              Write(Model.Client.thana);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 115 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                                                                                   Write(Model.Client.district);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - ");
#nullable restore
#line 115 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                                                                                                            Write(Model.Client.zipcode);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" </td></tr>
                </table>


            </div>
            <div class=""header"" style=""width:50%; float:right; text-align:right;"">

                <h3 style="" border: 2px solid #000000; color: #ffffff; background-color: #000000; text-align: center;"">Purchase Order</h3>
                <div style="" text-align: right;
        margin: 2rem 0rem 0rem 0rem;"">
                    <strong> Date : </strong> ");
#nullable restore
#line 125 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                         Write(Model.Purchase.entry_date.ToString("dd/MM/yy"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </div>\r\n                <div style=\" text-align: right;\r\n        margin: 2rem 0rem 0rem 0rem;\">\r\n                    <strong> Invoice No. : </strong> ");
#nullable restore
#line 129 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                Write(Model.Purchase.invoice);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </div>\r\n                <div style=\" text-align: right;\r\n        margin: 2rem 0rem 0rem 0rem;\">\r\n                    <strong> Supplier Invoice No. : </strong> ");
#nullable restore
#line 133 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                         Write(Model.SupInvoice);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                </div>

            </div>
        </div>


        <div style=""overflow: hidden;"">
            <div style=""width: 50%; float: left; "">
                <table id=""head_table"">


                    <tr>
                        <td><strong> Payment By : </strong></td>
                    </tr>
                    <tr>
                        <td> ");
#nullable restore
#line 149 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                        Write(Model.User.first_name);

#line default
#line hidden
#nullable disable
                WriteLiteral("  ");
#nullable restore
#line 149 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                Write(Model.User.last_name);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n\r\n                    </tr>\r\n                    <tr>\r\n\r\n                        <td>");
#nullable restore
#line 154 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                       Write(Model.User.user_type);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n\r\n                    </tr>\r\n                    <tr>\r\n\r\n                        <td>");
#nullable restore
#line 160 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                       Write(Model.User.phone);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n                    </tr>\r\n                    <tr>\r\n\r\n                        <td><strong>Payment Method : </strong></td>\r\n                        <td> ");
#nullable restore
#line 166 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                        Write(Model.Method);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</td>
                    </tr>
                </table>
            </div>
            <div style=""width: 50%; float: right; text-align:left;"">
                <table id=""head_table"">


                    <tr>
                        <td><strong> Payment To : </strong></td>


                    </tr>
                    <tr>
                        <td>");
#nullable restore
#line 180 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                       Write(Model.Supplier.name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n                    </tr>\r\n                    <tr>\r\n\r\n                        <td>");
#nullable restore
#line 185 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                       Write(Model.Supplier.company);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n                    </tr>\r\n                    <tr>\r\n\r\n                        <td>");
#nullable restore
#line 190 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                       Write(Model.Supplier.address);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 190 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                Write(Model.Supplier.thana);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 190 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                                       Write(Model.Supplier.district);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n                    </tr>\r\n                    <tr>\r\n\r\n                        <td>");
#nullable restore
#line 195 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                       Write(Model.Supplier.mobile);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</td>

                    </tr>

                </table>
            </div>

        </div>
    </div>




    <table id=""students"">
        <thead>
            <tr>

                <th>SL</th>
                <th>Item</th>
                <th>Unit Price</th>
                <th>Quantity</th>
                <th>Total Price</th>

            </tr>
        </thead>
");
#nullable restore
#line 220 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
           int i = 1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 221 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
         foreach (var s in Model.Purchase.purchase_list)
        {


#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 225 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                Write(i++);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 226 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
               Write(s.product_name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n                <td>");
#nullable restore
#line 228 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
               Write(s.unit_price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 229 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
               Write(s.quantity);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 230 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
               Write(s.total_price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n            </tr>\r\n");
#nullable restore
#line 233 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"

        }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n        <tr>\r\n\r\n            <td colspan=\"4\" style=\"border: 0px ; text-align:end;\">Net Total</td>\r\n            <td colspan=\"1\"> ");
#nullable restore
#line 240 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                        Write(Model.Purchase.total);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <td rowspan=\"2\" colspan=\"3\" style=\"font-size:15pt ; font-family: IDAutomationHC39M; text-align:left; align-content:center; border:0px;\">*");
#nullable restore
#line 244 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                                                                                                                Write(Model.Purchase.invoice);

#line default
#line hidden
#nullable disable
                WriteLiteral("*</td>\r\n            <td style=\"border: 0px ; text-align:end;\">Discount(");
#nullable restore
#line 245 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                                                          Write(Model.Purchase.discount_p);

#line default
#line hidden
#nullable disable
                WriteLiteral("%)</td>\r\n            <td>");
#nullable restore
#line 246 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
           Write(Model.Purchase.discount);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <td style=\"border: 0px ; text-align:end;\">Discounted Total</td>\r\n            <td>");
#nullable restore
#line 251 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
           Write(Model.Purchase.grand_total);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <td   colspan=\"4\"  style=\"border: 0px ; text-align:end;\">Paid</td>\r\n            <td  colspan=\"1\" >");
#nullable restore
#line 256 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                         Write(Model.Purchase.payment);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <td  colspan=\"4\"  style=\"border: 0px ; text-align:end;\">Due</td>\r\n            <td  colspan=\"1\" >");
#nullable restore
#line 261 "K:\toufiq\backend\POS\Reports\PurchaseOrder.cshtml"
                         Write(Model.Purchase.due);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n        </tr>\r\n\r\n    </table>\r\n\r\n\r\n    <div id=\"scissors\"></div>\r\n\r\n\r\n\r\n");
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
