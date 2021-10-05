using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CustomerManager.Infra.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected CustomerManagerContext _repository { get; set; }

        public RepositoryBase(CustomerManagerContext context)
        {
            this._repository = context;
        }

        public virtual async Task Create(T entity)
        {
            await this._repository.Set<T>().AddAsync(entity);
        }

        public virtual async Task Delete(T entity)
        {
            if (entity != null) await Task.FromResult(this._repository.Set<T>().Remove(entity));
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await this._repository.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
