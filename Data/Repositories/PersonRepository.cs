using Data.Data;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(TradeMarketDbContext tradeMarket)
            :base(tradeMarket)
        {
     
        }
    }
}
