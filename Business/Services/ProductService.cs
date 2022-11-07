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
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;


        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        public async Task AddAsync(ProductModel model)
        {
            if (model == null || model.ProductName == null || model.ProductName == string.Empty)
                throw new MarketException(nameof(model));
            if (model.Price < 0) throw new MarketException(nameof(model.Price));

            var product = mapper.Map<ProductModel, Product>(model);
            product.Category = unitOfWork.ProductCategoryRepository.GetByIdAsync(product.ProductCategoryId).Result;

            await unitOfWork.ProductRepository.AddAsync(product);
            await unitOfWork.SaveAsync();
        }
        public async Task AddCategoryAsync(ProductCategoryModel categoryModel)
        {
            if (categoryModel == null || categoryModel.CategoryName == null || categoryModel.CategoryName == string.Empty)
                throw new MarketException(nameof(categoryModel));

            await unitOfWork.ProductCategoryRepository.AddAsync(mapper.Map<ProductCategoryModel, ProductCategory>(categoryModel));
            await unitOfWork.SaveAsync();
        }



        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(await unitOfWork.ProductRepository.GetAllWithDetailsAsync());
        }
        public async Task<IEnumerable<ProductCategoryModel>> GetAllProductCategoriesAsync()
        {
            return mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryModel>>(await unitOfWork.ProductCategoryRepository.GetAllAsync());
        }



        public async Task<IEnumerable<ProductModel>> GetByFilterAsync(FilterSearchModel filterSearch)
        {
            var result = (await GetAllAsync());


            if (filterSearch.MaxPrice.HasValue)
                result = result.Where(el => el.Price < (decimal)(filterSearch.MaxPrice.Value));

            if (filterSearch.MinPrice.HasValue)
                result = result.Where(el => el.Price > filterSearch.MinPrice.Value);

            if (filterSearch.CategoryId.HasValue)
                result = result.Where(el => el.ProductCategoryId == filterSearch.CategoryId.Value);


            return result;
        }
        public async Task<ProductModel> GetByIdAsync(int id)
        {
            return mapper.Map<Product, ProductModel>(await unitOfWork.ProductRepository.GetByIdWithDetailsAsync(id));
        }



        public async Task UpdateAsync(ProductModel model)
        {
            if (model == null || model.ProductName == null || model.ProductName == string.Empty)
                throw new MarketException(nameof(model));
            if (model.Price < 0) throw new MarketException(nameof(model.Price));

            var product = mapper.Map<ProductModel, Product>(model);
            var category = product.Category;
            category.Id = model.ProductCategoryId;
            category.CategoryName = model.CategoryName;
            unitOfWork.ProductCategoryRepository.Update(category);

            unitOfWork.ProductRepository.Update(product);
            await unitOfWork.SaveAsync();
        }
        public async Task UpdateCategoryAsync(ProductCategoryModel categoryModel)
        {
            if (categoryModel == null || categoryModel.CategoryName == null || categoryModel.CategoryName == string.Empty)
                throw new MarketException(nameof(categoryModel));

            unitOfWork.ProductCategoryRepository.Update(mapper.Map<ProductCategoryModel, ProductCategory>(categoryModel));
            await unitOfWork.SaveAsync();
        }



        public async Task DeleteAsync(int modelId)
        {
            await unitOfWork.ProductRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveAsync();
        }
        public async Task RemoveCategoryAsync(int categoryId)
        {
            await unitOfWork.ProductCategoryRepository.DeleteByIdAsync(categoryId);
            await unitOfWork.SaveAsync();
        }
    }
}
