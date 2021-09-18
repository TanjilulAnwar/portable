using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Models.Models
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string division { get; set; }
        public string district { get; set; }
        public string thana { get; set; }
        public string email { get; set; }
        public string zipcode { get; set; }
        public string logo { get; set; }
    }
}
