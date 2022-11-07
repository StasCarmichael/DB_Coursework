using Data.Interfaces;
using Data.Repositories;
using System.Threading.Tasks;

namespace Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradeMarketDbContext dbContext;


        private readonly ICustomerRepository customerRepository;
        private readonly IPersonRepository personRepository;
        private readonly IAdministratorRepository administratorRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IReceiptRepository receiptRepository;
        private readonly IReceiptDetailRepository receiptDetailRepository;


        public UnitOfWork(TradeMarketDbContext tradeMarket)
        {
            dbContext = tradeMarket;

            customerRepository = new CustomerRepository(dbContext);
            personRepository = new PersonRepository(dbContext);
            administratorRepository = new AdministratorRepository(dbContext);
            productRepository = new ProductRepository(dbContext);
            productCategoryRepository = new ProductCategoryRepository(dbContext);
            receiptRepository = new ReceiptRepository(dbContext);
            receiptDetailRepository = new ReceiptDetailRepository(dbContext);
            
        }


        public ICustomerRepository CustomerRepository => customerRepository;
        public IPersonRepository PersonRepository => personRepository;
        public IAdministratorRepository AdministratorRepository => administratorRepository;
        public IProductRepository ProductRepository => productRepository;
        public IProductCategoryRepository ProductCategoryRepository => productCategoryRepository;
        public IReceiptRepository ReceiptRepository => receiptRepository;
        public IReceiptDetailRepository ReceiptDetailRepository => receiptDetailRepository;


        public async Task SaveAsync() => await dbContext.SaveChangesAsync();
    }
}
