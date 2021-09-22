using CustomerManager.Application.Dtos;
using CustomerManager.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Application.Services.Interfaces
{
    public interface ICustomerAppService
    {
        Task<ExecutionResult<CustomerResponseDto>> Create<TValidator>(CustomerRequestDto obj);

        Task Delete(Guid id);

        Task<CustomerResponseDto> GetById(Guid id);

        Task<ExecutionResult<CustomerResponseDto>> Update<TValidator>(Guid id);

    }
}
