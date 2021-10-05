using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<ExecutionResult<Customer>> Create<TValidator>(Customer entity);

        Task<ExecutionResult<Customer>> Delete(Guid id);
        Task<Customer> GetById(Guid id);

        Task<ExecutionResult<Customer>> Update<TValidator>(Customer entity);
    }
}
