using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;

namespace POS.Controllers
{
    public class ManufacturerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public ManufacturerController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
        }


        public JsonResult getAll()
        {
            string trade_code = getTrade();
            string client_code = getClient();
            IEnumerable<Manufacturer> list = _unitOfWork.Manufacturer.GetAll(u=>u.client_code == client_code && u.trade_code ==  trade_code);
            return Json(new { success = true, message = list });
        }

        [Route("~/Manufacturer/")]
        [Route("~/Manufacturer/index")]
        public IActionResult Manufacturer_Index()
        {
            return getAll();
        }

        [HttpPost]
        [Route("~/Manufacturer/add")]
        public IActionResult Upsert([FromBody] Manufacturer manufacturer)
        {

            if (ModelState.IsValid)
            {
                string trade_code = getTrade();
                string client_code = getClient();
                if (manufacturer.id == 0)
                {
                   
                    string m_code = _unitOfWork.Manufacturer.getManufacturerCode();
                    manufacturer.code = m_code;
                    manufacturer.name = manufacturer.name.ToUpper();
                    manufacturer.brand = manufacturer.brand.ToUpper();
                    manufacturer.contact_person = manufacturer.contact_person.ToUpper();
                    manufacturer.address = manufacturer.address.ToUpper();
                    manufacturer.entry_date = DateTime.Now.Date;
                    manufacturer.entry_by = GetUserId();
                    manufacturer.client_code = client_code;
                    manufacturer.trade_code = trade_code;
                    _unitOfWork.Manufacturer.Add(manufacturer);
                    POSLog pOSLog = _unitOfWork.POSLog.GetFirstOrDefault(u=>u.client_code == client_code && u.trade_code == trade_code);
                    pOSLog.manufacturer_code = m_code;
                    _unitOfWork.POSLog.Update(pOSLog);
                }
                else
                {
                    manufacturer.name = manufacturer.name.ToUpper();
                    manufacturer.brand = manufacturer.brand.ToUpper();
                    manufacturer.contact_person = manufacturer.contact_person.ToUpper();
                    manufacturer.address = manufacturer.address.ToUpper();
                    manufacturer.entry_date = DateTime.Now.Date;
                    manufacturer.entry_by = GetUserId();
                    _unitOfWork.Manufacturer.Update(manufacturer);
                }

                _unitOfWork.Save();
                return Ok(manufacturer);
            }
            else
            {
                return Json(new { success = false, message = "Add failed!!" });
            }

        }
        [HttpDelete]
        [Route("Manufacturer/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Manufacturer.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Manufacturer.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}
