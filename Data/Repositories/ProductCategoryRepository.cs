using Data.Data;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(TradeMarketDbContext tradeMarket)
            : base(tradeMarket)
        {        }
    }
}
