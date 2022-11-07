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
    public class ReceiptService : IReceiptService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;


        public ReceiptService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        public async Task AddAsync(ReceiptModel model)
        {
            await unitOfWork.ReceiptRepository.AddAsync(mapper.Map<ReceiptModel, Receipt>(model));
            await unitOfWork.SaveAsync();
        }
        public async Task AddProductAsync(int productId, int receiptId, int quantity)
        {
            var receipt = (await unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId));
            if (receipt == null) throw new MarketException(nameof(receipt));


            if (receipt.ReceiptDetails.Any(el => el.ProductId == productId))
            {
                var res = receipt.ReceiptDetails.First(el => el.ProductId == productId);
                res.Quantity += quantity;

                unitOfWork.ReceiptDetailRepository.Update(res);             
            }
            else
            {
                var product = unitOfWork.ProductRepository.GetByIdAsync(productId).Result;
                if (product == null)
                    throw new MarketException(nameof(productId));

                var result = new ReceiptDetailModel()
                {
                    ProductId = productId,
                    ReceiptId = receiptId,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    DiscountUnitPrice = product.Price * 0.01m * (decimal)(100 - receipt.Customer.DiscountValue)
                };

                await unitOfWork.ReceiptDetailRepository.AddAsync(mapper.Map<ReceiptDetailModel, ReceiptDetail>(result));                
            }

            await unitOfWork.SaveAsync();
        }



        public async Task<IEnumerable<ReceiptModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptModel>>(await unitOfWork.ReceiptRepository.GetAllWithDetailsAsync());
        }



        public async Task<IEnumerable<ReceiptDetailModel>> GetReceiptDetailsAsync(int receiptId)
        {
            return mapper.Map<IEnumerable<ReceiptDetail>, IEnumerable<ReceiptDetailModel>>
                  ((await unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId)).ReceiptDetails);
        }
        public async Task<IEnumerable<ReceiptModel>> GetReceiptsByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var result = mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptModel>>(await unitOfWork.ReceiptRepository.GetAllWithDetailsAsync());

            return result.Where(el => el.OperationDate.CompareTo(startDate) > 0
                && el.OperationDate.CompareTo(endDate) < 0);
        }



        public async Task<ReceiptModel> GetByIdAsync(int id)
        {
            return mapper.Map<Receipt, ReceiptModel>(await unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(id));
        }



        public async Task CheckOutAsync(int receiptId)
        {
            var result = (await unitOfWork.ReceiptRepository.GetByIdAsync(receiptId));
            result.IsCheckedOut = true;

            unitOfWork.ReceiptRepository.Update(result);
            await unitOfWork.SaveAsync();
        }
        public async Task<decimal> ToPayAsync(int receiptId)
        {
            var receipt = (await unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId));

            decimal sum = 0;
            foreach (var item in receipt.ReceiptDetails)
                sum += item.DiscountUnitPrice * item.Quantity;

            return sum;
        }



        public async Task UpdateAsync(ReceiptModel model)
        {
            unitOfWork.ReceiptRepository.Update(mapper.Map<ReceiptModel, Receipt>(model));
            await unitOfWork.SaveAsync();
        }



        public async Task DeleteAsync(int modelId)
        {
            var receiptDetailsIds = (await unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(modelId)).ReceiptDetails;

            await unitOfWork.ReceiptRepository.DeleteByIdAsync(modelId);

            foreach (var item in receiptDetailsIds)
                unitOfWork.ReceiptDetailRepository.Delete(item);

            await unitOfWork.SaveAsync();
        }
        public async Task RemoveProductAsync(int productId, int receiptId, int quantity)
        {
            var receipt = (await unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId));
            if (receipt == null) throw new MarketException(nameof(receipt));

            if (receipt.ReceiptDetails.Any(el => el.ProductId == productId))
            {
                var detailt = receipt.ReceiptDetails.First(el => el.ProductId == productId);

                if (detailt.Quantity > quantity)
                {
                    detailt.Quantity -= quantity;
                    unitOfWork.ReceiptRepository.Update(receipt);
                }
                else
                {
                    unitOfWork.ReceiptDetailRepository.Delete(detailt);
                }

                await unitOfWork.SaveAsync();
            }
            else { throw new MarketException(nameof(receiptId)); }
        }

    }
}
