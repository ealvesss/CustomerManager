using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Application.Services.Interfaces;
using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using System;
using FluentValidation.Results;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            if (result.ValidationResult.Errors.Count == 0) await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<Customer, CustomerResponseDto>(result.Data);

            return new ExecutionResult<CustomerResponseDto>() { Data = resultDto, ValidationResult = result.ValidationResult };
        }

        public async Task<ExecutionResult<CustomerResponseDto>> Delete<TValidator>(Guid id)
        {
            var validationResult = new ValidationResult();

            await this._customerService.Delete(id);

            if (await _context.SaveChangesAsync() == 0)
            {
                validationResult = new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("deleteFailed", $"Error on delete Customer {id}") });
                return await Task.FromResult(new ExecutionResult<CustomerResponseDto>() { Data = null, ValidationResult = validationResult });
            }

            return await Task.FromResult(new ExecutionResult<CustomerResponseDto>() { Data = null, ValidationResult = validationResult });
        }

        public async Task<CustomerResponseDto> GetById(Guid id)
        {
            var result = await this._customerService.GetById(id);

            var dto = _mapper.Map<Customer, CustomerResponseDto>(result);

            return dto;
        }

        public async Task<ExecutionResult<CustomerResponseDto>> Update<TValidator>(CustomerRequestDto requestDto)
        {
            var existsCustomer = await this._customerService.GetById(requestDto.Id);

            if (existsCustomer == null)
            {
                var validationResult = new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("NotExistsCustomer", "Customer Not Found!") });
                return new ExecutionResult<CustomerResponseDto> { Data = null, ValidationResult = validationResult };
            }

            if(existsCustomer.Email != requestDto.Email)
            {
                var validationResult = new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("EmailShouldNotBeUpdated", $"Email should not be Updated") });
                return new ExecutionResult<CustomerResponseDto> { Data = null, ValidationResult = validationResult };
            }

            var customer = _mapper.Map<CustomerRequestDto, Customer>(requestDto, existsCustomer);

            var result = await _customerService.Update<TValidator>(customer);

            if (result.ValidationResult.Errors.Count == 0) await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<Customer, CustomerResponseDto>(result.Data);

            return new ExecutionResult<CustomerResponseDto>() { Data = resultDto, ValidationResult = result.ValidationResult };

        }
    }
}

