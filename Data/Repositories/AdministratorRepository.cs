using Data.Data;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class AdministratorRepository : GenericRepository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(TradeMarketDbContext tradeMarket)
            : base(tradeMarket)
        { }
    }
}
