using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.ViewModels
{
    public class AccountsName
    {

        public int id { get; set; }
        public string ac_group_id { get; set; }
        public string ac_head_id { get; set; }
        public string ac_head_name { get; set; }
        public string description { get; set; }
        public bool ac_status { get; set; }

    }
}
