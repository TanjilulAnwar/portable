using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using POS.ViewModels;

namespace POS.Controllers
{
    public class PurchaseController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public PurchaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public class available
        {
            public string ac_head_id { get; set; }
            public string ac_head_name { get; set; }
            public double available_balance { get; set; }
        }


       public double checkBalance( string account_head_id)
        {


                string client_code = getClient();
                string trade_code = getTrade();

                var parameter = new DynamicParameters();
                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("ac_head", account_head_id);

                available figures = _unitOfWork.SP_Call.OneRecord<available>("available_balance", parameter);
                if (figures == null)
                {
                    return 0.0;
                }
                return figures.available_balance;
          
        }






        [HttpGet]
        [Route("~/DropDown/suppliers")]
        public  IActionResult purchase_dropdown_supplier()
        {
            try

            {
                string client_code = getClient();
                string trade_code = getTrade();
                IEnumerable<Supplier> supList = _unitOfWork.Supplier.GetAll(u => u.client_code == client_code && u.trade_code == trade_code); 
                var suppliers = from c in supList select (new { c.code, c.name });
                return Json(new { success = true, Suppliers = suppliers });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });

            }
        }

        [HttpGet]
        [Route("~/Product/info")]
        public IActionResult product_info(string product_code)
        {
            try
            {
                string client_code = getClient();
                string trade_code = getTrade();                                                     
                Product prod = _unitOfWork.Product.GetFirstOrDefault(u => u.product_code == product_code && u.client_code == client_code && u.trade_code == trade_code);
                if(prod == null)
                {
                    return Json(new { success = false, message = "No Product Found" });
                }

                var productObject = new
                {
                    product_code = prod.product_code,
                    mrp_price = prod.mrp_price,
                    unit_price = prod.unit_price,
                    manufacturer = prod.manufacturer,
                    quantity = prod.quantity,
                    description = prod.description

                };

                return Json(new { success = true, message= productObject });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });

            }
        }

        [HttpGet]
        [Route("~/Product/Categorywise")]
        public IActionResult Categorywise(string category_code)
        {

            try

            {
                string client_code = getClient();
                string trade_code = getTrade();
                List<Product> prodList = _unitOfWork.Product.GetAll(u => u.category_code == category_code && u.client_code == client_code && u.trade_code == trade_code).ToList();
                if (prodList.Count == 0)
                {
                    return Json(new { success = false, message = "No Product Found" });
                }
                var prodListNew = from c in prodList select (new { c.product_code, c.product_name,c.subcategory_code,c.subcategory });

               
                return Json(new { success = true, message = prodListNew.OrderBy(u=>u.product_name) });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });

            }
        }





        [HttpPost]
        [Route("~/Purchase/confirm")]
        public IActionResult PurchaseConfirm([FromBody] PurchaseVM purchaseVM)
        {

           try
           {
                if (ModelState.IsValid)
                {

                    /************validation**************/
                    if (purchaseVM.purchase_list == null)
                    {
                        return Json(new { success = false, message = "There are no Purchased product entry!" });
                    }
                    if (purchaseVM.purchase_list.Count == 0)
                    {
                        return Json(new { success = false, message = "Cannot have a purchase without a single product!" });
                    }
                    if (purchaseVM.grand_total - purchaseVM.payment < 0)
                    {
                        return Json(new { success = false, message = "Payment is greater than total" });
                    }
                    if (purchaseVM.account_head_id == null)
                    {
                        return Json(new { success = false, message = "Specify a payment type" });
                    }
                    double chBal = checkBalance(purchaseVM.account_head_id);
                    if (purchaseVM.payment > chBal)
                    {
                        return Json(new { success = false, message = "Not enough balance in this particular account!" });
                    }
                    /************validation**************/




                    string client_code = getClient();
                    string trade_code = getTrade();
                    string user_id = GetUserId();
                    Guid guid = Guid.NewGuid();
                    string TRX_ID = guid.ToString();
                    /////////////////////////////////Purchase Dr. Entry///////////////////////////////////////////////////
                    ProductEventInfo productEventInfo = new ProductEventInfo();
                    productEventInfo.transaction_id = TRX_ID;
                    AccountsHead acPurchase = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == SD.INVENTORY_COST && u.client_code == client_code);
                    productEventInfo.ac_head_id = acPurchase.ac_head_id;
                    productEventInfo.ac_head_name = acPurchase.ac_head_name;
                    productEventInfo.label = productEventInfo.ac_head_name;
                    productEventInfo.transaction_type = acPurchase.ac_head_name;
                    productEventInfo.client_code = client_code;
                    productEventInfo.trade_code = trade_code;
                    productEventInfo.user_id = user_id;

                    if (purchaseVM.invoice == null)
                    {
                        productEventInfo.invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);
                        purchaseVM.invoice = productEventInfo.invoice;
                        productEventInfo.purchase_invoice = purchaseVM.invoice;
                    }
                    else
                    {
                        productEventInfo.purchase_invoice = purchaseVM.invoice;
                        productEventInfo.invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);
                    }
                    productEventInfo.entry_date = purchaseVM.entry_date;
                    productEventInfo.supplier_code = purchaseVM.supplier_code;
                    productEventInfo.supplier_name = _unitOfWork.Supplier.GetFirstOrDefault(u => u.code == purchaseVM.supplier_code && u.client_code == client_code && u.trade_code == trade_code).name;
                    double total = 0.0;
                    foreach (ProductObject po in purchaseVM.purchase_list)
                    {
                        Product product = _unitOfWork.Product.GetFirstOrDefault(u => u.product_code == po.product_code && u.client_code == client_code && u.trade_code == trade_code);
                        product.quantity_in += po.quantity;
                        product.quantity += po.quantity;
                        product.unit_price = po.unit_price;
                        product.mrp_price = po.mrp_price;
                        product.last_expire_date = po.expire_date;
                        product.batch_no = po.batch_no;
                        _unitOfWork.Product.Update(product);
                        ProductStockIn prodStockIn = new ProductStockIn();
                        prodStockIn.transaction_id = TRX_ID;
                        prodStockIn.product_code = product.product_code;
                        prodStockIn.product_name = product.product_name;
                        prodStockIn.manufacturer_code = product.manufacturer_code;
                        prodStockIn.supplier_code = purchaseVM.supplier_code;
                        prodStockIn.quantity = po.quantity;
                        prodStockIn.unit_price = po.unit_price;
                        prodStockIn.mrp_price = po.mrp_price;
                        prodStockIn.batch_no = po.batch_no;
                        prodStockIn.total_price = po.quantity * po.unit_price;
                        prodStockIn.expire_date = po.expire_date;
                        prodStockIn.entry_date = purchaseVM.entry_date;
                        prodStockIn.invoice = productEventInfo.invoice;
                        prodStockIn.user_id = user_id;
                        prodStockIn.client_code = client_code;
                        prodStockIn.barcode = product.barcode;
                        prodStockIn.trade_code = trade_code;
                        _unitOfWork.ProductStockIn.Add(prodStockIn);

                        ProductStock ps = _unitOfWork.ProductStock.GetFirstOrDefault(u => u.product_code == po.product_code && u.entry_date == purchaseVM.entry_date && u.client_code == client_code && u.trade_code == trade_code);
                        if (ps == null)
                        {
                            ps = new ProductStock();
                            ps.product_code = po.product_code;
                            ps.product_name = product.product_name;
                            ps.manufacturer_code = product.manufacturer_code;
                            var parameter = new DynamicParameters();
                            parameter.Add("ProductCode", po.product_code);
                            parameter.Add("ClientCode", client_code);
                            parameter.Add("EntryDate", purchaseVM.entry_date.ToString("yyyy-MM-dd"));

                            ProductStock psPrevious = _unitOfWork.SP_Call.OneRecord<ProductStock>("latest_stock_entry", parameter);
                            if (psPrevious != null)
                            {
                                ps.opening_stock = (psPrevious.opening_stock + psPrevious.quantity_in) - psPrevious.quantity_out;

                            }
                            else
                            {
                                ps.opening_stock = 0;
                            }

                            ps.quantity_in = po.quantity;
                            ps.quantity_out = 0;
                            ps.closing_stock = (ps.opening_stock + ps.quantity_in) - ps.quantity_out;
                            ps.unit_price = po.unit_price;
                            ps.mrp_price = po.mrp_price;
                            ps.expire_date = po.expire_date;
                            ps.entry_date = purchaseVM.entry_date;
                            ps.user_id = user_id;
                            ps.barcode = product.barcode;
                            ps.client_code = client_code;
                            ps.trade_code = trade_code;
                            ps.batch_no = po.batch_no;



                            _unitOfWork.ProductStock.Add(ps);

                        }
                        else
                        {
                            ps.quantity_in += po.quantity;
                            ps.closing_stock = (ps.opening_stock + ps.quantity_in) - ps.quantity_out;
                            ps.unit_price = po.unit_price;
                            ps.mrp_price = po.mrp_price;
                            ps.user_id = user_id;
                            ps.expire_date = po.expire_date;

                            _unitOfWork.ProductStock.Update(ps);
                        }
                        ExpireLog el = new ExpireLog();
                        el.batch_no = po.batch_no;
                        el.quantity = po.quantity;
                        el.product_code = po.product_code;
                        el.product_name = po.product_name;
                        el.expire_date = po.expire_date;
                        el.trade_code = trade_code;
                        el.client_code = client_code;
                        _unitOfWork.ExpireLog.Add(el);

                        total += po.quantity * po.unit_price;

                    }
                    productEventInfo.grand_total = total;

                    if (purchaseVM.percent)
                    {
                        productEventInfo.dr_discount_percent = purchaseVM.discount;
                        productEventInfo.dr_discount = Math.Round(productEventInfo.grand_total * (productEventInfo.dr_discount_percent / 100), 2);

                    }
                    else
                    {
                        productEventInfo.dr_discount = purchaseVM.discount;
                        productEventInfo.dr_discount_percent = Math.Round((productEventInfo.dr_discount / productEventInfo.grand_total) * 100, 2);

                    }
                    productEventInfo.dr_amount = Math.Round(productEventInfo.grand_total - productEventInfo.dr_discount, 2);
                    productEventInfo.dr_total = productEventInfo.dr_amount;
                    productEventInfo.cr_amount = productEventInfo.cr_discount = productEventInfo.cr_discount_percent = productEventInfo.cr_total = 0.0;

                    productEventInfo.trx_info = acPurchase.ac_type;
                    productEventInfo.returned = false;
                    productEventInfo.status = SD.PAID;
                    _unitOfWork.ProductEventInfo.Add(productEventInfo);
                    /////////////////////


                    //////Bank or cash/////



                 if (purchaseVM.payment !=0)
                 { 
                    ProductEventInfo productEventMedium = productEventInfo.ShallowCopy();
                    productEventMedium.id = 0;
                    AccountsHead acMedium = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == purchaseVM.account_head_id && u.client_code == client_code);
                    productEventMedium.ac_head_id = acMedium.ac_head_id;
                    productEventMedium.ac_head_name = acMedium.ac_head_name;
                    productEventMedium.trx_info = SD.CREDIT;
                    productEventMedium.returned = false;
                    productEventMedium.transaction_type = productEventMedium.ac_head_name;
                    productEventMedium.cr_amount = purchaseVM.payment;
                    productEventMedium.cr_discount = productEventInfo.dr_discount;
                    productEventMedium.cr_discount_percent = productEventInfo.dr_discount_percent;
                    productEventMedium.cr_total = purchaseVM.payment;
                    productEventMedium.dr_amount = productEventMedium.dr_discount = productEventMedium.dr_total = productEventMedium.dr_discount_percent = 0.0;
                    productEventInfo.status = SD.PAID;

                    _unitOfWork.ProductEventInfo.Add(productEventMedium);
                }

