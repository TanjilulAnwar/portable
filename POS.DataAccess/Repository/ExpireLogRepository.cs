using POS.DataAccess.Data;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DataAccess.Repository
{
     public class ExpireLogRepository : Repository<ExpireLog>, IExpireLogRepository
    {

        private readonly ApplicationDbContext _db;

        public ExpireLogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
