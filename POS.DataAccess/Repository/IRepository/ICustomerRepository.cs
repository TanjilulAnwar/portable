using POS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DataAccess.Repository.IRepository
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        public void Update(Customer customer);
        public string getCustomerCode(string client_code, string trade_code);
    }
}
