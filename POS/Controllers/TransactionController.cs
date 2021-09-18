using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using POS.ViewModels;

namespace POS.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TransactionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public class available
        {
            public string ac_head_id { get; set; }
            public string ac_head_name { get; set; }
            public double available_balance { get; set; }
        }


        [HttpGet]
        [Route("~/Accountshead/AvailableBalance")]
        public IActionResult available_balance(string account_head_id)
        {
            try
            {
                string client_code = getClient();
                string trade_code =  getTrade();
                AccountsHead acHead = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == account_head_id
                && u.client_code == client_code && u.trade_code == trade_code);
                if (acHead == null)
                {
                    return Json(new { success = false, message = "Account Head not found!" });
                }


           
                var parameter = new DynamicParameters();
                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("ac_head", account_head_id);

                available figures = _unitOfWork.SP_Call.OneRecord<available>("available_balance", parameter);
                if (figures == null)
                {
                    return Json(new { success = true, message = new { available_balance = 0.0} });
                }
                return Json(new { success = true, message = figures });
            }
            catch{

                return Json(new { success = false, message = "Something went wrong!" });
            }
        
        
        
        
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















        [HttpGet]
        [Route("~/accountshead/entry_point")]
        public IActionResult Accounts_Index(string type)//E or I
        {
            string client_code = getClient();
            string trade_code = getTrade();

            List<AccountsHead> accountsHeads = new List<AccountsHead>();
            List<AccountsHead> accountsHeads_sub = new List<AccountsHead>();
            if (type == "E")
            {
                
                 accountsHeads = _unitOfWork.AccountsHead.GetAll(u => (u.control_type == type || u.control_type == "L" ) && u.client_code == client_code && u.trade_code == trade_code && u.main_sub == "M").ToList();
                accountsHeads_sub = _unitOfWork.AccountsHead.GetAll(u => (u.control_type == type || u.control_type == "L") && u.client_code == client_code ).ToList();
            }
            if (type == "I")
            {

                accountsHeads = _unitOfWork.AccountsHead.GetAll(u => (u.control_type == type || u.ac_head_id == SD.AC_RECEIVABLE || u.control_type == "C" || u.control_type == "L" && u.main_sub == "M") && u.client_code == client_code && u.trade_code == trade_code).ToList();
                accountsHeads_sub = _unitOfWork.AccountsHead.GetAll(u => (u.control_type == type || u.ac_head_id == SD.AC_RECEIVABLE || u.control_type == "C" || u.control_type == "L" ) && u.client_code == client_code && u.trade_code == trade_code).ToList();
            }

            var Query = from ac_head in accountsHeads select new
            {
                ac_head.ac_head_name,
                ac_head.ac_head_id,
                ac_name_list = from ac_name in accountsHeads_sub where ac_name.ac_name_head_id == ac_head.ac_head_id select new { ac_name.ac_head_name, ac_name.ac_head_id }
            } ;
            return Json(new { Query });
        }


        [HttpGet]
        [Route("~/accountshead/transactionMedia")]
        public IActionResult trxMedia()
        {
            string client_code = getClient();
            List<AccountsHead> accountsHeads = _unitOfWork.AccountsHead.GetAll(u =>u.ac_group_id == "01" && u.client_code == client_code && u.trade_code == getTrade()).ToList();
            var Query = from ac_head in accountsHeads select new { ac_head.ac_head_name, ac_head.ac_head_id };
            return Json(new { Query });
        }

        public class PayProp
        {
            public string payment_head { get; set; }
            public string payment_name_id { get; set; }
            public double amount { get; set; }
            public string supplier_code { get; set; }
            public string description { get; set; }
        }

        public class PayTrans
        {
            public DateTime payment_date { get; set; }
            public string account_head_id { get; set; }
            public string payment_by { get; set; }
            public double total_payment { get; set; }
            public List<PayProp> pay_for { get; set; }
            public string voucher_id { get; set; }
            public string ref_no { get; set; }
        }


        public class ReceiptProp
        {
            public string receipt_head { get; set; }
            public string receipt_name_id { get; set; }
            public double amount { get; set; }
            public string customer_code { get; set; }
            public string description { get; set; }
        }

        public class ReceiptTrans
        {
            public DateTime receipt_date { get; set; }
            public string account_head_id { get; set; }
            public string receipt_by { get; set; }
            public double total_payment { get; set; }
            public List<ReceiptProp> pay_for { get; set; }
            public string voucher_id { get; set; }
            public string ref_no { get; set; }
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


        [HttpGet]
        [Route("~/get/balance_sheet")]
        public IActionResult Balance_sheet(DateTime end_date)
         {
            try
            {
                string client_code = getClient();
                string trade_code =  getTrade();
               
             //User user = _unitOfWork.User.GetFirstOrDefault(u => u.user_id == GetUserId());
                var parameter = new DynamicParameters();
                parameter.Add("clientID",client_code);
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


                var result = new
                {
                    Asset = figures.Where(u => u.category == "A"),
                    Liability = figures.Where(u => u.category == "L"),
                    OwnersEquity = figures.Where(u => u.category == "C" || u.category == "E" || u.category == "I"),
                    TotalDebit = Math.Round(figures.Where(u => u.type == "Dr.").Sum(u => u.amount), 2),
                    TotalCredit = Math.Round(figures.Where(u => u.type == "Cr.").Sum(u => u.amount), 2)
                };



                return Json(new { success = true, message = result });



            }
            catch (Exception e)
            {

                return Json(new { success = false, message = e.Message });

            }


        }

        [HttpGet]
        [Route("~/Accounts/Payment/List")]
        public IActionResult BillPayList()
        {
            string client_code = getClient();
            string trade_code = getTrade();

            List<Ledger> ledgerList = _unitOfWork.Ledger.GetAll(u =>
            u.status == SD.PAID 
            && u.trx_info == SD.CREDIT
            && u.client_code == client_code 
            && u.trade_code == trade_code)
            .OrderByDescending(u => u.id)
            .ToList();


            List<PayTrans> payTransList = new List<PayTrans>();
            foreach (Ledger ledger in ledgerList)
            {
                PayTrans pt = new PayTrans();
                pt.payment_date = ledger.entry_date;
                pt.account_head_id =ledger.accounts_head_id;
                pt.payment_by = ledger.accounts_head_name;
                pt.total_payment = ledger.cr_total;
                pt.voucher_id = ledger.invoice;
                List<ProductEventInfo> peList = _unitOfWork.ProductEventInfo.GetAll(u => u.invoice == ledger.invoice
              && u.transaction_id == ledger.transaction_id
              && u.trade_code == trade_code
              && u.trx_info == SD.DEBIT).ToList();

                pt.pay_for = new List<PayProp>();
                foreach (ProductEventInfo productEventInfo in peList)
                {
                    PayProp pp = new PayProp();
                    pp.payment_head = productEventInfo.ac_head_name;
                    pp.amount = productEventInfo.dr_total;
                    pp.supplier_code = productEventInfo.supplier_code;
                    pp.description = productEventInfo.description;
                    pt.pay_for.Add(pp);

                }

                payTransList.Add(pt);
            }


            return Json(new { success = true, list = payTransList });
        }

        [HttpGet]
        [Route("~/Accounts/Receipt/List")]
        public IActionResult ReceiptList_i()
        {
            string client_code = getClient();
            string trade_code = getTrade();

            List<Ledger> ledgerList = _unitOfWork.Ledger.GetAll(u =>
            u.status == SD.RECIEVED
            && u.trx_info == SD.DEBIT
            && u.client_code == client_code
            && u.trade_code == trade_code)
            .OrderByDescending(u => u.id)
            .ToList();

            List<ReceiptTrans> receiptTransList = new List<ReceiptTrans>();
            foreach (Ledger ledger in ledgerList)
            {
                ReceiptTrans rt = new ReceiptTrans();

           
                rt.receipt_date = ledger.entry_date;
                rt.account_head_id = ledger.accounts_head_id;
                rt.receipt_by = ledger.accounts_head_name;
                rt.total_payment = ledger.dr_total;
                rt.voucher_id = ledger.invoice;
             
                List<ProductEventInfo> peList = _unitOfWork.ProductEventInfo.GetAll(u => u.invoice == ledger.invoice
             && u.transaction_id == ledger.transaction_id
             && u.trade_code == trade_code
             && u.trx_info == SD.CREDIT).ToList();
                rt.pay_for = new List<ReceiptProp>();
                foreach (ProductEventInfo productEventInfo in peList)
                {
                    ReceiptProp rp = new ReceiptProp();
                    rp.receipt_head = productEventInfo.ac_head_name;
                    rp.amount = productEventInfo.cr_total;
                    rp.customer_code = productEventInfo.customer_code;
                    rp.description = productEventInfo.description;
                    rt.pay_for.Add(rp);

                }

                receiptTransList.Add(rt);
            }


            return Json(new { success = true, list = receiptTransList });
        }
















        [HttpPost]
        [Route("~/Accounts/Payment/Submit")]
        public IActionResult BillPaySubmit_i([FromBody] PayTrans payTrans)
        {
            if (ModelState.IsValid)
            {


                if ( payTrans.pay_for == null)
                {
                    return Json(new { success = false, message = "Entry cannot be empty!" });
                }
                if (payTrans.pay_for.Count == 0 )
                {
                    return Json(new { success = false, message = "Entry cannot be empty!" });
                }
                if (payTrans.account_head_id == null)
                {
                    return Json(new { success = false, message = "Entry cannot be empty!" });
                }


                double chkBal = checkBalance(payTrans.account_head_id);
                double paid = payTrans.pay_for.Sum(u => u.amount);
                if (paid > chkBal)
                {
                    return Json(new { success = false, message = "Not enough balance in this account!" });
                }



                if (payTrans.payment_date == null)
                {
                    return Json(new { success = false, message = "Please set the Date!" });
                }

                AccountsHead acMedium = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == payTrans.account_head_id);
                Guid guid = Guid.NewGuid();
                string payTrx = guid.ToString();
                double total_amount = 0.00;
                string client_code = getClient();
                string trade_code = getTrade();
                string user_id = GetUserId();
                string invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);
                foreach (PayProp pp in payTrans.pay_for)
                {

                    //this part is for paid for (Debit)
                    ProductEventInfo productEventDr = new ProductEventInfo();
                    productEventDr.transaction_id = payTrx;
                    AccountsHead acPayment = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == pp.payment_name_id);
                    productEventDr.ac_head_id = acPayment.ac_head_id;
                    productEventDr.ac_head_name = acPayment.ac_head_name;
                    productEventDr.transaction_type = acPayment.ac_head_name;
                    if (pp.supplier_code != null)
                    {
                        productEventDr.supplier_code = pp.supplier_code;
                        productEventDr.supplier_name = _unitOfWork.Supplier.GetFirstOrDefault(u => u.code == pp.supplier_code).name;
                    }
                    productEventDr.label = acPayment.ac_head_name;
                    productEventDr.invoice = invoice;
                    productEventDr.entry_date = payTrans.payment_date.Date;
                    productEventDr.dr_amount = pp.amount;
                    productEventDr.dr_total = pp.amount;
                    productEventDr.dr_discount = productEventDr.dr_discount_percent = 0.00;
                    productEventDr.cr_amount = productEventDr.cr_discount_percent=0.00; productEventDr.cr_discount = 0.00; productEventDr.cr_total = 0.00;
                    productEventDr.user_id = user_id;
                    productEventDr.client_code = client_code;
                    productEventDr.entry_time = DateTime.Now;
                    productEventDr.trade_code = trade_code;
                    productEventDr.trx_info = SD.DEBIT;
                    productEventDr.returned = false;
                    productEventDr.status = SD.PAID;
                    productEventDr.ref_no = payTrans.ref_no;
                    productEventDr.description = pp.description;
                    _unitOfWork.ProductEventInfo.Add(productEventDr);



                    //this part is for paid for (credit)
                    ProductEventInfo productEventCr = new ProductEventInfo();
                    productEventCr.transaction_id = payTrx;
                    
                    productEventCr.ac_head_id = acMedium.ac_head_id;
                    productEventCr.ac_head_name = acMedium.ac_head_name;
                    productEventCr.transaction_type = acMedium.ac_head_name;
                    productEventCr.label = acPayment.ac_head_name;
                    productEventCr.invoice = invoice;
                    productEventCr.entry_date = payTrans.payment_date.Date;
                    productEventCr.cr_amount = pp.amount;
                    if (pp.supplier_code != null)
                    {
                        productEventCr.supplier_code = pp.supplier_code;
                        productEventCr.supplier_name = _unitOfWork.Supplier.GetFirstOrDefault(u => u.code == pp.supplier_code).name;
                    }
                    productEventCr.cr_total = pp.amount;
                    productEventCr.cr_discount = 0.00; productEventCr.cr_discount_percent = 0.00;
                    productEventCr.dr_amount = 0.00; productEventCr.dr_discount = 0.00; productEventCr.dr_discount_percent= 0.00; productEventCr.dr_total = 0.00;
                    productEventCr.user_id = user_id;
                    productEventCr.client_code = client_code;
                    productEventCr.entry_time = DateTime.Now;
                    productEventCr.trade_code = trade_code;
                    productEventCr.trx_info = SD.CREDIT;
                    productEventCr.returned = false;
                    productEventCr.status = SD.PAID;
                    productEventCr.ref_no = payTrans.ref_no;
                    productEventCr.description = pp.description;
                    _unitOfWork.ProductEventInfo.Add(productEventCr);


                    total_amount += pp.amount;


                }


                Ledger ledger = new Ledger();
                ledger.transaction_id = payTrx;
                ledger.accounts_head_id = acMedium.ac_head_id;
                ledger.accounts_head_name = acMedium.ac_head_name;
                ledger.transaction_type = ledger.accounts_head_name;
                ledger.invoice = invoice;
                ledger.entry_date = payTrans.payment_date.Date;
                ledger.cr_amount = total_amount;
                ledger.cr_total = total_amount;
                ledger.cr_discount = 0.00; ledger.dr_amount = 0.00; ledger.dr_discount = 0.00; ledger.dr_total = 0.00;
                ledger.user_id = user_id;
                ledger.client_code = client_code;
                ledger.entry_time = DateTime.Now;
                ledger.trade_code = trade_code; 
                ledger.trx_info = SD.CREDIT;//By logic of cash/bank/asset decrease 
                ledger.status = SD.PAID;
                _unitOfWork.Ledger.Add(ledger);

                _unitOfWork.Save();
                return Json(new { success = true, message = "Payment Entry Successful!", voucher_id = invoice });

            }




            return Json(new { success = false, message = "Failed to update Entry!" });

        }







        [HttpPost]
        [Route("~/Accounts/Receipt/Submit")]
        public IActionResult ReceipSubmit_i([FromBody] ReceiptTrans receiptTrans)
        {
            if (ModelState.IsValid)
            {


                if (receiptTrans.pay_for == null)
                {
                    return Json(new { success = false, message = "Entry cannot be empty!" });
                }
                if (receiptTrans.pay_for.Count == 0)
                {
                    return Json(new { success = false, message = "Entry cannot be empty!" });
                }
                if (receiptTrans.account_head_id == null)
                {
                    return Json(new { success = false, message = "Entry cannot be empty!" });
                }


                if (receiptTrans.receipt_date == null)
                {
                    return Json(new { success = false, message = "Please set the Date!" });
                }

                AccountsHead acMedium = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == receiptTrans.account_head_id);
                Guid guid = Guid.NewGuid();
                string payTrx = guid.ToString();
                double total_amount = 0.00;
                string client_code = getClient();
                string trade_code = getTrade();
                string user_id = GetUserId();
                string invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);
                foreach (ReceiptProp rp in receiptTrans.pay_for)
                {

                    //this part is for paid for (Credit)
                    ProductEventInfo productEventCr = new ProductEventInfo();
                    productEventCr.transaction_id = payTrx;
                    AccountsHead acReceipt = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == rp.receipt_name_id);
                    productEventCr.ac_head_id = acReceipt.ac_head_id;
                    productEventCr.ac_head_name = acReceipt.ac_head_name;
                    productEventCr.transaction_type = acReceipt.ac_head_name;
                    if (rp.customer_code != null)
                    {
                        productEventCr.customer_code = rp.customer_code;
                        productEventCr.customer_name = _unitOfWork.Customer.GetFirstOrDefault(u => u.code == rp.customer_code).name;
                    }
                    productEventCr.label = acReceipt.ac_head_name;
                    productEventCr.invoice = invoice;
                    productEventCr.entry_date = receiptTrans.receipt_date.Date;
                    productEventCr.cr_amount = rp.amount;
                    productEventCr.cr_total = rp.amount;
                    productEventCr.cr_discount = productEventCr.cr_discount_percent = 0.00;
                    productEventCr.dr_amount = productEventCr.dr_discount_percent = 0.00; productEventCr.dr_discount = 0.00; productEventCr.dr_total = 0.00;
                    productEventCr.user_id = user_id;
                    productEventCr.client_code = client_code;
                    productEventCr.entry_time = DateTime.Now;
                    productEventCr.trade_code = trade_code;
                    productEventCr.trx_info = SD.CREDIT;
                    productEventCr.returned = false;
                    productEventCr.status = SD.RECIEVED;
                    productEventCr.ref_no = receiptTrans.ref_no;
                    productEventCr.description = rp.description;
                    _unitOfWork.ProductEventInfo.Add(productEventCr);



                    //this part is for paid for (Debit)

                    ProductEventInfo productEventDr = new ProductEventInfo();
                    productEventDr.label = acReceipt.ac_head_name;
                    productEventDr.transaction_id = payTrx;
                    productEventDr.ac_head_id = acMedium.ac_head_id;
                    productEventDr.ac_head_name = acMedium.ac_head_name;
                    productEventDr.transaction_type = acMedium.ac_head_name;
                    productEventDr.invoice = invoice;
                    productEventDr.entry_date = receiptTrans.receipt_date.Date;
                    if (rp.customer_code != null)
                    {
                        productEventDr.customer_code = rp.customer_code;
                        productEventDr.customer_name = _unitOfWork.Customer.GetFirstOrDefault(u => u.code == rp.customer_code).name;
                    }
                    productEventDr.dr_amount = rp.amount;
                    productEventDr.dr_total = rp.amount;
                    productEventDr.dr_discount = 0.00; productEventDr.dr_discount_percent = 0.00;
                    productEventDr.cr_amount = 0.00; productEventDr.cr_discount = 0.00; productEventDr.cr_discount_percent = 0.00; productEventDr.cr_total = 0.00;
                    productEventDr.user_id = user_id;
                    productEventDr.client_code = client_code;
                    productEventDr.entry_time = DateTime.Now;
                    productEventDr.trade_code = trade_code;
                    productEventDr.trx_info = SD.DEBIT;
                    productEventDr.returned = false;
                    productEventDr.status = SD.RECIEVED;
                    productEventDr.ref_no = receiptTrans.ref_no;
                    productEventDr.description = rp.description;
                    _unitOfWork.ProductEventInfo.Add(productEventDr);


                    total_amount += rp.amount;


                }


                Ledger ledger = new Ledger();
                ledger.transaction_id = payTrx;
                ledger.accounts_head_id = acMedium.ac_head_id;
                ledger.accounts_head_name = acMedium.ac_head_name;
                ledger.transaction_type = ledger.accounts_head_name;
                ledger.invoice = invoice;
                ledger.entry_date = receiptTrans.receipt_date.Date;
                ledger.dr_amount = total_amount;
                ledger.dr_total = total_amount;
                ledger.dr_discount = 0.00; ledger.cr_amount = 0.00; ledger.cr_discount = 0.00; ledger.cr_total = 0.00;
                ledger.user_id = user_id;
                ledger.client_code = client_code;
                ledger.entry_time = DateTime.Now;
                ledger.trade_code = trade_code;
                ledger.trx_info = SD.DEBIT;//By logic of cash/bank/asset increase 
                ledger.status = SD.RECIEVED;
                _unitOfWork.Ledger.Add(ledger);

                _unitOfWork.Save();
                return Json(new { success = true, message = "Receipt Entry Successful!", voucher_id = invoice });

            }




            return Json(new { success = false, message = "Failed to update Entry!" });

        }

        public class Contra
        {
            public string ac_head_dr { get; set; }//from
            public string ac_head_name_dr { get; set; }//from
            public string ac_head_cr { get; set; }//to
            public string ac_head_name_cr { get; set; }//to
            public DateTime entry_date { get; set; }
            public double amount { get; set; }
            public string reference { get; set; }
            public string description { get; set; }

        }






        [HttpGet]
        [Route("~/Accounts/ContraList")]
        public IActionResult ContraList()
        {

            try
            {
                string client_code = getClient();
                string trade_code = getTrade();
                List<ProductEventInfo> peList = _unitOfWork.ProductEventInfo.GetAll(u => u.transaction_type == SD.CONTRA
             && u.client_code == client_code && u.trade_code == trade_code).ToList();
                List<string> trx_list = peList.Select(u => u.transaction_id).Distinct().ToList();
                List<Contra> contraList = new List<Contra>();
                foreach (string st in trx_list)
                {
                    ProductEventInfo pe_dr = peList.FirstOrDefault(u => u.transaction_id == st && u.trx_info == SD.DEBIT);
                    ProductEventInfo pe_cr = peList.FirstOrDefault(u => u.transaction_id == st && u.trx_info == SD.CREDIT); ;
                    Contra con = new Contra();
                    con.ac_head_cr = pe_cr.ac_head_id;
                    con.ac_head_name_cr = pe_cr.ac_head_name;
                    con.ac_head_dr = pe_dr.ac_head_id;
                    con.ac_head_name_dr = pe_dr.ac_head_name;
                    con.amount = pe_dr.dr_total;
                    con.description = pe_dr.description;
                    con.reference = pe_dr.ref_no;
                    con.entry_date = pe_dr.entry_date;


                    contraList.Add(con);
            

                }

                return Json(new { success = true, message = contraList });
            }
            catch
            {
                return Json(new { success = false, message = "No Record found!" });
            }

      }













        [HttpPost]
        [Route("~/Accounts/Contra")]
        public IActionResult ContraSubmit([FromBody]Contra contra )
        {
            try {
                if (contra.ac_head_dr ==null || contra.ac_head_cr == null)
                {
                    return Json(new { success = false, message = "This type of transactions are not allowed!" });
                }

                if (contra.ac_head_dr == contra.ac_head_cr)
                {
                    return Json(new { success = false, message = "This type of transactions are not allowed!" });
                }
                Guid guid = Guid.NewGuid();
                string payTrx = guid.ToString();
                string client_code = getClient();
                string trade_code = getTrade();
                string user_id = GetUserId();
                string invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);



                //this part is for Deposit (Debit)
                AccountsHead DebitHead = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.client_code == client_code && u.trade_code == trade_code && u.ac_head_id == contra.ac_head_dr);
                ProductEventInfo productEventDr = new ProductEventInfo();
                productEventDr.transaction_id = payTrx;
                productEventDr.ac_head_id = DebitHead.ac_head_id;
                productEventDr.ac_head_name = DebitHead.ac_head_name;
                productEventDr.label = SD.CONTRA;
                productEventDr.transaction_type = SD.CONTRA;
                productEventDr.invoice = invoice;
                productEventDr.entry_date = contra.entry_date.Date;
                productEventDr.dr_amount = contra.amount;
                productEventDr.dr_total = contra.amount;
                productEventDr.dr_discount = 0.00; productEventDr.dr_discount_percent = 0.00;
                productEventDr.cr_amount = 0.00; productEventDr.cr_discount = 0.00; productEventDr.cr_discount_percent = 0.00; productEventDr.cr_total = 0.00;
                productEventDr.user_id = user_id;
                productEventDr.client_code = client_code;
                productEventDr.entry_time = DateTime.Now;
                productEventDr.trade_code = trade_code;
                productEventDr.trx_info = SD.DEBIT;
                productEventDr.returned = false;
                productEventDr.status = SD.DEPOSIT;
                productEventDr.description = contra.description;
                productEventDr.ref_no = contra.reference;
                _unitOfWork.ProductEventInfo.Add(productEventDr);

                Ledger ledger = new Ledger();
                ledger.transaction_id = payTrx;
                ledger.accounts_head_id = DebitHead.ac_head_id;
                ledger.accounts_head_name = DebitHead.ac_head_name;
                ledger.transaction_type = SD.CONTRA;
                ledger.invoice = invoice;
                ledger.entry_date = contra.entry_date.Date;
                ledger.dr_amount = contra.amount;
                ledger.dr_total = contra.amount;
                ledger.dr_discount = 0.00; ledger.cr_amount = 0.00; ledger.cr_discount = 0.00; ledger.cr_total = 0.00;
                ledger.user_id = user_id;
                ledger.client_code = client_code;
                ledger.entry_time = DateTime.Now;
                ledger.trade_code = trade_code;
                ledger.trx_info = SD.DEBIT;//By logic of cash/bank/asset increase 
                ledger.status = SD.DEPOSIT;
                _unitOfWork.Ledger.Add(ledger);








                //this part is for Withdrawn (Credit)
                AccountsHead CreditHead = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.client_code == client_code && u.trade_code == trade_code && u.ac_head_id == contra.ac_head_cr);
                ProductEventInfo productEventCr = new ProductEventInfo();
                productEventCr.transaction_id = payTrx;
                productEventCr.ac_head_id = CreditHead.ac_head_id;
                productEventCr.ac_head_name = CreditHead.ac_head_name;
                productEventCr.label = SD.CONTRA;
                productEventCr.transaction_type = SD.CONTRA;
                productEventCr.invoice = invoice;
                productEventCr.entry_date = contra.entry_date.Date;
                productEventCr.cr_amount = contra.amount;
                productEventCr.cr_total = contra.amount;
                productEventCr.cr_discount = productEventCr.cr_discount_percent = 0.00;
                productEventCr.dr_amount = productEventCr.dr_discount_percent = 0.00; productEventCr.dr_discount = 0.00; productEventCr.dr_total = 0.00;
                productEventCr.user_id = user_id;
                productEventCr.client_code = client_code;
                productEventCr.entry_time = DateTime.Now;
                productEventCr.trade_code = trade_code;
                productEventCr.trx_info = SD.CREDIT;
                productEventCr.returned = false;
                productEventCr.status = SD.WITHDRAWN;
                productEventCr.description = contra.description;
                productEventCr.ref_no = contra.reference;
                _unitOfWork.ProductEventInfo.Add(productEventCr);

                Ledger ledger_cr = new Ledger();
                ledger_cr.transaction_id = payTrx;
                ledger_cr.accounts_head_id = CreditHead.ac_head_id;
                ledger_cr.accounts_head_name = CreditHead.ac_head_name;
                ledger_cr.transaction_type = SD.CONTRA;
                ledger_cr.invoice = invoice;
                ledger_cr.entry_date = contra.entry_date.Date;
                ledger_cr.cr_amount = contra.amount;
                ledger_cr.cr_total = contra.amount;
                ledger_cr.cr_discount = 0.00; ledger_cr.dr_amount = 0.00; ledger_cr.dr_discount = 0.00; ledger_cr.dr_total = 0.00;
                ledger_cr.user_id = user_id;
                ledger_cr.client_code = client_code;
                ledger_cr.entry_time = DateTime.Now;
                ledger_cr.trade_code = trade_code;
                ledger_cr.trx_info = SD.CREDIT;//By logic of cash/bank/asset decrease 
                ledger_cr.status = SD.WITHDRAWN;
                _unitOfWork.Ledger.Add(ledger_cr);


                _unitOfWork.Save();

                return Json(new { success = true, message = "successfully updated!" });

            }
            catch
            {
                return Json(new { success = false, message = "Invalid Input!" });
            }

          

        }





        public class JournalProp
        {
            public string ac_head_id { get; set; }
            public string ac_head_name { get; set; }
            public double amount { get; set; }
            public string description { get; set; }
        }

        public class JournalTrans
        {
            public DateTime journal_date { get; set; }
            public List<JournalProp> journal_debit { get; set; }
            public List<JournalProp> journal_credit { get; set; }
            public string description { get; set; }
            public string reference { get; set; }
        }



        [HttpGet]
        [Route("~/Accounts/Journal/History")]
        public IActionResult journalHistory(DateTime history_month)
        {


            try
            {


                List<JournalTrans> jtList = new List<JournalTrans>();
                string trade_code = getTrade();
                string client_code = getClient();
                List<ProductEventInfo> peList = new List<ProductEventInfo>();
                peList = _unitOfWork.ProductEventInfo.GetAll(u =>
                u.status == SD.JOURNAL
                && u.client_code == client_code
                && u.trade_code == trade_code
             && u.entry_date.Month == history_month.Month
              && u.entry_date.Year == history_month.Year
                 ).ToList();
                if (peList.Count == 0)
                {
                    return Json(new { success = false, message = "No Record Found!" });
                }

                List<string> trxidList = peList.Select(u => u.transaction_id).Distinct().ToList();

                foreach (string trxid in trxidList)
                {

                    JournalTrans jt = new JournalTrans();
                    List<ProductEventInfo> debitList = peList.Where(u => u.transaction_id == trxid && u.trx_info == SD.DEBIT).ToList();
                    List<ProductEventInfo> creditList = peList.Where(u => u.transaction_id == trxid && u.trx_info == SD.CREDIT).ToList();

                    jt.journal_date = debitList.First().entry_date;
                    jt.reference = debitList.First().ref_no;
                    jt.description = debitList.First().description;
                    jt.journal_debit = (from debit in debitList
                                      //  where debit.transaction_id == trxid && debit.trx_info == SD.DEBIT
                                        select (new JournalProp()
                                        {
                                            ac_head_id = debit.ac_head_id,
                                            ac_head_name = debit.ac_head_name,
                                            amount = debit.dr_total,
                                            description = debit.description

                                        })).ToList();
                    jt.journal_credit = (from credit in creditList
                                       //  where credit.transaction_id == trxid && credit.trx_info == SD.DEBIT
                                         select (new JournalProp()
                                         {
                                             ac_head_id = credit.ac_head_id,
                                             ac_head_name = credit.ac_head_name,
                                             amount = credit.cr_total,
                                             description = credit.description

                                         })).ToList();

                    jtList.Add(jt);



                }
                return Json(new { success = true, message = jtList });


            }
            catch
            {
                return Json(new { success = false, message = "Something went wrong!" });
            }

       



        }




        [HttpPost]
        [Route("~/Accounts/Journal")]
        public IActionResult journalSubmit([FromBody] JournalTrans journalTrans)
        {
            try
            {


                if (journalTrans.journal_debit == null || journalTrans.journal_credit == null)
                {
                    return Json(new { success = false, message = "This type of transactions are not allowed!" });
                }
                if (journalTrans.journal_debit.Count == 0 || journalTrans.journal_credit.Count == 0)
                {
                    return Json(new { success = false, message = "This type of transactions are not allowed!" });
                }

                double total_debit = journalTrans.journal_debit.Sum(u => u.amount);
                double total_credit = journalTrans.journal_credit.Sum(u => u.amount);
                if (total_credit != total_debit)
                {
                    return Json(new { success = false, message = "Total debit is not equal to total credit!" });
                }


                Guid guid = Guid.NewGuid();
                string payTrx = guid.ToString();
                string client_code = getClient();
                string trade_code = getTrade();
                string user_id = GetUserId();
                string invoice = _unitOfWork.ProductStock.setInvoiceNo(trade_code);


                //this part is for(Debit)
                foreach (JournalProp jp in journalTrans.journal_debit)
                {
                   
                    ProductEventInfo productEventDr = new ProductEventInfo();
                    productEventDr.transaction_id = payTrx;
                    AccountsHead ah = _unitOfWork.AccountsHead.GetFirstOrDefault(u=>u.ac_head_id == jp.ac_head_id && u.trade_code == trade_code);
                    productEventDr.ac_head_id = ah.ac_head_id;
                    productEventDr.ac_head_name = ah.ac_head_name;
                    productEventDr.label = SD.JOURNAL;
                    productEventDr.transaction_type = SD.JOURNAL;
                    productEventDr.invoice = invoice;
                    productEventDr.entry_date = journalTrans.journal_date.Date;
                    productEventDr.dr_amount = jp.amount;
                    productEventDr.dr_total = jp.amount;
                    productEventDr.dr_discount = 0.00; productEventDr.dr_discount_percent = 0.00;
                    productEventDr.cr_amount = 0.00; productEventDr.cr_discount = 0.00; productEventDr.cr_discount_percent = 0.00; productEventDr.cr_total = 0.00;
                    productEventDr.user_id = user_id;
                    productEventDr.client_code = client_code;
                    productEventDr.entry_time = DateTime.Now;
                    productEventDr.trade_code = trade_code;
                    productEventDr.trx_info = SD.DEBIT;
                    productEventDr.returned = false;
                    productEventDr.status = SD.JOURNAL;
                    productEventDr.description = journalTrans.description;
                    productEventDr.ref_no = journalTrans.reference;
                    _unitOfWork.ProductEventInfo.Add(productEventDr);



                    if(ah.ac_group_id == "01")
                    {
                        Ledger ledger = new Ledger();
                        ledger.transaction_id = payTrx;
                        ledger.accounts_head_id = ah.ac_head_id;
                        ledger.accounts_head_name = ah.ac_head_name;
                        ledger.transaction_type = SD.JOURNAL;
                        ledger.invoice = invoice;
                        ledger.entry_date = journalTrans.journal_date.Date;
                        ledger.dr_amount = jp.amount;
                        ledger.dr_total = jp.amount;
                        ledger.dr_discount = 0.00; ledger.cr_amount = 0.00; ledger.cr_discount = 0.00; ledger.cr_total = 0.00;
                        ledger.user_id = user_id;
                        ledger.client_code = client_code;
                        ledger.entry_time = DateTime.Now;
                        ledger.trade_code = trade_code;
                        ledger.trx_info = SD.DEBIT; //By logic of cash/bank/asset increase 
                        ledger.status = SD.RECIEVED;
                        _unitOfWork.Ledger.Add(ledger);
                    }



                }







                //this part is for  (Credit)
                foreach (JournalProp jp in journalTrans.journal_credit)
                {
                    ProductEventInfo productEventCr = new ProductEventInfo();
                    productEventCr.transaction_id = payTrx;
                    AccountsHead ah = _unitOfWork.AccountsHead.GetFirstOrDefault(u => u.ac_head_id == jp.ac_head_id && u.trade_code == trade_code);
                    productEventCr.ac_head_id = ah.ac_head_id;
                    productEventCr.ac_head_name = ah.ac_head_name;
                    productEventCr.label = SD.JOURNAL;
                    productEventCr.transaction_type = SD.JOURNAL;
                    productEventCr.invoice = invoice;
                    productEventCr.entry_date = journalTrans.journal_date.Date;
                    productEventCr.cr_amount = jp.amount;
                    productEventCr.cr_total = jp.amount;
                    productEventCr.cr_discount = productEventCr.cr_discount_percent = 0.00;
                    productEventCr.dr_amount = productEventCr.dr_discount_percent = 0.00; productEventCr.dr_discount = 0.00; productEventCr.dr_total = 0.00;
                    productEventCr.user_id = user_id;
                    productEventCr.client_code = client_code;
                    productEventCr.entry_time = DateTime.Now;
                    productEventCr.trade_code = trade_code;
                    productEventCr.trx_info = SD.CREDIT;
                    productEventCr.returned = false;
                    productEventCr.status = SD.JOURNAL;
                    productEventCr.description = journalTrans.description;
                    productEventCr.ref_no = journalTrans.reference;
                    _unitOfWork.ProductEventInfo.Add(productEventCr);



                    if (ah.ac_group_id == "01")
                    {
                        Ledger ledger_cr = new Ledger();
                        ledger_cr.transaction_id = payTrx;
                        ledger_cr.accounts_head_id = jp.ac_head_id;
                        ledger_cr.accounts_head_name = jp.ac_head_name;
                        ledger_cr.transaction_type = SD.JOURNAL;
                        ledger_cr.invoice = invoice;
                        ledger_cr.entry_date = journalTrans.journal_date.Date;
                        ledger_cr.cr_amount = jp.amount;
                        ledger_cr.cr_total = jp.amount;
                        ledger_cr.cr_discount = 0.00; ledger_cr.dr_amount = 0.00; ledger_cr.dr_discount = 0.00; ledger_cr.dr_total = 0.00;
                        ledger_cr.user_id = user_id;
                        ledger_cr.client_code = client_code;
                        ledger_cr.entry_time = DateTime.Now;
                        ledger_cr.trade_code = trade_code;
                        ledger_cr.trx_info = SD.CREDIT;//By logic of cash/bank/asset decrease 
                        ledger_cr.status = SD.PAID;
                        _unitOfWork.Ledger.Add(ledger_cr);
                    }
                }
               

            


                _unitOfWork.Save();

                return Json(new { success = true, message = "successfully updated!" });

            }
            catch
            {
                return Json(new { success = false, message = "Invalid Input!" });
            }



        }

































    }
}
