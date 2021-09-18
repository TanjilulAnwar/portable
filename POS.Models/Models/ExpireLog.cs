using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Models.Models
{
    public class ExpireLog
    {
        public int id { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public double quantity { get; set; }
        public DateTime expire_date { get; set; }
        public string batch_no { get; set; }
        public string client_code { get; set; }
        public string trade_code { get; set; }

    }
}
