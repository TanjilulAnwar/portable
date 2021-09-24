#pragma checksum "K:\toufiq\backend\POS\Reports\Ledger.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca52fabe050119c1a033cd965f02d118390b3097"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Reports_Ledger), @"mvc.1.0.view", @"/Reports/Ledger.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca52fabe050119c1a033cd965f02d118390b3097", @"/Reports/Ledger.cshtml")]
    public class Reports_Ledger : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ca52fabe050119c1a033cd965f02d118390b30972663", async() => {
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
          width: 90%;
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

        .c");
                WriteLiteral(@"enter {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ca52fabe050119c1a033cd965f02d118390b30975439", async() => {
                WriteLiteral("\r\n\r\n    <div>\r\n        <div class=\"center\">\r\n            <table class=\"center\">\r\n\r\n                <thead>\r\n                    <tr style=\"text-align:center;\">\r\n                        <td style=\"padding: 8px 8px 8px 8px;\"><img");
                BeginWriteAttribute("src", " src=\"", 2040, "\"", 2081, 1);
#nullable restore
#line 75 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
WriteAttributeValue("", 2046, Model.Server + Model.Client.logo, 2046, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Image\" /></td>\r\n                        <td style=\" text-align: center;padding: 8px 8px 8px 8px;\">\r\n                            <div style=\"text-align:center;  \">\r\n                                <h1>");
#nullable restore
#line 78 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
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
#line 85 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                Write(Model.Client.address);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 85 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                                       Write(Model.Client.thana);

#line default
#line hidden
#nullable disable
                WriteLiteral(", ");
#nullable restore
#line 85 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                                                            Write(Model.Client.district);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - ");
#nullable restore
#line 85 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                                                                                     Write(Model.Client.zipcode);

#line default
#line hidden
#nullable disable
                WriteLiteral("  </td></tr>\r\n            </table>\r\n            <table class=\"center\" style=\"margin-top:2rem;\">\r\n                <tr>\r\n                    <td style=\"text-align:center; font-size:20px ;\">Ledger Report From <strong>");
#nullable restore
#line 89 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                                          Write(Model.StartDate.ToString("dd/MM/yy"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong> to <strong>");
#nullable restore
#line 89 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                                                                                                    Write(Model.EndDate.ToString("dd/MM/yy"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong> </td>\r\n\r\n\r\n                </tr>\r\n            </table>\r\n        </div>\r\n        <div style=\" text-align: right;\r\n        margin: 2rem 0rem 0rem 0rem;\">\r\n            <strong> Date : </strong> ");
#nullable restore
#line 97 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
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
                <td>Opening Balance :  <strong>  ");
#nullable restore
#line 110 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                            Write(Model.Result.OpeningBalance);

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong> </td>\r\n\r\n            </tr>\r\n\r\n        </table>\r\n\r\n\r\n\r\n\r\n");
                WriteLiteral("\r\n\r\n");
#nullable restore
#line 122 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
           int i = 1; int c = 0; 

#line default
#line hidden
#nullable disable
                WriteLiteral("    \r\n");
#nullable restore
#line 124 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
         foreach (var la in Model.lax)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <table id=\"dataTable\" width=\"100%\">\r\n                <tr> <td>");
#nullable restore
#line 127 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                    Write(la.account_head_name);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</td></tr>
            </table>
            <table id=""dataTable"" style=""margin-left:50px; margin-bottom:2rem;"">

                <thead>
                    <tr>

                        <th>SL </th>
                        <th>Date</th>
                        <th>Action</th>
");
                WriteLiteral(@"                        <th>Invoice</th>
                        <th>Supplier</th>
                        <th>Customer</th>
                        <th style="" text-align: right;"">Debit</th>
                        <th style="" text-align: right;"">Credit</th>
                    </tr>
                </thead>
");
#nullable restore
#line 145 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                 foreach (var ph in la.ledgerList)
                {



#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr>\r\n\r\n                <td>");
#nullable restore
#line 151 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                Write(i++);

#line default
#line hidden
#nullable disable
                WriteLiteral(". </td>\r\n                <td>");
#nullable restore
#line 152 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
               Write(ph.entry_date.ToString("dd/MM/yy"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n");
                WriteLiteral("                <td>");
#nullable restore
#line 154 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
               Write(ph.label);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                <td>");
#nullable restore
#line 155 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
               Write(ph.invoice);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                <td>");
#nullable restore
#line 156 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
               Write(ph.supplier_name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 157 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
               Write(ph.customer_name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n");
#nullable restore
#line 159 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                 if (ph.dr_total == 0)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <td style=\" text-align: right;\"></td>\r\n");
#nullable restore
#line 162 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <td style=\" text-align: right;\">");
#nullable restore
#line 165 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                               Write(ph.dr_total);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n");
#nullable restore
#line 166 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 167 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                 if (ph.cr_total == 0)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <td style=\" text-align: right;\"></td>\r\n");
#nullable restore
#line 170 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <td style=\" text-align: right;\">");
#nullable restore
#line 173 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                               Write(ph.cr_total);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n");
#nullable restore
#line 174 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n            </tr>\r\n");
#nullable restore
#line 177 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"



                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                <tr>
                    <td colspan=5 style=""border-bottom: 0px; text-align: right;""></td>
                    <td style=""border-bottom: 0px; text-align: right;""><strong>SUB TOTAL: </strong> </td>

                    <td style=""border-bottom: 0px; text-align: right;"">");
#nullable restore
#line 186 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                  Write(la.sub_total_debit);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                    <td style=\"border-bottom: 0px; text-align: right;\">");
#nullable restore
#line 187 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                  Write(la.sub_total_credit);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n                </tr>\r\n");
#nullable restore
#line 189 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                   c++;

#line default
#line hidden
#nullable disable
#nullable restore
#line 190 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                 if (Model.Count == c)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                    <tr>
                        <td colspan=5 style=""border-bottom: 0px; text-align: right; ""></td>
                        <td style=""border-bottom: 0px; text-align: right; ""><strong>GRAND TOTAL: </strong> </td>
                        <td style=""font-size:18px; border-bottom: 0px; text-align: right; ""> <strong><u>");
#nullable restore
#line 195 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                                                   Write(Model.Result.Debit);

#line default
#line hidden
#nullable disable
                WriteLiteral("</u></strong></td>\r\n                        <td style=\"font-size:18px; border-bottom: 0px; text-align: right; \"><strong><u>");
#nullable restore
#line 196 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                                                                                                  Write(Model.Result.Credit);

#line default
#line hidden
#nullable disable
                WriteLiteral("</u> </strong></td>\r\n                    </tr>\r\n");
#nullable restore
#line 198 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n\r\n\r\n            </table>\r\n");
#nullable restore
#line 204 "K:\toufiq\backend\POS\Reports\Ledger.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n\r\n\r\n");
                WriteLiteral("\r\n\r\n\r\n    </div>\r\n\r\n");
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
