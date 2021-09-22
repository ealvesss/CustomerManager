using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Domain.Entities;

namespace CustomerManager.Application.AutoMapper
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<FavoriteRequestDto, Favorite>()
                .ForPath(f => f.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

            CreateMap<Favorite, FavoriteResultDto>()
                .ForPath(f => f.FavoriteId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Product, ProductDto>()
                .ForPath(p => p.Id, opt => opt.MapFrom(src => src.ExternalProductId));

            CreateMap<ProductDto, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForPath(p => p.ExternalProductId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