/////////////////Payable//////////////////////////////
               if(purchaseVM.grand_total-purchaseVM.payment!=0)
                    {
                        ProductEventInfo productEventInfoPayable = productEventInfo.ShallowCopy();
                        productEventInfoPayable.id = 0;
                        productEventInfoPayable.invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);
                        AccountsHead acPayable = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == SD.AC_PAYABLE && u.client_code == client_code);
                        productEventInfoPayable.ac_head_id = acPayable.ac_head_id;
                        productEventInfoPayable.ac_head_name = acPayable.ac_head_name;
                        productEventInfoPayable.transaction_type = acPayable.ac_head_name;
                        productEventInfoPayable.cr_amount = productEventInfo.dr_total - purchaseVM.payment;
                        productEventInfoPayable.cr_discount = productEventInfo.dr_discount;
                        productEventInfoPayable.cr_discount_percent = productEventInfo.dr_discount_percent;
                        productEventInfoPayable.cr_total = productEventInfoPayable.cr_amount;
                        productEventInfoPayable.dr_amount = productEventInfoPayable.dr_discount = productEventInfoPayable.dr_total = productEventInfoPayable.dr_discount_percent = 0.0; 
                        productEventInfoPayable.trx_info = acPayable.ac_type;
                        productEventInfoPayable.status = SD.DUE;
                        _unitOfWork.ProductEventInfo.Add(productEventInfoPayable);



                        //ProductEventInfo productEventPayableTRX = productEventInfo.ShallowCopy();
                        //productEventPayableTRX.invoice = productEventInfoPayable.invoice;
                        //productEventPayableTRX.id = 0;
                        //productEventPayableTRX.dr_amount = productEventInfoPayable.cr_amount;
                        //productEventPayableTRX.dr_discount = productEventInfoPayable.cr_discount;
                        //productEventPayableTRX.dr_discount_percent = productEventInfoPayable.cr_discount_percent;
                        //productEventPayableTRX.dr_total = productEventInfoPayable.cr_total;
                        //productEventPayableTRX.cr_amount = productEventPayableTRX.cr_discount = productEventPayableTRX.cr_total = productEventPayableTRX.cr_discount_percent = 0.0;
                        //productEventPayableTRX.status = SD.DUE;
                        //_unitOfWork.ProductEventInfo.Add(productEventPayableTRX);

                    }
                   


                _unitOfWork.Save();


                    return Json(new { success = true, message = "Successful!!" , invoice= productEventInfo.invoice});
                }
                else
                {
                    return Json(new { success = false, message = "Input error !" });
                }


           }


          catch (Exception e)
         {
               return Json(new { success = false, message = e.Message });
          }
             

            
        }


        [HttpGet]
        [Route("~/Purchase/history")]
        public IActionResult PurchaseHistoryByDate(DateTime start_date, DateTime end_date,string supplier_code)
        {

            try{

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
      
                if(peList.Count == 0)
                {
                    return Json(new { success = false, message = "No Purchase was made in this day!" });
                }
                List<PurchaseVM> purchaseVMList = new List<PurchaseVM>();
                foreach(ProductEventInfo pe in peList)
                {


                    PurchaseVM pv = new PurchaseVM();
                    pv.transaction_id = pe.transaction_id;
                    pv.invoice = pe.invoice;
                    ProductEventInfo peX = _unitOfWork.ProductEventInfo.GetFirstOrDefault( u=>
                    u.trx_info == SD.CREDIT
                    && u.invoice ==pe.invoice
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
                    List<ProductStockIn> prodstockin = _unitOfWork.ProductStockIn.GetAll(u=>u.client_code == client_code && u.client_code == client_code && u.transaction_id == pe.transaction_id).ToList();
                    if(prodstockin.Count() == 0)
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
                    if(peDues != null)
                    {
                        pv.due = peDues.cr_total;
                    }
                    purchaseVMList.Add(pv);


                }


                return Json(new { success = true, message = purchaseVMList });
            }

            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        
        }

        [HttpGet]
        [Route("~/Purchase/history/single")]
        public IActionResult PurchaseHistoryByInvoice(string invoice)
        {
            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                List<ProductEventInfo> peList = _unitOfWork.ProductEventInfo.GetAll(
                    u => u.invoice == invoice
                    && u.ac_head_id == SD.INVENTORY_COST
                     && u.status == SD.PAID
                    && u.client_code == client_code
                    && u.trade_code == trade_code
                    ).ToList();
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
                    ProductEventInfo payment = _unitOfWork.ProductEventInfo.GetFirstOrDefault(u =>
                 u.trx_info == SD.CREDIT
                 && u.invoice == pe.invoice
                 && u.status == SD.PAID
                    );
                    ProductEventInfo payable = _unitOfWork.ProductEventInfo.GetFirstOrDefault(u =>
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
                    pv.returned = pe.returned;
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
                    pv.payment = payment.cr_total;
                    pv.payment_type = payment.ac_head_name;
                    pv.grand_total = pv.total - pv.discount;
                    pv.due = 0;
                    if (payable != null)
                    {
                        pv.due = payable.cr_total;
                    }
                    purchaseVMList.Add(pv);


                }


                return Json(new { success = true, message = purchaseVMList });
            }

            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }


        }








    }
}
