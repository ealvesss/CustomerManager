using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CustomerManager.Infra.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        protected CustomerManagerContext _ctx;

        public CustomerRepository(CustomerManagerContext context) 
            : base(context)
        {
            _ctx = context;
        }

        public override Task Delete(Customer entity)
        {
            return base.Delete(entity);
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await _ctx.Set<Customer>().FirstOrDefaultAsync(c => c.Email == email);
        }

        public override async Task<Customer> GetById(Guid Id)
        {
            return await _ctx.Set<Customer>().FirstOrDefaultAsync(c => c.Id == Id);
        }
    }
}
