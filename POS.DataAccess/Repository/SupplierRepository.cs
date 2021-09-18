using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.DataAccess.Data;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;


namespace POS.DataAccess.Repository
{
    public class SupplierRepository:Repository<Supplier>,ISupplierRepository
    {
        private readonly ApplicationDbContext _db;

        public SupplierRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public string getSupplierCode(string client_code,string trade_code)
        {
            POSLog pOSLog = _db.Pos_log.FirstOrDefault(u=>u.client_code == client_code && u.trade_code == trade_code);
            string supplier_code;
            if (pOSLog == null)
            {
                POSLog pOSLog_new = new POSLog();
                pOSLog_new.client_code = client_code;
                pOSLog_new.trade_code = trade_code;
                _db.Pos_log.Add(pOSLog_new);
                //_db.SaveChanges();

            }
            if (pOSLog.supplier_code == null)
            {
                supplier_code = "03012001";

            }
           
            else
            {
                string temp = pOSLog.supplier_code.Substring(5, 3);
                int code_no = Convert.ToInt32(temp);
                code_no++;
                string s = code_no.ToString("000");
                supplier_code = "03012" + s;

            }
            return supplier_code;

        }

        public void Update(Supplier supplier)
        {
            Supplier aSupplier = _db.Suppliers_info.FirstOrDefault(c => c.id == supplier.id);
            if (aSupplier != null)
            {
                //aSupplier.code = supplier.code;
                aSupplier.name = supplier.name;
                aSupplier.company = supplier.company;
                aSupplier.address = supplier.address;
                aSupplier.division = supplier.division;
                aSupplier.district = supplier.district;
                aSupplier.thana = supplier.thana;
                aSupplier.mobile = supplier.mobile;
                aSupplier.email = supplier.email;
                aSupplier.supplier_type = supplier.supplier_type;
                aSupplier.remarks = supplier.remarks;
                aSupplier.entry_date = supplier.entry_date;
                aSupplier.entry_by = supplier.entry_by;

            }

           
        }

    }


}
