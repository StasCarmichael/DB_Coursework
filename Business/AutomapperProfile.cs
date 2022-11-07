using AutoMapper;
using Business.Models;
using Data.Entities;
using System.Linq;

namespace Business
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            CreateMap<Product, ProductModel>()
                .ForMember(pm => pm.ReceiptDetailIds, r => r.MapFrom(x => x.ReceiptDetails.Select(rd => rd.Id)))
                .ForMember(pm => pm.CategoryName, r => r.MapFrom(x => x.Category.CategoryName))
                .ReverseMap();


            CreateMap<Customer, CustomerModel>()
                .ForMember(cm => cm.ReceiptsIds, r => r.MapFrom(c => c.Receipts.Select(x => x.Id)))
                .ForMember(cm => cm.Name, r => r.MapFrom(c => c.Person.Name))
                .ForMember(cm => cm.Surname, r => r.MapFrom(c => c.Person.Surname))
                .ForMember(cm => cm.BirthDate, r => r.MapFrom(c => c.Person.BirthDate))
                .ReverseMap();


            CreateMap<Receipt, ReceiptModel>()
                .ForMember(rm => rm.ReceiptDetailsIds, r => r.MapFrom(x => x.ReceiptDetails.Select(rd => rd.Id)))
                .ReverseMap();


            CreateMap<ReceiptDetail, ReceiptDetailModel>()
                .ReverseMap();


            CreateMap<Administrator, AdministratorModel>()
                .ReverseMap();


            CreateMap<ProductCategory, ProductCategoryModel>()
                .ForMember(pcm => pcm.ProductIds, r => r.MapFrom(x => x.Products.Select(x => x.Id)))
                .ReverseMap();
        }
    }
}