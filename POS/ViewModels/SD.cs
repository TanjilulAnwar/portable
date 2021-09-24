using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.ViewModels
{
    public static class SD
    {
        public static string SERVER = "http://localhost:44319";
        public static string PAID = "PAID";
        public static string RECIEVED = "RECIEVED";
        public static string WITHDRAWN = "WITHDRAWN";
        public static string DEPOSIT = "DEPOSIT";
        public static string CONTRA = "CONTRA";
        public static string DUE = "DUE";
        public static string DEBIT = "Dr.";
        public static string CREDIT = "Cr.";
        public static string CASH = "01020000";
        public static string AC_PAYABLE = "03010000";
        public static string AC_RECEIVABLE = "11010000";
        public static string INVENTORY_COST = "08010000";
        public static string SALES_REVENUE = "06010000";
        public static string CUSTOMER_RETURNS = "08040000";
        public static string RETURNED_GOODS = "06020000";

    }
}
