using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;


        public AdministratorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }




        public async Task<IEnumerable<AdministratorModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<Administrator>, IEnumerable<AdministratorModel>>
                (await unitOfWork.AdministratorRepository.GetAllAsync());
        }
        public async Task<AdministratorModel> GetByIdAsync(int id)
        {
            return mapper.Map<Administrator, AdministratorModel>
                (await unitOfWork.AdministratorRepository.GetByIdAsync(id));
        }


        public async Task AddAsync(AdministratorModel model)
        {
            if (model == null || model.Login == null || model.Password == null) throw new MarketException(nameof(model));
            if (model.AccessLevel > 0 && model.AccessLevel < 100
                && model.Login.Length <= 80 && model.Login.Length >= 8
                && model.Password.Length <= 50 && model.Password.Length >= 8)
            {
                await unitOfWork.AdministratorRepository.AddAsync(mapper.Map<AdministratorModel, Administrator>(model));
                await unitOfWork.SaveAsync();
            }
            else throw new MarketException(nameof(model.Login));
        }


        public async Task UpdateAsync(AdministratorModel model)
        {
            if (model == null) throw new MarketException(nameof(model));

            var admin = await unitOfWork.AdministratorRepository.GetByIdAsync(model.Id);


            if (model.Login == null || model.Login.Length > 80 || model.Login.Length < 8)
            { model.Login = admin.Login; }

            if (model.Password == null || model.Password.Length > 80 || model.Password.Length < 8)
            { model.Password = admin.Password; }

            if (model.AccessLevel <= 0 || model.AccessLevel > 100)
            { model.AccessLevel = admin.AccessLevel; }


            unitOfWork.AdministratorRepository.Update(mapper.Map<AdministratorModel, Administrator>(model));
            await unitOfWork.SaveAsync();
        }


        public async Task DeleteAsync(int modelId)
        {
            await unitOfWork.AdministratorRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveAsync();
        }
    }
}
