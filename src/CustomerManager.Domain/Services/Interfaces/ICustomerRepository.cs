using CustomerManager.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<Customer> GetByEmail(string Id);
    }
}
