using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;


        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        public async Task AddAsync(CustomerModel model)
        {
            if (model == null) throw new MarketException(nameof(model));
            if (model.BirthDate.Year < 1900 || model.BirthDate > DateTime.Today) throw new MarketException(nameof(model.BirthDate));
            if (model.Name == null || model.Surname == null) throw new MarketException(nameof(model.Name));
            if (model.Name == string.Empty || model.Surname == string.Empty) throw new MarketException(nameof(model.Name));
            if (model.DiscountValue < 0) throw new MarketException(nameof(model.DiscountValue));

            await unitOfWork.CustomerRepository.AddAsync(mapper.Map<CustomerModel, Customer>(model));
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await unitOfWork.CustomerRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(await unitOfWork.CustomerRepository.GetAllWithDetailsAsync());
        }

        public async Task<CustomerModel> GetByIdAsync(int id)
        {
            return mapper.Map<Customer, CustomerModel>(await unitOfWork.CustomerRepository.GetByIdWithDetailsAsync(id));
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomersByProductIdAsync(int productId)
        {
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>
                ((await unitOfWork.CustomerRepository.GetAllWithDetailsAsync())
                .Where(el => el.Receipts
                    .Any(x => x.ReceiptDetails
                        .Any(v => v.ProductId == productId))));
        }

        public async Task UpdateAsync(CustomerModel model)
        {
            if (model == null) throw new MarketException(nameof(model));
            if (model.BirthDate.Year < 1900 || model.BirthDate > DateTime.Today) throw new MarketException(nameof(model.BirthDate));
            if (model.Name == null || model.Surname == null) throw new MarketException(nameof(model.Name));
            if (model.Name == string.Empty || model.Surname == string.Empty) throw new MarketException(nameof(model.Name));
            if (model.DiscountValue < 0) throw new MarketException(nameof(model.DiscountValue));


            //question
            var customer = mapper.Map<CustomerModel, Customer>(model);
            var person = customer.Person;
            person.Id = customer.Id;
            customer.PersonId = customer.Id;
            unitOfWork.PersonRepository.Update(person);


            unitOfWork.CustomerRepository.Update(customer);
            await unitOfWork.SaveAsync();
        }
    }
}
