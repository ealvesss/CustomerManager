using CustomerManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> Get();
        Task<IEnumerable<Customer>> GetById(Guid Id);
        Task Create(Customer entity);
        Task Update(Customer entity);
        Task Delete(Customer entity);
    }
}
