using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CustomerController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }


        public double CustomerDue(string customer_code, string client_code, string trade_code)
        {
            double due = 0.0;
            var parameter = new DynamicParameters();
            parameter.Add("CustomerCode", customer_code);
            parameter.Add("ClientCode", client_code);
            parameter.Add("TradeCode", trade_code);

            due = _unitOfWork.SP_Call.Single<double>("Customer_Due", parameter);
            return due;
        }









        [HttpGet]
        [Route("~/Customer/search")]
        public IActionResult customer_search(string search_string)
        {

            try
            {
                string client_code = getClient();
                string trade_code = getTrade();
                List<Customer> cusList = new List<Customer>();
                if (search_string == null)
                {

                    cusList = _unitOfWork.Customer.GetAll(u => u.client_code == client_code && u.trade_code == trade_code).OrderBy(u => u.name).ToList();
                    if (cusList.Count() == 0)
                    {
                        return Json(new { success = false });
                    }
                    var customerx = from p in cusList
                                    select (new
                                    {
                                        p.code,
                                        p.name,
                                        p.mobile,
                                        p.company,
                                        due = CustomerDue(p.code, client_code, trade_code)
                                    });

                    return Json(new { success = true, message = customerx });

                }

                search_string = search_string.ToUpper();
                cusList = _unitOfWork.Customer.GetAll(u => (u.name.ToUpper().Contains(search_string) || u.mobile.Contains(search_string) || u.code.Contains(search_string)) && u.client_code == client_code && u.trade_code == trade_code).ToList();
                if (cusList.Count() == 0)
                {
                    return Json(new { success = false });
                }


                var customers = from p in cusList select (new { p.code, p.name, p.mobile, p.company, p.patient_id, p.address, due = CustomerDue(p.code, client_code, trade_code) });

                return Json(new { success = true, message = customers });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });

            }
        }













        public JsonResult getAll()
        {
            string client_code = getClient();
            string trade_code = getTrade();
            IEnumerable<Customer> list = _unitOfWork.Customer.GetAll(u => u.client_code == client_code && u.trade_code == trade_code);
            return Json(new { success = true, message = list });
        }

        [Route("~/Customer/")]
        [Route("~/Customer/index")]
        public IActionResult Customer_Index()
        {
            return getAll();
        }
        [HttpPost]
        [Route("~/Customer/add")]
        public IActionResult Upsert([FromBody] Customer customer)
        {

            if (ModelState.IsValid)
            {
                if (customer.id == 0)
                {
                    string client_code = getClient();
                    string trade_code = getTrade();
                    string c_code = _unitOfWork.Customer.getCustomerCode(client_code,trade_code);
                    customer.code = c_code;
                    customer.name = customer.name.ToUpper();

                    customer.company = customer.company==null?null: customer.company.ToUpper();
                    customer.address = customer.address == null ? null : customer.address.ToUpper();
                    customer.entry_date = DateTime.Now.Date;
                    customer.entry_by = GetUserId();
                    customer.client_code = client_code;
                    customer.trade_code = trade_code;
                    _unitOfWork.Customer.Add(customer);



                    POSLog pOSLog = _unitOfWork.POSLog.GetFirstOrDefault();
                    pOSLog.customer_code = c_code;
                    _unitOfWork.POSLog.Update(pOSLog);
                }
                else
                {
                    customer.name = customer.name.ToUpper();
                    customer.company = customer.company == null ? null : customer.company.ToUpper();
                    customer.address = customer.address == null ? null : customer.address.ToUpper();
                    customer.entry_date = DateTime.Now.Date;
                    customer.entry_by = GetUserId();
                    _unitOfWork.Customer.Update(customer);
                }



                _unitOfWork.Save();
                return Ok(customer);
            }
            else
            {
                return Json(new { success = false, message = "Add failed!!" });
            }

        }
        [HttpDelete]
        [Route("customer/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Supplier.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Supplier.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}
