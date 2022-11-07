using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly DbSet<Customer> dbSet;


        public CustomerRepository(TradeMarketDbContext tradeMarket)
            : base(tradeMarket)
        {
            dbSet = tradeMarket.Set<Customer>();
        }


        public async Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            return await dbSet.Include(el => el.Person)
                    .Include(el => el.Receipts).ThenInclude(el => el.ReceiptDetails)
                    .ToListAsync();
        }

        public async Task<Customer> GetByIdWithDetailsAsync(int id)
        {
            return await dbSet.Include(el => el.Person)
                    .Include(el => el.Receipts).ThenInclude(el => el.ReceiptDetails)
                    .FirstAsync(el => el.Id == id);
        }
    }
}
