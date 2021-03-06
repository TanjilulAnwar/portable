using POS.DataAccess.Data;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;

namespace POS.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ExpireLog = new ExpireLogRepository(_db);
            Supplier = new SupplierRepository(_db);
            Customer = new CustomerRepository(_db);
            POSLog = new POSLogRepository(_db);
            Manufacturer = new ManufacturerRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Unit = new UnitRepository(_db);
            ProductEventInfo = new ProductEventInfoRepository(_db);
            ProductStock = new ProductStockRepository(_db);
            ProductStockIn = new ProductStockInRepository(_db);
            ProductStockOut = new ProductStockOutRepository(_db);
            SubCategory = new SubCategoryRepository(_db);
            SP_Call = new SP_Call(_db);
            Trade = new TradeRepository(_db);
            User = new UserRepository(_db);
            Client = new ClientRepository(_db);
            UserTrade = new UserTradeRepository(_db);
            AccountsGroup = new AccountsGroupRepository(_db);
            AccountsHead = new AccountsHeadRepository(_db);
            AccountControl = new AccountControlRepository(_db);
            Ledger = new LedgerRepository(_db);
        }

        public IExpireLogRepository ExpireLog { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IPOSLogRepository POSLog { get; private set; }
        public IManufacturerRepository Manufacturer { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IUnitRepository Unit { get; private set; }
        public IProductEventInfoRepository ProductEventInfo { get; private set; }
        public IProductStockRepository ProductStock { get; private set; }
        public IProductStockInRepository ProductStockIn { get; private set; }
        public IProductStockOutRepository ProductStockOut { get; private set; }
        public ISubCategoryRepository SubCategory { get; private set; }
        public ISP_Call SP_Call { get; private set; }
        public IUserRepository User { get; private set; }
        public ITradeRepository Trade { get; private set; }
        public IClientRepository Client { get; private set; }
        public IUserTradeRepository UserTrade { get; private set; }
        public IAccountControlRepository AccountControl { get; private set; }
        public IAccountsGroupRepository AccountsGroup { get; private set; }
        public IAccountsHeadRepository AccountsHead { get; private set; }
        public ILedgerRepository Ledger { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
