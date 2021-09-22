using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Domain.Entities;

namespace CustomerManager.Application.AutoMapper
{
    public class CustomerProfile : Profile
    {

        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResponseDto>();
            CreateMap<CustomerRequestDto, Customer>();
        }
    }
}
