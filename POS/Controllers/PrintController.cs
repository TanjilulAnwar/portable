using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using POS.ViewModels;
using Wkhtmltopdf.NetCore;

namespace POS.Controllers
{
    public class PrintController : BaseController
    {

        private readonly IGeneratePdf _generatePdf;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PrintController(IUnitOfWork unitOfWork, IMapper mapper, IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            return View();
        }

        
   
        public Client getClient(string client_code)
        {
            Client client = _unitOfWork.Client.GetFirstOrDefault(u => u.code == client_code);
            return client;
        }

        [HttpGet]
        [Route("~/print/Suppliers")]
        public async Task<IActionResult> SuppliersList()
        {
            try
            {


                string client_code = getClient();
                string trade_code = getTrade();
                List<Supplier> suppliers = _unitOfWork.Supplier.GetAll(u => u.client_code == client_code && u.trade_code == trade_code).ToList();
                var options = new ConvertOptions
                {
               
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER+"/footer.html",
                  
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), SuppliersList = suppliers, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/SupplierList.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);

            }
            catch
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" });
            }

        }








        [HttpGet]
        [Route("~/print/StockPosition")]
        public async Task<IActionResult> StockPosition()
        {
            try
            {
                string client_code = getClient();
                string trade_code = getTrade();

                List<Product> products = _unitOfWork.Product.GetAll(u => u.client_code == client_code && u.trade_code == trade_code).OrderByDescending(u=>u.quantity).ToList();

                var options = new ConvertOptions
                {
                    
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);

                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), Time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local).ToString("hh:mm tt"), productList = products, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/StockPosition.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);

            }
            catch(Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" + e });
            }



        }

















        [Route("~/Category/Print")]
        public async Task<IActionResult> CategoryPrint()
        {
            try
            {

                string client_code =getClient();
                string trade_code = getTrade();
                IEnumerable<Category> list = await _unitOfWork.Category.GetAllAsync(u =>u.client_code == client_code && u.trade_code == trade_code);
                List<CategoryVM> categoryVMs = new List<CategoryVM>();
                foreach (Category cat in list)
                {
                    CategoryVM categoryVM = new CategoryVM();
                    categoryVM = _mapper.Map<CategoryVM>(cat);
                    var SubCategories = _unitOfWork.SubCategory.GetAll(u => u.trade_code == trade_code && u.category_code == cat.code);
                    categoryVM.subcategories = from s in SubCategories
                                               select (s.name).ToString();
                    categoryVMs.Add(categoryVM);
                }
          

                var options = new ConvertOptions
                {
                   
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
             
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 10,
                        Left = 10,
                        Right = 10,
                        Bottom = 10
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), CategoryList = categoryVMs, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/CategoryList.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);

            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }





        [HttpGet]
        [Route("~/print/Customer")]
        public async Task<IActionResult> CustomerList()
        {
            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                //string client_code = getClient();
                //string trade_code = getTrade();
                List<Customer> customers = _unitOfWork.Customer.GetAll(u => u.client_code == client_code && u.trade_code == trade_code).ToList();

                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                    //  HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), CustomerList = customers, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/CustomerList.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);

            }
            catch(Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" + e});
            }



        }
        public class ProductList
        {
            public string category { get; set; }
            public string category_code { get; set; }

            public List<Product> productsL { get; set; }
        }





        [HttpGet]
        [Route("~/print/Products")]
        public async Task<IActionResult> Product_List()
        {
            try
            {


                string client_code = getClient();
                string trade_code =getTrade();

                List<Product> products = _unitOfWork.Product.GetAll(u => u.client_code == client_code && u.trade_code == trade_code).OrderBy(u=>u.product_name).ToList();
                List<ProductList> groupedList = products.GroupBy(i => i.category_code)
  .Select(j => new ProductList()
  {
      category = j.First().category,
      category_code = j.First().category_code,
      productsL = j.Select(f => new Product()
      {
          product_name = f.product_name,
          product_code = f.product_code,
          unit_price = f.unit_price,
          mrp_price = f.mrp_price,
          quantity = f.quantity,
          description = f.description
      }).ToList()
  })
  .ToList();
                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                  //  HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER+"/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), products = groupedList, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/ProductList.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);

            }
            catch(Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" , j= e.Message});
            }



        }







        [HttpGet]
        [Route("~/print/Manufacturers")]
        public async Task<IActionResult> ManufacturerList()
        {


            try {

                string client_code = getClient();
                string trade_code = getTrade();
                List<Manufacturer> manufacturers = _unitOfWork.Manufacturer.GetAll(u => u.client_code == client_code && u.trade_code == trade_code).ToList();

                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
             //       HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), ManufacturerList = manufacturers, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/ManufacturerList.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" });
            }
           

        }













        public class LedgerAccount
        {

            public string account_head_name { get; set; }
            public List<LedgerO> ledgerList { get; set; }

            public double sub_total_credit { get; set; }
            public double sub_total_debit { get; set; }
        }

            public class LedgerO
        {
            
            public string account { get; set; }
            public DateTime entry_date { get; set; }
            public string invoice { get; set; }
            public string customer_name { get; set; }
            public string supplier_name { get; set; }
            public string label { get; set; }
            public double dr_total { get; set; }
            public double cr_total { get; set; }

        }

        public class LedgerResult
        {
           
            public double OpeningBalance { get; set; }
            public double Debit { get; set; }
            public double Credit { get; set; }
        }

        [HttpGet]
        [Route("~/print/cashflow")]
        public async Task<IActionResult> cashflow(DateTime start_date, DateTime end_date)
        {
            try
            {

                string client_code = "01";//getClient();
                string trade_code = "0101";//getTrade();
                var parameter = new DynamicParameters();
                parameter.Add("ClientCode", client_code);
                parameter.Add("TradeCode", trade_code);
                parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));

                List<LedgerO> LedgerList = _unitOfWork.SP_Call.List<LedgerO>("cash_flow", parameter).ToList();

              
                if (LedgerList == null)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                if (LedgerList.Count == 0)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                List<string> listo = LedgerList.Select(x => x.account).Distinct().ToList();
                List<LedgerAccount> ledgerAccounts = new List<LedgerAccount>();
                List<AccountsHead> listX = _unitOfWork.AccountsHead.GetAll(u => u.client_code == client_code).ToList();
                foreach (string acc in listo)
                {
                    LedgerAccount ledgerAccount = new LedgerAccount();
                    ledgerAccount.account_head_name = acc;
                    ledgerAccount.ledgerList = LedgerList.Where(u => u.account == acc).ToList();
                    ledgerAccount.sub_total_debit = Math.Round(ledgerAccount.ledgerList.Sum(u => u.dr_total), 2);
                    ledgerAccount.sub_total_credit = Math.Round(ledgerAccount.ledgerList.Sum(u => u.cr_total), 2);
                    ledgerAccounts.Add(ledgerAccount);
                }

                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                    // HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",

                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), Ledger = LedgerList, StartDate = start_date, EndDate = end_date,  lax = ledgerAccounts, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/Cashflow.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" + e });
            }

        }






















        [HttpGet]
        [Route("~/print/Ledger")]
        public async Task<IActionResult> LedgerList(DateTime start_date, DateTime end_date)
        {
            try {

                string client_code = getClient();
                string trade_code = getTrade();
                var parameter = new DynamicParameters();
                parameter.Add("ClientCode", client_code);
                parameter.Add("TradeCode", trade_code);
                parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));

                List<LedgerO> LedgerList = _unitOfWork.SP_Call.List<LedgerO>("ledger_report", parameter).ToList();
           
                LedgerResult ledgerResult = _unitOfWork.SP_Call.OneRecord<LedgerResult>("ledger_total", parameter);
                if (LedgerList == null)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                if (LedgerList.Count == 0)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                List<string> listo = LedgerList.Select(x => x.account).Distinct().ToList();
                List<LedgerAccount> ledgerAccounts = new List<LedgerAccount>();
                List<AccountsHead> listX = _unitOfWork.AccountsHead.GetAll(u => u.client_code == client_code).ToList();
                foreach(string acc in listo)
                {
                    LedgerAccount ledgerAccount = new LedgerAccount();
                    ledgerAccount.account_head_name =acc;
                    ledgerAccount.ledgerList = LedgerList.Where(u => u.account == acc).ToList();
                    ledgerAccount.sub_total_debit = Math.Round(ledgerAccount.ledgerList.Sum(u => u.dr_total),2);
                    ledgerAccount.sub_total_credit = Math.Round(ledgerAccount.ledgerList.Sum(u => u.cr_total), 2);
                    ledgerAccounts.Add(ledgerAccount);
                }

                /////////////////////////////////
                Client client = getClient(client_code);

                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };
                var options = new ConvertOptions
                {
                 //   HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",

                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), Ledger = LedgerList, StartDate = start_date, EndDate = end_date, Result = ledgerResult, lax = ledgerAccounts, Count = ledgerAccounts.Count(), Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/Ledger.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch(Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!"+ e });
            }
            
        }


        [HttpGet]
        [Route("~/print/supplier/Ledger")]
        public async Task<IActionResult> LedgerList(DateTime start_date, DateTime end_date,string supplier_code)
        {
            try
            {

                string client_code = getClient();
                string trade_code =  getTrade();
                Supplier supplier = _unitOfWork.Supplier.GetFirstOrDefault(u => u.code == supplier_code && u.client_code == client_code && u.trade_code == trade_code);
                if(supplier == null)
                {
                    return Json(new { success = false, message = "No supplier found" });
                }
                var parameter = new DynamicParameters();
                parameter.Add("ClientCode", client_code);
                parameter.Add("TradeCode", trade_code);
                parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("SupplierCode", supplier_code);
                List<LedgerO> LedgerList = _unitOfWork.SP_Call.List<LedgerO>("supplier_ledger_report", parameter).ToList();

                LedgerResult ledgerResult = _unitOfWork.SP_Call.OneRecord<LedgerResult>("supplier_ledger_total", parameter);
                if (LedgerList == null)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                if (LedgerList.Count == 0)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                List<string> listo = LedgerList.Select(x => x.account).Distinct().ToList();
                List<LedgerAccount> ledgerAccounts = new List<LedgerAccount>();
                List<AccountsHead> listX = _unitOfWork.AccountsHead.GetAll(u => u.client_code == client_code).ToList();
                foreach (string acc in listo)
                {
                    LedgerAccount ledgerAccount = new LedgerAccount();
                    ledgerAccount.account_head_name = acc;
                    ledgerAccount.ledgerList = LedgerList.Where(u => u.account == acc).ToList();
                    ledgerAccount.sub_total_debit = Math.Round(ledgerAccount.ledgerList.Sum(u => u.dr_total), 2);
                    ledgerAccount.sub_total_credit = Math.Round(ledgerAccount.ledgerList.Sum(u => u.cr_total), 2);
                    ledgerAccounts.Add(ledgerAccount);
                }

                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                    // HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",

                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), Ledger = LedgerList, StartDate = start_date, EndDate = end_date, Result = ledgerResult, lax = ledgerAccounts, Count = ledgerAccounts.Count(),Supplier = supplier.name+" ( "+supplier_code+" )", Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/SupplierLedger.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" + e });
            }

        }

















        [HttpGet]
        [Route("~/Sale/history/print")]
        public async Task<IActionResult> SaleHistoryByDate(DateTime start_date, DateTime end_date, string customer_code)
        {

            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                List<ProductEventInfo> peList = new List<ProductEventInfo>();
                if (customer_code != "undefined" && customer_code != "null" && customer_code != null)
                {
                    peList = _unitOfWork.ProductEventInfo.GetAll(
                   u => (u.entry_date >= start_date && u.entry_date <= end_date)
                   && u.ac_head_id == SD.SALES_REVENUE
                    && u.status == SD.PAID
                   && u.client_code == client_code
                   && u.trade_code == trade_code
                   && u.customer_code == customer_code
                   ).ToList();
                }

                else
                {
                    peList = _unitOfWork.ProductEventInfo.GetAll(
                   u => (u.entry_date >= start_date && u.entry_date <= end_date)
                   && u.ac_head_id == SD.SALES_REVENUE
                    && u.status == SD.PAID
                   && u.client_code == client_code
                   && u.trade_code == trade_code
                   ).ToList();
                }

                if (peList.Count == 0)
                {
                    return Json(new { success = false, message = "No SALE was made in this day!" });
                }
                List<SaleVM> saleVMList = new List<SaleVM>();
                foreach (ProductEventInfo pe in peList)
                {
                    ProductEventInfo peX = _unitOfWork.ProductEventInfo.GetFirstOrDefault(u =>
                 u.trx_info == SD.DEBIT
                 && u.invoice == pe.invoice
                 && u.status == SD.RECIEVED
                  );
                    ProductEventInfo peDues = _unitOfWork.ProductEventInfo.GetFirstOrDefault(u =>
             u.transaction_id == pe.transaction_id
             && u.ac_head_id == SD.AC_RECEIVABLE
             && u.status == SD.DUE
             );
                    SaleVM sv = new SaleVM();
                    sv.transaction_id = pe.transaction_id;
                    sv.invoice = pe.invoice;
                    sv.payment = peX.dr_total;
                    sv.total = pe.grand_total;
                    sv.discount = pe.cr_discount;
                    sv.discount_p = pe.cr_discount_percent;
                    sv.grand_total = sv.total - sv.discount;
                    sv.due = 0.0;
                    if (peDues != null)
                    {
                        sv.due = peDues.dr_total;
                    }
                    sv.payment_type = peX.ac_head_name;
                    sv.entry_date = pe.entry_date;
                    sv.entry_time = pe.entry_time;
                    sv.customer_code = pe.customer_code;
                    sv.customer_name = pe.customer_name;
                    List<ProductStockOut> prodstockout = _unitOfWork.ProductStockOut.GetAll(u => u.client_code == client_code && u.transaction_id == pe.transaction_id && u.trade_code == trade_code).ToList();
                    if (prodstockout.Count() == 0)
                    {
                        saleVMList.Add(sv);
                        continue;

                    }
                    sv.sales_list = new List<ProductObject>();
                    sv.sales_list = (from p in prodstockout
                                     select (new ProductObject
                                     {
                                         product_code = p.product_code,
                                         product_name = p.product_name,
                                         mrp_price = p.mrp_price,
                                         unit_price = p.unit_price,
                                         discount = p.discount_percentage,
                                         total_price = p.total_price_deducted,
                                         expire_date = p.expire_date,
                                         quantity = p.quantity
                                     })).ToList();


                    saleVMList.Add(sv);

                }


                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), SaleList = saleVMList , Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/SaleHistory.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);
            }

            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }


















        [HttpGet]
        [Route("~/Purchase/history/print")]
        public async Task<IActionResult> PurchaseHistoryByDate(DateTime start_date, DateTime end_date, string supplier_code)
        {

            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                List<ProductEventInfo> peList = new List<ProductEventInfo>();

                if (supplier_code != "undefined" && supplier_code != "null" && supplier_code != null)
                {
                    peList = _unitOfWork.ProductEventInfo.GetAll(
             u => (u.entry_date >= start_date && u.entry_date <= end_date)
             && u.ac_head_id == SD.INVENTORY_COST
             && u.status == SD.PAID
             && u.client_code == client_code
             && u.trade_code == trade_code
             && u.supplier_code == supplier_code
             ).ToList();
                }

                else
                {
                    peList = _unitOfWork.ProductEventInfo.GetAll(
      u => (u.entry_date >= start_date && u.entry_date <= end_date)
      && u.ac_head_id == SD.INVENTORY_COST
      && u.status == SD.PAID
      && u.client_code == client_code
      && u.trade_code == trade_code
      ).ToList();
                }

                if (peList.Count == 0)
                {
                    return Json(new { success = false, message = "No Purchase was made in this day!" });
                }
                List<PurchaseVM> purchaseVMList = new List<PurchaseVM>();
                foreach (ProductEventInfo pe in peList)
                {


                    PurchaseVM pv = new PurchaseVM();
                    pv.transaction_id = pe.transaction_id;
                    pv.invoice = pe.invoice;
                    ProductEventInfo peX = _unitOfWork.ProductEventInfo.GetFirstOrDefault(u =>
                   u.trx_info == SD.CREDIT
                   && u.invoice == pe.invoice
                   && u.status == SD.PAID
                    );
                    ProductEventInfo peDues = _unitOfWork.ProductEventInfo.GetFirstOrDefault(u =>
             u.transaction_id == pe.transaction_id
             && u.ac_head_id == SD.AC_PAYABLE
             && u.status == SD.DUE
             );
                    //  pv.total = pe.dr_total;
                    //  pv.payment = pe.dr_amount;
                    pv.discount_p = pe.dr_discount_percent;
                    pv.discount = pe.dr_discount;
                    pv.entry_date = pe.entry_date;
                    pv.supplier_code = pe.supplier_code;
                    pv.supplier_name = pe.supplier_name;
                    List<ProductStockIn> prodstockin = _unitOfWork.ProductStockIn.GetAll(u => u.client_code == client_code && u.client_code == client_code && u.transaction_id == pe.transaction_id).ToList();
                    if (prodstockin.Count() == 0)
                    {
                        purchaseVMList.Add(pv);
                        continue;

                    }
                    pv.purchase_list = new List<ProductObject>();
                    pv.purchase_list = (from p in prodstockin
                                        select (new ProductObject
                                        {
                                            product_code = p.product_code,
                                            product_name = p.product_name,
                                            mrp_price = p.mrp_price,
                                            unit_price = p.unit_price,
                                            expire_date = p.expire_date,
                                            total_price = p.total_price,
                                            quantity = p.quantity
                                        })).ToList();
                    pv.total = pv.purchase_list.Sum(u => u.total_price);
                    pv.payment = peX.cr_total;
                    pv.payment_type = peX.ac_head_name;
                    pv.grand_total = pv.total - pv.discount;
                    pv.due = 0;
                    if (peDues != null)
                    {
                        pv.due = peDues.cr_total;
                    }
                    purchaseVMList.Add(pv);


                }

                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                    // HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",

                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), PurchaseList = purchaseVMList, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/PurchaseHistory.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);

            }

            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }




























        [HttpGet]
        [Route("~/print/Ledger/head")]
        public async Task<IActionResult> LedgerListByHead(DateTime start_date, DateTime end_date, string ac_head_id)

        {
            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                var parameter = new DynamicParameters();
                parameter.Add("ClientCode", client_code);
                parameter.Add("TradeCode", trade_code);
                parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));




                List<LedgerO> LedgerList = _unitOfWork.SP_Call.List<LedgerO>("ledger_report", parameter).ToList();

                LedgerResult ledgerResult = _unitOfWork.SP_Call.OneRecord<LedgerResult>("ledger_total", parameter);
                if (LedgerList == null)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                if (LedgerList.Count == 0)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }


                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                    HeaderHtml = SD.SERVER + "/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footerTwo.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), Ledger = LedgerList, StartDate = start_date, EndDate = end_date, Result = ledgerResult, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/Ledger.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" + e });
            }

        }



        public class PurchaseDetail {
 

            public DateTime entry_date { get; set; }
            public string product { get; set; }
            public string manufacturer { get; set; }
            public double quantity { get; set; }
            public double unit_price { get; set; }
            public double total { get; set; }



        }


        public class SalesPurchase
        {


            public DateTime entry_date { get; set; }

            public double purchase { get; set; }
            public double sales { get; set; }
      


        }

        public class StockReport
        {


            public DateTime entry_date { get; set; }
            public string product { get; set; }
            public string manufacturer { get; set; }
            public double opening_stock { get; set; }
            public double stock_in { get; set; }
            public double stock_out { get; set; }
            public double closing_stock { get; set; }
            public double unit_price { get; set; }
            public double mrp_price { get; set; }



        }
        public class SaleDetail
        {

            public DateTime entry_date { get; set; }
            public string product { get; set; }
            public string customer_name { get; set; }
            public string manufacturer { get; set; }
            public double quantity { get; set; }
            public double mrp { get; set; }
            public double discount { get; set; }
            public double total { get; set; }


        }


        [HttpGet]
        [Route("~/print/DetailReportPurchase")]
        public async Task<IActionResult> DetailReportPurchase(DateTime start_date, DateTime end_date, string supplier_code)
        {
            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                if (supplier_code == null)
                {
                    return Json(new { success = false, message = "Supplier needs to be specified!" });
                }
                Supplier supplier = _unitOfWork.Supplier.GetFirstOrDefault(u => u.code == supplier_code && u.client_code == client_code && u.trade_code== trade_code);
                if(supplier == null)
                {
                    return Json(new { success = false, message = "No Supplier found with this code!" });
                }

                var parameter = new DynamicParameters();
                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("SupplierCode", supplier_code);



                List<PurchaseDetail> PurchaseList = _unitOfWork.SP_Call.List<PurchaseDetail>("details_purchase", parameter).ToList();
                if (PurchaseList == null)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                if (PurchaseList.Count == 0)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }

                double grand_total = PurchaseList.Sum(u => u.total);
                double q_total = PurchaseList.Sum(u => u.quantity);
                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                  //  HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml =SD.SERVER+ "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), SupplierInfo = supplier ,PurDetail = PurchaseList, StartDate = start_date, EndDate = end_date, GrandTotal = grand_total, Quantity_total = q_total, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/PurchaseDetails.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch(Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" + e});
            }


        }





        [HttpGet]
        [Route("~/print/DetailReportSales")]
        public async Task<IActionResult> DetailReportSales(DateTime start_date, DateTime end_date)
        {
            try
            {

                string client_code =  getClient();
                string trade_code = getTrade();


                var parameter = new DynamicParameters();
                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));




                List<SaleDetail> SaleList = _unitOfWork.SP_Call.List<SaleDetail>("details_sales", parameter).ToList();
                if (SaleList == null)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                if (SaleList.Count == 0)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                double grand_total = SaleList.Sum(u => u.total);
                double q_total = SaleList.Sum(u => u.quantity);

                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                   // HeaderHtml = SD.SERVER+ "/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER+ "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), SaleDetail = SaleList, StartDate = start_date, EndDate = end_date, GrandTotal = grand_total , Quantity_total =q_total, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/SaleDetails.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!", ex =e.Message });
            }


        }



        public class AcReport
        {
            public string ac_group_name { get; set; }
            public string ac_group_id { get; set; }
            public List<AccountsHead> AcHeadList { get; set; }
        }

        [HttpGet]
        [Route("~/print/AccountGroups")]
        public async Task<IActionResult> AccountGroups()
        {
            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                List<AccountsHead> acHeads = _unitOfWork.AccountsHead.GetAll(u=>u.client_code == client_code && u.client_code == client_code && u.trade_code == trade_code).ToList();
                List<AcReport> groupedList = acHeads.GroupBy(i => i.ac_group_id)
    .Select(j => new AcReport()
    {
        ac_group_id = j.First().ac_group_id,
        ac_group_name = j.First().ac_group_name,
        AcHeadList = j.Select(f => new AccountsHead()
        {
            ac_head_name = f.ac_head_name,
            ac_head_id = f.ac_head_id,
            ac_type = f.ac_type,
            description = f.description
        }).ToList()
    })
    .ToList();



                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                 //   HeaderHtml = SD.SERVER + "/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);

        

                  var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"),AcGroups = groupedList, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/AccountHeadReport.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!", ex = e.Message });
            }


        }

        public class AcNames
        {
            public string ac_name { get; set; }
            public string ac_name_id { get; set; }
            public string description { get; set; }
        }

        public class AcHeads
        {
            public string ac_head_name { get; set; }
            public string ac_head_id { get; set; }
            public string description { get; set; }
            public List<AcNames> AcNameList { get; set; }
        }



        public class AcGroups
        {
            public string ac_group_name { get; set; }
            public string ac_group_id { get; set; }
            public List<AcHeads> AcHeadList { get; set; }
        }

        [HttpGet]
        [Route("~/print/AccountNames")]
        public async Task<IActionResult> AccountNames()
        {
            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                List<AccountsHead> acHeadsM = _unitOfWork.AccountsHead.GetAll(u => u.main_sub == "M" && u.client_code == client_code && u.trade_code == trade_code).ToList();
                List<AccountsHead> acHeadsS = _unitOfWork.AccountsHead.GetAll(u => u.main_sub == "S" && u.client_code == client_code && u.trade_code == trade_code).ToList();
                List<AcGroups> groupedList = acHeadsM.GroupBy(i => i.ac_group_id)
               .Select(j => new AcGroups()
        {
        ac_group_id = j.First().ac_group_id,
        ac_group_name = j.First().ac_group_name,
        AcHeadList = j.Select(f => new AcHeads()
        {
            ac_head_name = f.ac_head_name,
            ac_head_id = f.ac_head_id,
            description = f.description,
            AcNameList = (from acNames in acHeadsS where acNames.ac_name_head_id == f.ac_head_id select(new AcNames{ac_name =  acNames.ac_head_name ,ac_name_id= acNames.ac_head_id, description = acNames.description })).ToList(),

        }).ToList()
    })
    .ToList();



                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                  //  HeaderHtml = SD.SERVER + "/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), AcGroups = groupedList, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/AccountNameReport.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!", ex = e.Message });
            }


        }








        [HttpGet]
        [Route("~/print/Inventory")]
        public async Task<IActionResult> Inventory(DateTime start_date, DateTime end_date)
        {
            try
            {

                string client_code =  getClient();
                string trade_code = getTrade();


                var parameter = new DynamicParameters();
                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));


                List<StockReport> inventory = _unitOfWork.SP_Call.List<StockReport>("stock_report", parameter).ToList();
                if (inventory == null)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                if (inventory.Count == 0)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }


                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                  //  HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), inventory = inventory, StartDate = start_date, EndDate = end_date, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/StockReport.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!", ex = e.Message });
            }


        }

        [HttpGet]
        [Route("~/print/SalesPurchaseSummary")]
        public async Task<IActionResult> SalesPurchaseSummary(DateTime start_date, DateTime end_date)
        {
            try
            {

                string client_code = getClient();
                string trade_code = getTrade();


                var parameter = new DynamicParameters();
                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
                parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));




                List<SalesPurchase> salpur = _unitOfWork.SP_Call.List<SalesPurchase>("sales_purchase_report", parameter).ToList();
                if (salpur == null)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }
                if (salpur.Count == 0)
                {
                    return Json(new { success = false, message = "No entry found during this period" });

                }


                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                  //  HeaderHtml = SD.SERVER + "/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 15,
                        Left = 10,
                        Right = 10,
                        Bottom = 15
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), salpur = salpur, StartDate = start_date, EndDate = end_date, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/SalePurchase.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!", ex = e.Message });
            }


        }





        [HttpGet]
        [Route("~/print/Receipt")]
        public async Task<IActionResult> Reciept(string invoice)
        {
            try
            {

                string client_code = getClient();
                string user_id = GetUserId();
                string trade_code = getTrade();


                //string client_code = getClient();
                //string user_id = GetUserId();
                //string trade_code = getTrade();


                List<ProductEventInfo> peList = _unitOfWork.ProductEventInfo.GetAll(
                    u => u.invoice == invoice
                    && u.ac_head_id == SD.SALES_REVENUE
                    && u.status == SD.PAID
                    && u.client_code == client_code
                    && u.trade_code == trade_code
                    ).ToList();


                ProductEventInfo payment = _unitOfWork.ProductEventInfo.GetFirstOrDefault(
                  u => u.invoice == invoice
                  && u.ac_head_id != SD.SALES_REVENUE
                    && u.status == SD.RECIEVED
                 && u.client_code == client_code
                 && u.trade_code == trade_code);


                if (peList.Count == 0)
                {
                    return Json(new { success = false, message = "No SALE found with this invoice No.!" });
                }
                SaleVM sv = new SaleVM();

                foreach (ProductEventInfo pe in peList)
                {
                    user_id = pe.user_id;
                    sv.transaction_id = pe.transaction_id;
                    sv.invoice = pe.invoice;
                    sv.payment = payment.dr_total;
                    sv.total = pe.grand_total;
                    
                    sv.discount = pe.cr_discount;
                    sv.discount_p = pe.cr_discount_percent;
                    sv.grand_total = pe.cr_total;

                    sv.due =Math.Round(pe.cr_total - payment.dr_total,2);
                    sv.entry_date = pe.entry_date;
                    sv.entry_time = pe.entry_time;
                    sv.customer_code = pe.customer_code;
                    sv.customer_name = pe.customer_name;
                    List<ProductStockOut> prodstockout = _unitOfWork.ProductStockOut.GetAll(u => u.client_code == client_code && u.transaction_id == pe.transaction_id && u.trade_code == trade_code).ToList();
                    if (prodstockout.Count() == 0)
                    {
                        continue;

                    }
                    sv.sales_list = new List<ProductObject>();
                    sv.sales_list = (from p in prodstockout
                                     select (new ProductObject
                                     {
                                         product_code = p.product_code,
                                         product_name = p.product_name.Length > 30 ? p.product_name.Substring(0, 30) + "..." : p.product_name,
                                         mrp_price = p.mrp_price,
                                         unit_price = p.unit_price,
                                         discount = p.discount_percentage,
                                         total_price = p.total_price_deducted,
                                         expire_date = p.expire_date,
                                         quantity = p.quantity
                                     })).ToList();

                }
                User user = _unitOfWork.User.GetFirstOrDefault(u => u.user_id == user_id);

             




                double ratio = 74;
                double page_height = 120 + (sv.sales_list.Count() * (ratio))+20;
                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
                { "username", "USER" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {

                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    PageHeight = page_height,
                    // PageWidth = 73.97500,//If width is changed so must be the ratio variable
                    PageWidth = ratio,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 0,
                        Left = 2,
                        Right = 2,
                        Bottom = 0
                    }

                };
                _generatePdf.SetConvertOptions(options);
                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), SaleList = sv, EntryTime = DateTime.SpecifyKind(sv.entry_time, DateTimeKind.Local).ToString("hh:mm tt"), User = user, Method = payment, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/Receipt.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);

            }
            catch
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" });
            }
           

        }



        [HttpGet]
        [Route("~/print/PaymentVoucher")]
        public async Task<IActionResult> PaymentVoucher(string voucher_id)
        {

            try
            {
                string client_code = getClient();
                string trade_code = getTrade();

                Ledger ledger = _unitOfWork.Ledger.GetFirstOrDefault(u =>
                 u.invoice == voucher_id
                && u.status == SD.PAID
                && u.trx_info == SD.CREDIT
                && u.client_code == client_code
                && u.trade_code == trade_code);

                List<ProductEventInfo> peList = _unitOfWork.ProductEventInfo.GetAll(u => u.invoice == ledger.invoice
              && u.transaction_id == ledger.transaction_id
              && u.trade_code == trade_code
              && u.trx_info == SD.DEBIT).ToList();


                User user = _unitOfWork.User.GetFirstOrDefault(u => u.user_id == ledger.user_id);

                var options = new ConvertOptions
                {
                    FooterHtml = SD.SERVER + "/footer.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    //HeaderHtml = SD.SERVER+"/header.html",
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 20,
                        Left = 10,
                        Right = 5,
                        Bottom = 20
                    }

                };
                _generatePdf.SetConvertOptions(options);

                var model = ToExpando(new { Date = ledger.entry_date.ToString("dd/MM/yyyy"), User = user, Debit = peList,  ledger = ledger, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/PaymentVoucher.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" + e});
            }

        }

        public class BalanceSheet
        {


            public string account { get; set; }
            public double amount { get; set; }
            public string category { get; set; }
            public string type { get; set; }

        }

        public class BalanceSheetCompile
        {
            public string accounts_group_name { get; set; }
            public List<BalanceSheet> BalanceSheetItems { get; set; }
        }


        public class Neo
        {

            public List<BalanceSheet> Asset { get; set; }
            public List<BalanceSheet> Liability { get; set; }
            public List<BalanceSheet> OwnersEquity { get; set; }
            public double TotalDebit { get; set; }
            public double TotalCredit { get; set; }

        }
        [HttpGet]
        [Route("~/print/balance_sheet")]
        public async Task<IActionResult> Balance_sheet(DateTime end_date)
        {
            try
            {
                string client_code = getClient();
                string trade_code = getTrade();
              
                   User user = _unitOfWork.User.GetFirstOrDefault(u => u.user_id == GetUserId());
                var parameter = new DynamicParameters();
                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("endDate", end_date.Date.ToString("yyyy-MM-dd"));

                List<BalanceSheet> figures = _unitOfWork.SP_Call.List<BalanceSheet>("balance_sheet", parameter).ToList();
                if (figures == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "No Entry found"
                    });
                }
                if (figures.Count == 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "No Entry found"
                    });
                }
   
                var result = new Neo()
                {
                    Asset = figures.Where(u => u.category == "A").ToList(),
                    Liability = figures.Where(u => u.category == "L").ToList(),
                    OwnersEquity = figures.Where(u => u.category == "C" || u.category == "I").ToList(),
                    TotalDebit = Math.Round(figures.Where(u => u.type == "Dr.").Sum(u => u.amount), 2),
                    TotalCredit = Math.Round(figures.Where(u => u.type == "Cr.").Sum(u => u.amount), 2)
                };
                /////////////////////////////////
                var kv = new Dictionary<string, string>
            {
             { "username", user.first_name+"_"+user.last_name },
          //  { "username", "POSLele" },
                { "age", "20" },
                { "url", "google.com" }
            };

                var options = new ConvertOptions
                {
                  //  HeaderHtml = SD.SERVER+"/header.html",
                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    Replacements = kv,
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 20,
                        Left = 10,
                        Right = 10,
                        Bottom = 20
                    }

                };
                _generatePdf.SetConvertOptions(options);


                //  decimal totBalance = 10.0M;
                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), Result = result , EndDate = end_date, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/BalanceSheet.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


            }
            catch (Exception e)
            {

                return Json(new { success = false, message = e.Message });

            }


        }




        [HttpGet]
        [Route("~/print/ReceiptVoucher")]
        public async Task<IActionResult> ReceiptVoucher(string voucher_id)
        {



            string client_code = getClient();
            string trade_code = getTrade();

            Ledger ledger = _unitOfWork.Ledger.GetFirstOrDefault(u =>
             u.invoice == voucher_id
            && u.status == SD.RECIEVED
            && u.trx_info == SD.DEBIT
            && u.client_code == client_code
            && u.trade_code == trade_code);

            List<ProductEventInfo> peList = _unitOfWork.ProductEventInfo.GetAll(u => u.invoice == ledger.invoice
          && u.transaction_id == ledger.transaction_id
          && u.trade_code == trade_code
          && u.trx_info == SD.CREDIT).ToList();
            User user = _unitOfWork.User.GetFirstOrDefault(u => u.user_id == ledger.user_id);

                var options = new ConvertOptions
                {

                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 20,
                        Left = 10,
                        Right = 5,
                        Bottom = 20
                    }

                };
                _generatePdf.SetConvertOptions(options);

                var model = ToExpando(new { Date = ledger.entry_date.ToString("dd/MM/yyyy"), User = user,  Credit = peList, ledger = ledger, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/ReceiptVoucher.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);

           
        }




        public class IncomeStatement
        {

            public DateTime entry_date { get; set; }
            public string ac_head_name { get; set; }

            public string invoice { get; set; }

            public double amount { get; set; }

            public string control_type { get; set; }


        }


        [HttpGet]
        [Route("~/print/IncomeStatement")]
        public async Task<IActionResult> Income_Statement(DateTime start_date, DateTime end_date)
        {


            string client_code =  getClient();
            string trade_code =  getTrade();
            List<IncomeStatement> incomeStatements = new List<IncomeStatement>();
            var parameter = new DynamicParameters();
            parameter.Add("clientID", client_code);
            parameter.Add("tradeID", trade_code);
            parameter.Add("StartDate", start_date.Date.ToString("yyyy-MM-dd"));
            parameter.Add("EndDate", end_date.Date.ToString("yyyy-MM-dd"));


            if (start_date.Date == new DateTime(0001, 1, 1))
            {
                incomeStatements = _unitOfWork.SP_Call.List<IncomeStatement>("income_statement_last_date", parameter).ToList();
            }
            else
            {

                incomeStatements = _unitOfWork.SP_Call.List<IncomeStatement>("income_statement", parameter).ToList();
            }




            List<IncomeStatement> Revenue = incomeStatements.Where(u => u.control_type == "I").ToList();
            List<IncomeStatement> Expense = incomeStatements.Where(u => u.control_type == "E").ToList();
            bool profit = true;
            double net_total = 0;
            double total_rev = Math.Round(Revenue.Sum(u => u.amount), 2);
            double total_expense = Math.Round(Expense.Sum(u => u.amount), 2);
            if (total_rev >= total_expense)
            {

                profit = true;

                net_total = Math.Round(total_rev - total_expense ,2);

            }
            else
            {
                profit = false;

                net_total = Math.Round(total_expense - total_rev ,2);
            }
            User user = _unitOfWork.User.GetFirstOrDefault(u => u.user_id == GetUserId());

            var options = new ConvertOptions
            {

                PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
              //  HeaderHtml = SD.SERVER + "/header.html",
                FooterHtml = SD.SERVER+"/footer.html",
                PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                {
                    Top = 20,
                    Left = 10,
                    Right = 5,
                    Bottom = 20
                }

            };
            _generatePdf.SetConvertOptions(options);

            var model = ToExpando(new
            {
                Date = DateTime.Now.ToString("dd/MM/yyyy"),
                User = user,
                StartDate = start_date,
                EndDate = end_date,
                Revenue = Revenue,
                Expense = Expense,
                RevenueTotal = total_rev,
                ExpenseTotal = total_expense,
                Profit = profit,
                NetTotal = net_total,
                Client = getClient(client_code),
                Server = SD.SERVER
            });
            string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/IncomeStatement.cshtml");
            return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);


        }







        [HttpGet]
        [Route("~/print/PurchaseOrder")]
        public async Task<IActionResult> Invoice(string invoice)
        {

            try
            {
                string client_code = getClient();
                string trade_code = getTrade();
                string user_id = GetUserId();
                ProductEventInfo pe = _unitOfWork.ProductEventInfo.GetFirstOrDefault(
                    u => u.invoice == invoice
                    && u.ac_head_id== SD.INVENTORY_COST
                    && u.status == SD.PAID
                    && u.client_code == client_code
                    && u.trade_code == trade_code
                    );
                if (pe == null)
                {
                    return Json(new { success = false, message = "No PURCHASE found with this invoice no.!" });
                }





               ProductEventInfo payment = _unitOfWork.ProductEventInfo.GetFirstOrDefault(
                    u => u.invoice == invoice
                    && u.ac_head_id != SD.INVENTORY_COST
                    && u.trx_info == SD.CREDIT
                     && u.status == SD.PAID
                    && u.client_code == client_code
                    && u.trade_code == trade_code);


                //ProductEventInfo payable = _unitOfWork.ProductEventInfo.GetFirstOrDefault(
                //   u => u.transaction_id == pe.transaction_id
                //   && u.ac_head_id == SD.AC_PAYABLE
                //    && u.status == SD.DUE
                //   && u.client_code == client_code
                //   && u.trade_code == trade_code);


                PurchaseVM pv = new PurchaseVM();
                user_id = pe.user_id;
                pv.invoice = pe.invoice;
                pv.payment = payment.cr_total;
                pv.discount_p = pe.dr_discount_percent;
                pv.discount = pe.dr_discount;
                pv.entry_date = pe.entry_date;
                pv.supplier_code = pe.supplier_code;
                pv.supplier_name = pe.supplier_name;
                Supplier supl = _unitOfWork.Supplier.GetFirstOrDefault(u => u.code == pv.supplier_code);
                List<ProductStockIn> prodstockin = _unitOfWork.ProductStockIn.GetAll(u => u.client_code == client_code && u.transaction_id == pe.transaction_id && u.trade_code == trade_code).ToList();
                if (prodstockin.Count() == 0)
                {

                    return Json(new { success = false, message = "No Items Found!" });

                }
                pv.purchase_list = new List<ProductObject>();
                pv.purchase_list = (from p in prodstockin
                                    select (new ProductObject
                                    {
                                        product_code = p.product_code,
                                        product_name = p.product_name,
                                        mrp_price = p.mrp_price,
                                        unit_price = p.unit_price,
                                        expire_date = p.expire_date,
                                        total_price = p.total_price,
                                        quantity = p.quantity
                                    })).ToList();


                pv.total = pe.grand_total;
                pv.grand_total = pv.total - pe.dr_discount;
                pv.due = Math.Round(pv.grand_total - payment.cr_total,2);

                User user = _unitOfWork.User.GetFirstOrDefault(u => u.user_id == user_id);


                var options = new ConvertOptions
                {

                    PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait,
                    FooterHtml = SD.SERVER + "/footer.html",
                    PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                    {
                        Top = 10,
                        Left = 5,
                        Right = 5,
                        Bottom = 10
                    }

                };
                _generatePdf.SetConvertOptions(options);



                var model = ToExpando(new { Date = DateTime.Now.ToString("dd/MM/yyyy"), SupInvoice = pe.purchase_invoice ,Purchase = pv, Supplier = supl, User = user, Method = payment.transaction_type, Client = getClient(client_code), Server = SD.SERVER });
                string htmlViewX = await System.IO.File.ReadAllTextAsync("Reports/PurchaseOrder.cshtml");
                return await _generatePdf.GetPdfViewInHtml(htmlViewX, model);
            }
            catch
            {
                return Json(new { success = false, message = "Sorry! Something Went Wrong!" });

            }
       

        }
        public ExpandoObject ToExpando(object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = new RouteValueDictionary(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }


    }
}
