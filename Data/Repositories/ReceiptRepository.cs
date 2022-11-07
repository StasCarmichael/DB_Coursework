using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ReceiptRepository : GenericRepository<Receipt>, IReceiptRepository
    {
        private readonly DbSet<Receipt> dbSet;


        public ReceiptRepository(TradeMarketDbContext tradeMarket)
            : base(tradeMarket)
        {
            dbSet = tradeMarket.Set<Receipt>();
        }


        public async Task<IEnumerable<Receipt>> GetAllWithDetailsAsync()
        {
            return await dbSet.Include(el => el.Customer)
                    .Include(el => el.ReceiptDetails)
                        .ThenInclude(el => el.Product)
                            .ThenInclude(el => el.Category)
                    .ToListAsync();
        }

        public async Task<Receipt> GetByIdWithDetailsAsync(int id)
        {
            return await dbSet.Include(el => el.Customer)
                     .Include(el => el.ReceiptDetails)
                         .ThenInclude(el => el.Product)
                             .ThenInclude(el => el.Category)
                     .FirstAsync(el => el.Id == id);
        }
    }
}
