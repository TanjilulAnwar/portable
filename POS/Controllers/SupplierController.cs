using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
namespace POS.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public SupplierController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [Route("~/Supplier/search")]
        public IActionResult supplier_search(string search_string)
        {

            try
            {
                string client_code = getClient();
                string trade_code = getTrade();
                List<Supplier> supList = new List<Supplier>();

                if (search_string == null)
                {

                    supList = _unitOfWork.Supplier.GetAll(u => u.client_code == client_code && u.trade_code == trade_code).OrderBy(u => u.name).ToList();
                    if (supList.Count() == 0)
                    {
                        return Json(new { success = false });
                    }
                    var supplierx = from p in supList
                                    select (new
                                    {
                                        p.code,
                                        p.name,
                                        p.mobile,
                                        p.company,
                                        due = SupplierDue(p.code, client_code, trade_code)
                                    });

                    return Json(new { success = true, message = supplierx });

                }

                search_string = search_string.ToUpper();
                supList = _unitOfWork.Supplier.GetAll(u => (u.name.ToUpper().Contains(search_string) || u.mobile.Contains(search_string) || u.code.Contains(search_string)) && u.client_code == client_code && u.trade_code == trade_code).OrderBy(u => u.name).ToList();
                if (supList.Count() == 0)
                {
                    return Json(new { success = false });
                }


                var suppliers = from p in supList
                                select (new
                                {
                                    p.code,
                                    p.name,
                                    p.mobile,
                                    p.company,
                                    due = SupplierDue(p.code, client_code, trade_code)
                                });

                return Json(new { success = true, message = suppliers });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });

            }
        }



        public double SupplierDue(string supplier_code, string client_code, string trade_code)
        {
            double due = 0.0;
            var parameter = new DynamicParameters();
            parameter.Add("SupplierCode", supplier_code);
            parameter.Add("ClientCode", client_code);
            parameter.Add("TradeCode", trade_code);

            due = _unitOfWork.SP_Call.Single<double>("Supplier_Due", parameter);
            return due;
        }







        [Route("/")]
        [Route("~/index")]
        public IActionResult Index()
        {
            return Ok("Welcome Home!");
        }

      
        [Route("~/area")]
        public IActionResult GetArea()
        {
            //https://www.youtube.com/watch?v=SholKTNGdHk&ab_channel=RazorCXTechnologies
            string webRootPath = _hostEnvironment.WebRootPath;
            string j;
            var uploads = Path.Combine(webRootPath, @"area.json");
            using (var reader = new StreamReader(uploads))
            {
                j = reader.ReadToEnd();
            }
            return Ok(j);

        }


        public JsonResult getAll()
        {
            string client_code = getClient();
            string trade_code = getTrade();
            IEnumerable<Supplier> list = _unitOfWork.Supplier.GetAll(u=>u.client_code ==  client_code && u.trade_code ==  trade_code);
            return Json(new { success = true, message = list });
        }

        [Route("~/supplier/")]
        [Route("~/supplier/index")]
        public IActionResult supplier_Index()
        {
            return getAll();
        }
        [HttpPost]
        [Route("~/supplier/add")]
        public IActionResult Upsert([FromBody]Supplier supplier)
        {

            if (ModelState.IsValid)
            {
                if (supplier.id == 0)
                {
                    string client_code = getClient();
                    string trade_code = getTrade();
                    string s_code = _unitOfWork.Supplier.getSupplierCode(client_code,trade_code);
                    supplier.code = s_code;
                    supplier.name = supplier.name.ToUpper();
                    supplier.company = supplier.company.ToUpper();
                    supplier.address = supplier.address.ToUpper();
                    supplier.entry_date = DateTime.Now.Date;
                    supplier.entry_by = "ADMIN";
                    supplier.client_code = client_code;
                    supplier.trade_code = trade_code;
                    _unitOfWork.Supplier.Add(supplier);
                    POSLog pOSLog = _unitOfWork.POSLog.GetFirstOrDefault();
                    pOSLog.supplier_code = s_code;
                    _unitOfWork.POSLog.Update(pOSLog);
                }
                else
                {
                    supplier.name = supplier.name.ToUpper();
                    supplier.company = supplier.company.ToUpper();
                    supplier.address = supplier.address.ToUpper();
                    supplier.entry_date = DateTime.Now.Date;
                    supplier.entry_by = "ADMIN";
                    _unitOfWork.Supplier.Update(supplier);
                }

                

                _unitOfWork.Save();
                return Ok(supplier);
            }
            else
            {
                return Json(new { success = false, message = "Add failed!!" });
            }
           
        }
        [HttpDelete]
        [Route("supplier/delete/{id}")]
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
