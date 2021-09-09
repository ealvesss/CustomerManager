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
        Task<ExecutionResult<Customer>> Create<TValidator>(Customer obj);

        Task Delete(Customer entity);

        Task<IEnumerable<Customer>> Get();

        Task<Customer> GetBy(Expression<Func<Customer,bool>> Id);

        Task<ExecutionResult<Customer>> Update<TValidator>(Customer obj);
    }
}
