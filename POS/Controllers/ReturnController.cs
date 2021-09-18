using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using POS.ViewModels;
using System;
using System.Collections.Generic;


namespace POS.Controllers
{
    public class ReturnController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ReturnController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public class ReturnVM
        {

            public string supplier_code { get; set; }
            public string customer_code { get; set; }
            public string invoice { get; set; }
            public List<ProductObject> product_list { get; set; }
            public string account_head_id { get; set; }
            public double total { get; set; }
            public double payment { get; set; }
            public double discount_p { get; set; }
            public double discount { get; set; }
            public bool percent { get; set; }
            public string return_by { get; set; }



        }


        public class available
        {
            public string ac_head_id { get; set; }
            public string ac_head_name { get; set; }
            public double available_balance { get; set; }
        }


        public double checkBalance(string account_head_id)
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




        [HttpPost]
        [Route("~/Sales/Return")]
        public IActionResult SalesReturn([FromBody] ReturnVM returnVM)
        {

            try
            {
                if (ModelState.IsValid)
                {


                    /************validation**************/
                    if (returnVM.product_list == null)
                    {
                        return Json(new { success = false, message = "There are no Return product entry!" });
                    }
                    if (returnVM.product_list.Count == 0)
                    {
                        return Json(new { success = false, message = "Cannot have a Return without a single product!" });
                    }
                    if (returnVM.total <= 0)
                    {
                        return Json(new { success = false, message = "Invalid Entry" });
                    }
                    if (returnVM.return_by == null)
                    {
                        return Json(new { success = false, message = "Specify a payment type" });
                    }
                    double chBal = checkBalance(returnVM.return_by);
                    if (returnVM.total > chBal)
                    {
                        return Json(new { success = false, message = "Not enough balance in this particular account!" });
                    }


                    /************validation**************/



                    string client_code = getClient();
                    string trade_code = getTrade();
                    string user_id = GetUserId();
                    Guid guid = Guid.NewGuid();
                    string TRX_ID = guid.ToString();
                    DateTime entry_date = DateTime.Now.Date;
                    string invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);


                    ProductEventInfo productEventInfo = new ProductEventInfo();
                    productEventInfo.transaction_id = TRX_ID;
                    AccountsHead acPurchase = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == SD.CUSTOMER_RETURNS && u.client_code == client_code);
                    productEventInfo.ac_head_id = acPurchase.ac_head_id;
                    productEventInfo.ac_head_name = acPurchase.ac_head_name;
                    productEventInfo.transaction_type = acPurchase.ac_head_name;
                    productEventInfo.label = acPurchase.ac_head_name;
                    productEventInfo.client_code = client_code;
                    productEventInfo.trade_code = trade_code;
                    productEventInfo.user_id = user_id;
                    productEventInfo.invoice = invoice;
                    if(returnVM.customer_code == "CUS0000X")
                    {
                        productEventInfo.customer_name = "G. CUSTOMER";
                    }
                    else
                    {
                        productEventInfo.customer_code = returnVM.customer_code;
                        productEventInfo.customer_name = _unitOfWork.Customer.GetFirstOrDefault(u => u.code == returnVM.customer_code && u.client_code == client_code && u.trade_code == trade_code).name;
                    }
                   

                    double total = 0.0;
                    foreach (ProductObject po in returnVM.product_list)
                    {
                        Product product = _unitOfWork.Product.GetFirstOrDefault(u => u.product_code == po.product_code && u.client_code == client_code && u.trade_code == trade_code);
                        product.quantity_in += po.quantity;
                        product.quantity += po.quantity;
                        product.unit_price = po.unit_price;
                        product.mrp_price = po.mrp_price;
                        product.last_expire_date = po.expire_date;
                        _unitOfWork.Product.Update(product);
                        ProductStockIn prodStockIn = new ProductStockIn();
                        prodStockIn.transaction_id = TRX_ID;
                        prodStockIn.product_code = product.product_code;
                        prodStockIn.product_name = product.product_name;
                        prodStockIn.manufacturer_code = product.manufacturer_code;
                        prodStockIn.quantity = po.quantity;
                        prodStockIn.unit_price = po.unit_price;
                        prodStockIn.supplier_code = acPurchase.ac_head_name;
                        prodStockIn.batch_no = po.batch_no;
                        prodStockIn.mrp_price = po.mrp_price;
                        prodStockIn.total_price = po.quantity * po.unit_price;
                        prodStockIn.expire_date = po.expire_date;
                        prodStockIn.entry_date = entry_date;
                        prodStockIn.invoice = invoice;
                        prodStockIn.user_id = user_id;
                        prodStockIn.client_code = client_code;
                        prodStockIn.barcode = product.barcode;
                        prodStockIn.trade_code = trade_code;
                        _unitOfWork.ProductStockIn.Add(prodStockIn);




                        ProductStock ps = _unitOfWork.ProductStock.GetFirstOrDefault(u => u.product_code == po.product_code && u.entry_date == entry_date && u.client_code == client_code && u.trade_code == trade_code);
                        if (ps == null)
                        {
                            ps = new ProductStock();
                            ps.product_code = po.product_code;
                            ps.product_name = product.product_name;
                            ps.manufacturer_code = product.manufacturer_code;
                            var parameter = new DynamicParameters();
                            parameter.Add("ProductCode", po.product_code);
                            parameter.Add("ClientCode", client_code);
                            parameter.Add("EntryDate", entry_date.ToString("yyyy-MM-dd"));



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
                            ps.entry_date = entry_date;
                            ps.user_id = user_id;
                            ps.barcode = product.barcode;
                            ps.client_code = client_code;
                            ps.trade_code = trade_code;
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


                        total += po.quantity * po.unit_price;

                    }

                    productEventInfo.dr_amount = total;
                    if (returnVM.percent)
                    {
                        productEventInfo.dr_discount_percent = returnVM.discount;
                        productEventInfo.dr_discount = Math.Round(productEventInfo.dr_amount * (productEventInfo.dr_discount_percent / 100),2);
                        productEventInfo.dr_total = productEventInfo.dr_amount - productEventInfo.dr_discount;
                    }
                    else
                    {
                        productEventInfo.dr_discount = returnVM.discount;
                        productEventInfo.dr_discount_percent = Math.Round((productEventInfo.dr_discount / productEventInfo.dr_amount) * 100,2);
                        productEventInfo.dr_total = productEventInfo.dr_amount - productEventInfo.dr_discount;
                    }



                    productEventInfo.cr_amount = productEventInfo.cr_discount = productEventInfo.cr_discount_percent = productEventInfo.cr_total = 0.0;
                    productEventInfo.returned = true;
                    productEventInfo.trx_info = acPurchase.ac_type;
                    productEventInfo.status = SD.PAID;
                    productEventInfo.grand_total = productEventInfo.dr_total;

                    _unitOfWork.ProductEventInfo.Add(productEventInfo);



                    ProductEventInfo productEventMedium = productEventInfo.ShallowCopy();
                    productEventMedium.id = 0;
                    AccountsHead acMedium = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == returnVM.return_by && u.client_code == client_code);
                    productEventMedium.ac_head_id = acMedium.ac_head_id;
                    productEventMedium.ac_head_name = acMedium.ac_head_name;
                    productEventMedium.transaction_type = acMedium.ac_head_name;
                    productEventMedium.cr_amount = productEventInfo.dr_amount;
                    productEventMedium.cr_discount = productEventInfo.dr_discount;
                    productEventMedium.cr_discount_percent = productEventInfo.dr_discount_percent;
                    productEventMedium.cr_total = productEventInfo.dr_total;
                    productEventMedium.dr_amount = productEventMedium.dr_discount = productEventMedium.dr_total = productEventMedium.dr_discount_percent = 0.0;

                    productEventInfo.returned = true;
                    productEventInfo.trx_info = SD.CREDIT;
                    productEventInfo.status = SD.PAID;
                    _unitOfWork.ProductEventInfo.Add(productEventMedium);
                  

                    ProductEventInfo previous = _unitOfWork.ProductEventInfo.GetFirstOrDefault(u => u.invoice == returnVM.invoice && u.ac_head_id == SD.SALES_REVENUE);
                    if(previous != null)
                    {
                        previous.returned = true;
                        _unitOfWork.ProductEventInfo.Update(previous);
                    }
                   


                    _unitOfWork.Save();


                    return Json(new { success = true, message = "Return Successful!!", invoice = invoice });
                }
                else
                {
                    return Json(new { success = false, message = "Input error!" });
                }


            }


            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }



        }



        [HttpPost]
        [Route("~/Purchase/return")]
        public IActionResult PurchaseReturn([FromBody] ReturnVM returnVM)
        {

            try
           {
            if (ModelState.IsValid)
            {
                   
                    /************validation**************/
                    if (returnVM.supplier_code == null)
                    {
                        throw new InvalidOperationException("No Supplier Code Found!!");
                     }
                 
                    if (returnVM.product_list == null)
                    {
                        return Json(new { success = false, message = "There are no Return product entry!" });
                    }
                    if (returnVM.product_list.Count == 0)
                    {
                        return Json(new { success = false, message = "Cannot have a Return without a single product!" });
                    }
                    if (returnVM.total <= 0)
                    {
                        return Json(new { success = false, message = "Invalid Entry" });
                    }
                    if (returnVM.return_by == null)
                    {
                        return Json(new { success = false, message = "Specify a recieve type" });
                    }

                    /************validation**************/

                    //saleVM.invoice= DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + RandomString(7);

                    DateTime entry_date = DateTime.Now.Date;
                string client_code = getClient();
                string trade_code = getTrade();
                string user_id = GetUserId();
                //  string TRX_ID = _unitOfWork.ProductStock.setTransactionID(client_code);
                string invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);
                Guid guid = Guid.NewGuid();
                string TRX_ID = guid.ToString();

                ProductEventInfo productEventInfo = new ProductEventInfo();
                productEventInfo.transaction_id = TRX_ID;
                AccountsHead acSales = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == SD.RETURNED_GOODS && u.client_code == client_code);
                productEventInfo.ac_head_id = acSales.ac_head_id;
                productEventInfo.ac_head_name = acSales.ac_head_name;
                productEventInfo.transaction_type = acSales.ac_head_name;
                    productEventInfo.label = acSales.ac_head_name;
                productEventInfo.invoice =invoice;
                    productEventInfo.user_id = user_id;
                    productEventInfo.client_code = client_code;
                    productEventInfo.trade_code = trade_code;
                    productEventInfo.entry_date = entry_date;
                productEventInfo.entry_time = DateTime.Now;
                productEventInfo.supplier_code = returnVM.supplier_code;
                productEventInfo.supplier_name = _unitOfWork.Supplier.GetFirstOrDefault(u => u.code == returnVM.supplier_code && u.client_code == client_code).name;

                 double total = 0.0;
                foreach (ProductObject po in returnVM.product_list)
                {

                    Product product = _unitOfWork.Product.GetFirstOrDefault(u => u.product_code == po.product_code && u.client_code == client_code && u.trade_code == trade_code);
                    if (product.quantity < po.quantity)
                    {
                        return Json(new { success = false, message = "The quantity demanded for " + product.product_name + " exceeds available quantity! Sales was Cancelled !" });
                    }
                    product.quantity_out += po.quantity;
                    product.quantity -= po.quantity;
                    // product.unit_price = po.unit_price;
                    // product.mrp_price = po.mrp_price;
                    // product.last_expire_date = po.expire_date;
                    _unitOfWork.Product.Update(product);
                    ProductStockOut prodStockOut = new ProductStockOut();
                    prodStockOut.transaction_id = TRX_ID;
                    prodStockOut.product_code = product.product_code;
                    prodStockOut.product_name = product.product_name;
                    prodStockOut.manufacturer_code = product.manufacturer_code;
                  //  prodStockOut.customer_code = saleVM.customer_code;
                    prodStockOut.quantity = po.quantity;
                    prodStockOut.unit_price = po.unit_price;
                    prodStockOut.mrp_price = po.mrp_price;
                    prodStockOut.total_price = po.quantity * po.mrp_price;
                    prodStockOut.discount_percentage = po.discount;
                    prodStockOut.discount = prodStockOut.total_price * (po.discount / 100);
                    prodStockOut.total_price_deducted = prodStockOut.total_price - prodStockOut.discount;
                    prodStockOut.expire_date = po.expire_date;
                    prodStockOut.entry_date =entry_date;
                    prodStockOut.customer_code = acSales.ac_head_name;
                    prodStockOut.batch_no = po.batch_no;
                    prodStockOut.invoice = invoice;
                    prodStockOut.user_id = user_id;
                    prodStockOut.client_code = client_code;
                    prodStockOut.barcode = product.barcode;
                    prodStockOut.trade_code = trade_code;
                    _unitOfWork.ProductStockOut.Add(prodStockOut);


                    ProductStock ps = _unitOfWork.ProductStock.GetFirstOrDefault(u => u.product_code == po.product_code && u.entry_date == entry_date && u.client_code == client_code && u.trade_code == trade_code);
                    if (ps == null)
                    {
                        ps = new ProductStock();
                        ps.product_code = po.product_code;
                        ps.product_name = product.product_name;
                        ps.manufacturer_code = product.manufacturer_code;
                        var parameter = new DynamicParameters();
                        parameter.Add("ProductCode", po.product_code);
                        parameter.Add("ClientCode", client_code);
                        parameter.Add("EntryDate", entry_date.ToString("yyyy-MM-dd"));



                        ProductStock psPrevious = _unitOfWork.SP_Call.OneRecord<ProductStock>("latest_stock_entry", parameter);
                        if (psPrevious != null)
                        {
                            ps.opening_stock = (psPrevious.opening_stock + psPrevious.quantity_in) - psPrevious.quantity_out;

                        }
                        else
                        {
                            return Json(new { success = false, message = product.product_name + " is out of stock! Sales was Cancelled !" });
                            // ps.opening_stock = 0;
                        }

                        ps.quantity_in = 0;
                        ps.quantity_out = po.quantity;
                        ps.closing_stock = (ps.opening_stock + ps.quantity_in) - ps.quantity_out;
                        ps.unit_price = po.unit_price;
                        ps.mrp_price = po.mrp_price;
                        ps.expire_date = po.expire_date;
                        ps.entry_date = entry_date;
                        ps.user_id = user_id;
                        ps.barcode = product.barcode;
                        ps.client_code = client_code;
                        ps.trade_code = trade_code;
                        _unitOfWork.ProductStock.Add(ps);

                    }
                    else
                    {
                        ps.quantity_out += po.quantity;
                        ps.closing_stock = (ps.opening_stock + ps.quantity_in) - ps.quantity_out;
                        ps.unit_price = po.unit_price;
                        ps.mrp_price = po.mrp_price;
                        ps.user_id = user_id;
                        ps.expire_date = po.expire_date;
                        _unitOfWork.ProductStock.Update(ps);
                    }


                    total += prodStockOut.total_price_deducted;

                }
                productEventInfo.cr_amount = total;
                if (returnVM.percent)
                {
                    productEventInfo.cr_discount_percent = returnVM.discount;
                    productEventInfo.cr_discount = Math.Round(productEventInfo.cr_amount * (productEventInfo.cr_discount_percent / 100),2);
                  
                }
                else
                {
                    productEventInfo.cr_discount = returnVM.discount;
                    productEventInfo.cr_discount_percent = Math.Round((productEventInfo.cr_discount / productEventInfo.cr_amount) * 100,2);
                    
                }

                    productEventInfo.cr_total = Math.Round(productEventInfo.cr_amount - productEventInfo.cr_discount, 2);
                    productEventInfo.dr_amount = productEventInfo.dr_discount = productEventInfo.dr_discount_percent = productEventInfo.dr_total = 0.0;

                    productEventInfo.returned = true;
                    productEventInfo.trx_info = acSales.ac_type;
                    productEventInfo.status = SD.RECIEVED;
                    productEventInfo.grand_total = productEventInfo.cr_total;
                    _unitOfWork.ProductEventInfo.Add(productEventInfo);


                    ProductEventInfo productEventMedium = productEventInfo.ShallowCopy();
                    productEventMedium.id = 0;
                    AccountsHead acMedium = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == returnVM.return_by && u.client_code == client_code);
                    productEventMedium.ac_head_id = acMedium.ac_head_id;
                    productEventMedium.ac_head_name = acMedium.ac_head_name;
                    productEventMedium.transaction_type = acMedium.ac_head_name;
                    productEventMedium.dr_amount = productEventInfo.cr_amount;
                    productEventMedium.dr_discount = productEventInfo.cr_discount;
                    productEventMedium.dr_discount_percent = productEventInfo.cr_discount_percent;
                    productEventMedium.dr_total = productEventInfo.cr_total;
                    productEventMedium.cr_amount = productEventMedium.cr_discount = productEventMedium.cr_total = productEventMedium.cr_discount_percent = 0.0;
                    
                    productEventInfo.returned = true;
                    productEventInfo.trx_info = SD.DEBIT;
                    productEventInfo.status = SD.RECIEVED;
                    _unitOfWork.ProductEventInfo.Add(productEventMedium);

                    ProductEventInfo previous = _unitOfWork.ProductEventInfo.GetFirstOrDefault(u => u.invoice == returnVM.invoice && u.ac_head_id == SD.INVENTORY_COST);
                    previous.returned = true;
                    _unitOfWork.ProductEventInfo.Update(previous);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Successful!!", invoice = productEventInfo.invoice });
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







    }
}
