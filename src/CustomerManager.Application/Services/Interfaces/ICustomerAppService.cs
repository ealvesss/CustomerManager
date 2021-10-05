using CustomerManager.Application.Dtos;
using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Application.Services.Interfaces
{
    public interface ICustomerAppService
    {
        Task<ExecutionResult<CustomerResponseDto>> Create<TValidator>(CustomerRequestDto obj);

        Task<ExecutionResult<CustomerResponseDto>> Delete<TValidator>(Guid id);

        Task<CustomerResponseDto> GetById(Guid id);

        Task<ExecutionResult<CustomerResponseDto>> Update<TValidator>(CustomerRequestDto entity);

    }
}
