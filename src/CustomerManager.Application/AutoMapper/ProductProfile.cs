using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Domain.Entities;

namespace CustomerManager.Application.AutoMapper
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<Product, ProductRequestDto>()
               .ForPath(p => p.ExternalId, opt => opt.MapFrom(src => src.ExternalProductId));

            CreateMap<ProductRequestDto, Product>()
                .ForPath(p => p.ExternalProductId, opt => opt.MapFrom(src => src.ExternalId)); ;
            
            CreateMap<ProductRequestUpdateDto, Product>()
                .ForPath(p => p.ExternalProductId, opt => opt.MapFrom(src => src.ExternalId));
           
            CreateMap<Product, ProductRequestDto>()
                .ForPath(p => p.ExternalId, opt => opt.MapFrom(src => src.ExternalProductId));


            
        }
    }
}
