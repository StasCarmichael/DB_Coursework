using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> dbSet;


        public ProductRepository(TradeMarketDbContext tradeMarket)
            : base(tradeMarket)
        {
            dbSet = tradeMarket.Set<Product>();
        }


        public async Task<IEnumerable<Product>> GetAllWithDetailsAsync()
        {
            return await dbSet.Include(el => el.ReceiptDetails)
                    .Include(el => el.Category)
                    .ToListAsync();
        }

        public async Task<Product> GetByIdWithDetailsAsync(int id)
        {
            return await dbSet.Include(el => el.ReceiptDetails)
                    .Include(el => el.Category)
                    .FirstAsync(el => el.Id == id);
        }
    }
}
