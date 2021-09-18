using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Controllers
{
    //THIS IS PHARMACY
    public class HomeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public class ExpireProduct
        {

            public string product_code { get; set; }
            public string product_name { get; set; }
            public double quantity { get; set; }
            public DateTime expire_date { get; set; }
            public string batch_no { get; set; }

           

        }

        public class ExpireProp
        {


            public DateTime date { get; set; }

            public List<ExpireProduct> product_list { get; set; }

        }

        [HttpGet]
        [Route("~/Home/ExpiredNotice")]
        public IActionResult ExpiredNotice()
        {


            try
            {
                // throw new InvalidOperationException("Logfile cannot be read-only");
                string client_code = getClient();
                string trade_code =  getTrade();
                string today = DateTime.Now.ToString("yyyy-MM-dd");

                var parameter = new DynamicParameters();

                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("endDate", today);


                List<ExpireProduct> ToBeExpired = new List<ExpireProduct>();
                ToBeExpired = _unitOfWork.SP_Call.List<ExpireProduct>("expired_notice", parameter).ToList();
                if(ToBeExpired.Count == 0)
                {
                    return Json(new { success = true, message = ToBeExpired });
                }

                List<DateTime> unique_dates = (from prod in ToBeExpired
                                               select prod.expire_date).Distinct().ToList();

                List<ExpireProp> expireProps = new List<ExpireProp>();

                foreach (DateTime date in unique_dates)
                {

                    ExpireProp expireProp = new ExpireProp();
                    expireProp.date = date;
                    expireProp.product_list = new List<ExpireProduct>();
                    expireProp.product_list = ToBeExpired.Where(u => u.expire_date == date).ToList();
                    expireProps.Add(expireProp);

                }


                return Json(new { success = true, message = expireProps });



            }
            catch
            {
                return Json(new { success = false, message = "something went wrong!" });
            }


        }




        [HttpGet]
        [Route("~/Home/Expired")]
        public IActionResult Expired()
        {


            try
            {
                // throw new InvalidOperationException("Logfile cannot be read-only");
                string client_code = getClient();
                string trade_code = getTrade();
                string today = DateTime.Now.ToString("yyyy-MM-dd");

                var parameter = new DynamicParameters();

                parameter.Add("clientID", client_code);
                parameter.Add("tradeID", trade_code);
                parameter.Add("endDate", today);


                List<ExpireProduct> ToBeExpired = new List<ExpireProduct>();
                ToBeExpired = _unitOfWork.SP_Call.List<ExpireProduct>("already_expired", parameter).ToList();
             


                return Json(new { success = true, message = ToBeExpired });


            }
            catch
            {
                return Json(new { success = false, message = "something went wrong!" });
            }


        }













    }



}
