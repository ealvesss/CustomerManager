using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Application.Services.Interfaces;
using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerAppService(ICustomerService customerService, IMapper mapper)
        {
            this._customerService = customerService;
            this._mapper = mapper;
        }

        public async Task<ExecutionResult<CustomerDto>> Create<TValidator>(CustomerDto customerDto)
        {
            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            
            var result = await _customerService.Create<TValidator>(customer);

            var resultDto = _mapper.Map<Customer, CustomerDto>(result.Data);
            
            return new ExecutionResult<CustomerDto>() { Data = resultDto, ValidationResult = result.ValidationResult };
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerDto>> Get()
        {
            var result = await this._customerService.Get();

            var dto = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(result);

            return dto;
        }

        public async Task<CustomerDto> GetById(Guid id)
        {

            var result = await this._customerService.GetBy(x => x.Id == id);

            var dto = _mapper.Map<Customer, CustomerDto>(result);

            return dto;
        }

        public Task<ExecutionResult<CustomerDto>> Update<TValidator>(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
