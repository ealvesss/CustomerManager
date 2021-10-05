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
                .ForPath(f => f.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForPath(f => f.Id, opt => opt.MapFrom(src => src.FavoriteId));                

            CreateMap<Favorite, FavoriteResponseDto>()
                .ForPath(f => f.Id, opt => opt.MapFrom(src => src.Id));

        }
    }
}
