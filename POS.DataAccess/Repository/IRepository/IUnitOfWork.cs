using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DataAccess.Repository.IRepository
{

    public interface IUnitOfWork : IDisposable
    {
        IExpireLogRepository ExpireLog { get; }
        ISupplierRepository Supplier { get; }
        ICustomerRepository Customer { get; }
        IPOSLogRepository POSLog { get; }
        IManufacturerRepository Manufacturer { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IUnitRepository Unit { get; }
        IProductEventInfoRepository ProductEventInfo { get; }
        IProductStockRepository ProductStock { get; }
        IProductStockInRepository ProductStockIn{ get; }
        IProductStockOutRepository ProductStockOut { get; }
        ISubCategoryRepository SubCategory { get; }
        ISP_Call SP_Call { get; }
        IUserRepository User { get; }
        ITradeRepository Trade { get; }
        IClientRepository Client { get; }
        IUserTradeRepository UserTrade { get; }
        IAccountsHeadRepository AccountsHead { get; }
        IAccountsGroupRepository AccountsGroup { get; }
        IAccountControlRepository AccountControl { get; }
        ILedgerRepository Ledger { get; }
        void Save();
    }

}
