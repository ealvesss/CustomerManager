using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Application.Services.Interfaces;
using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly CustomerManagerContext _context;

        public CustomerAppService(ICustomerService customerService, IMapper mapper, CustomerManagerContext context)
        {
            this._customerService = customerService;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ExecutionResult<CustomerResponseDto>> Create<TValidator>(CustomerRequestDto customerDto)
        {
            var customer = _mapper.Map<CustomerRequestDto, Customer>(customerDto);
            
            var result = await _customerService.Create<TValidator>(customer);

            if(result.ValidationResult.Errors.Count == 0 ) await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<Customer, CustomerResponseDto>(result.Data);

            return new ExecutionResult<CustomerResponseDto>() { Data = resultDto, ValidationResult = result.ValidationResult };
        }

        public Task Delete(Guid id)
        {

            //todo: call savechanges()
            throw new NotImplementedException();
        }

        public async Task<CustomerResponseDto> GetById(Guid id)
        {

            var result = await this._customerService.GetByExpression(x => x.Id == id);

            var dto = _mapper.Map<Customer, CustomerResponseDto>(result);

            return dto;
        }

        public Task<ExecutionResult<CustomerResponseDto>> Update<TValidator>(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
