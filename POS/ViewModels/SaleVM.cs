using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.ViewModels
{
    public class SaleVM
    {
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string account_head_id { get; set; }
        public string transaction_id { get; set; }
        public DateTime entry_date { get; set; }
        public DateTime entry_time { get; set; }
        public string invoice { get; set; }
        public string receive_type { get; set; }
        public double total { get; set; }
        public double payment { get; set; }
        public double grand_total { get; set; }
        public double due { get; set; }
        public double discount { get; set; }
        public double discount_p { get; set; }
        public bool percent { get; set; }
        public bool returned { get; set; }
        public bool g_customer { get; set; }
        public string payment_type { get; set; }
        public List<ProductObject> sales_list { get; set; }

    }
}
