using POS.DataAccess.Data;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    


        public string getCustomerCode(string client_code,string trade_code)
        {
            POSLog pOSLog = _db.Pos_log.FirstOrDefault(u => u.client_code== client_code &&  u.trade_code == trade_code);
            string customer_code;
            if (pOSLog == null)
            {
                POSLog pOSLog_new = new POSLog();
                pOSLog_new.trade_code = trade_code;
                _db.Pos_log.Add(pOSLog_new);

            }
            if (pOSLog.customer_code == null)
            {
                customer_code = "CUS00001";

            }

            else
            {
                string temp = pOSLog.customer_code.Substring(3, 5);
                int code_no = Convert.ToInt32(temp);
                code_no++;
                string s = code_no.ToString("00000");
                customer_code = "CUS" + s;

            }
            return customer_code;

        }








        public void Update(Customer customer)
        {
            _db.Customers_info.Update(customer);
        }
    }
}
