using CustomerManager.Application.Dtos;
using CustomerManager.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Application.Services.Interfaces
{
    public interface ICustomerAppService
    {
        Task<ExecutionResult<CustomerDto>> Create<TValidator>(CustomerDto obj);

        Task Delete(Guid id);

        Task<IEnumerable<CustomerDto>> Get();

        Task<CustomerDto> GetById(Guid id);

        Task<ExecutionResult<CustomerDto>> Update<TValidator>(Guid id);

    }
}
