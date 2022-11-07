using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ReceiptDetailRepository : GenericRepository<ReceiptDetail>, IReceiptDetailRepository
    {
        private readonly DbSet<ReceiptDetail> dbSet;


        public ReceiptDetailRepository(TradeMarketDbContext tradeMarket)
            : base(tradeMarket)
        {
            dbSet = tradeMarket.Set<ReceiptDetail>();
        }


        public async Task<IEnumerable<ReceiptDetail>> GetAllWithDetailsAsync()
        {
            return await dbSet.Include(el => el.Receipt)
                        .Include(el => el.Product)
                            .ThenInclude(el => el.Category)
                        .ToListAsync();
        }
    }
}
