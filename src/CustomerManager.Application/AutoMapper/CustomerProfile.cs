using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.Application.AutoMapper
{
   public class CustomerProfile : Profile
    {

        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
