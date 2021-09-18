using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using POS.ViewModels;

namespace POS.Controllers
{
    public class AccountsHeadController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public AccountsHeadController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        [HttpGet]
        [Route("~/AccountsHead/dropdown")]
        public IActionResult ACHAllList()
        {
            string client_code = getClient();
            string trade_code = getTrade();
            List<AccountsHead> achList = _unitOfWork.AccountsHead.GetAll(u => u.client_code == client_code && u.trade_code == trade_code ).ToList();
            var dropdown = from ach in achList select (new { ac_head_id = ach.ac_head_id, ac_head_name = ach.ac_head_name });
            return Json(new { success = true, message = dropdown });
        }

        [HttpGet]
        [Route("~/AccountsHead/List")]
        public IActionResult ACHList()
        {
            string client_code = getClient();
            string trade_code = getTrade();
            List<AccountsHead> achList = _unitOfWork.AccountsHead.GetAll(u => u.client_code == client_code && u.trade_code == trade_code && u.main_sub == "M").ToList();
            return Json(new { success = true, message = achList });
        }

        [HttpGet]
        [Route("~/AccountsName/List")]
        public IActionResult ACNList()
        {
            string client_code = getClient();
            string trade_code = getTrade();
            List<AccountsHead> achList = _unitOfWork.AccountsHead.GetAll(u => u.client_code == client_code && u.trade_code == trade_code && u.main_sub =="S").ToList();
            return Json(new { success = true, message = achList });
        }


        public class AcReport
        {
            public string ac_group_name { get; set; }
            public string ac_group_id { get; set; }
            public List<AccountsHead> AcHeadList { get; set; }
        }


        [HttpGet]
        [Route("~/AccountsHead/GroupBy")]
        public IActionResult ACHGroupBy()
        {
            try
            {

                string client_code = getClient();
                string trade_code = getTrade();
                List<AccountsHead> acHeads = _unitOfWork.AccountsHead.GetAll(u => u.client_code == client_code && u.trade_code == trade_code).ToList();
                List<AcReport> groupedList = acHeads.GroupBy(i => i.ac_group_id)
    .Select(j => new AcReport()
    {
        ac_group_id = j.First().ac_group_id,
        ac_group_name = j.First().ac_group_name,
        AcHeadList = j.Select(f => new AccountsHead()
        {
            ac_head_name = f.ac_head_name,
            ac_head_id = f.ac_head_id
        }).ToList()
    })
    .ToList();

                return Json(new { success = true, message = groupedList });
            }
            catch
            {
                return Json(new { success = false, message = "Error in fetching AccountHeads" });
            }

            }



        [HttpPost]
        [Route("~/AccountsHead/add")]
        public IActionResult Upsert([FromBody] AccountsHead accountsHead)
        {

            if (ModelState.IsValid)
            {
                string client_code = getClient();
                string trade_code = getTrade();
                if (accountsHead.id == 0)
                {
                    AccountsGroup ag = _unitOfWork.AccountsGroup.GetFirstOrDefault(u => u.ac_group_id == accountsHead.ac_group_id);
                    accountsHead.ac_head_id = _unitOfWork.AccountsHead._setAccountsHeadID(accountsHead.ac_group_id,client_code);
                    accountsHead.control_type = ag.control_type;
                    accountsHead.ac_group_name = ag.ac_group_name;
                    accountsHead.ac_name_head_id = accountsHead.ac_head_id;
                    accountsHead.main_sub = "M";
                    accountsHead.client_code = client_code;
                    accountsHead.trade_code = trade_code;
                    _unitOfWork.AccountsHead.Add(accountsHead);
             

                }
                else
                {
                    if( accountsHead.id <= 106)
                    {
                        return Json(new { success = false, message = "This account head is set up by system and therefore cannot be updated!" });
                    }
                    AccountsGroup ag = _unitOfWork.AccountsGroup.GetFirstOrDefault(u => u.ac_group_id == accountsHead.ac_group_id);
                    accountsHead.ac_head_id = _unitOfWork.AccountsHead._setAccountsHeadID(accountsHead.ac_group_id, client_code);
                    accountsHead.ac_name_head_id = accountsHead.ac_head_id;
                    accountsHead.control_type = ag.control_type;
                    accountsHead.ac_group_name = ag.ac_group_name;
                    accountsHead.main_sub = "M";
                    accountsHead.client_code = client_code;
                    accountsHead.trade_code = trade_code;
                    _unitOfWork.AccountsHead.Update(accountsHead);

                }
                _unitOfWork.Save();
                return Json(new { success = true, message = accountsHead });

            }
            else
            {
                return Json(new { success = false, message = "Add failed!!" });
            }

        }




       



        [HttpPost]
        [Route("~/AccountsName/add")]
        public IActionResult AccountsNameUpsert([FromBody] AccountsName accountsName)
        {

            if (ModelState.IsValid)
            {
                string client_code = getClient();
                string trade_code = getTrade();

                AccountsHead accountsHead = new AccountsHead();

                if (accountsHead.id == 0)
                {
                    AccountsGroup ag = _unitOfWork.AccountsGroup.GetFirstOrDefault(u => u.ac_group_id == accountsName.ac_group_id);
                    accountsHead.ac_head_id = _unitOfWork.AccountsHead._setAccountsNameID(accountsName.ac_head_id, client_code,trade_code);
                  
                    accountsHead.ac_head_name = accountsName.ac_head_name;
                    accountsHead.control_type = ag.control_type;
                    accountsHead.ac_group_name = ag.ac_group_name;
                    accountsHead.ac_group_id = ag.ac_group_id;
                    accountsHead.ac_type = ag.ac_type;
                    accountsHead.ac_name_head_id = accountsName.ac_head_id;
                    accountsHead.main_sub = "S";
                    accountsHead.description = accountsName.description;
                    accountsHead.ac_status = accountsName.ac_status;
                    accountsHead.client_code = client_code;
                    accountsHead.trade_code = trade_code;
                    _unitOfWork.AccountsHead.Add(accountsHead);


                }
                else
                {
                    if (accountsHead.id <= 106)
                    {
                        return Json(new { success = false, message = "This account head is set up by system and therefore cannot be updated!" });
                    }
                    AccountsGroup ag = _unitOfWork.AccountsGroup.GetFirstOrDefault(u => u.ac_group_id == accountsHead.ac_group_id);
                    accountsHead.ac_head_id = _unitOfWork.AccountsHead._setAccountsNameID(accountsHead.ac_head_id, client_code,trade_code);
                    accountsHead.ac_name_head_id = accountsName.ac_head_id;
                    accountsHead.ac_head_name = accountsName.ac_head_name;
                    accountsHead.control_type = ag.control_type;
                    accountsHead.ac_group_name = ag.ac_group_name;
                    accountsHead.description = accountsName.description;
                    accountsHead.ac_status = accountsName.ac_status;
                    accountsHead.main_sub = "S";
                    accountsHead.client_code = client_code;
                    accountsHead.trade_code = trade_code;
                    _unitOfWork.AccountsHead.Update(accountsHead);

                }
                _unitOfWork.Save();
                return Json(new { success = true, message = accountsHead });

            }
            else
            {
                return Json(new { success = false, message = "Add failed!!" });
            }

        }















    }
}
