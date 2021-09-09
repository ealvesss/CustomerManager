using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Infra.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected CustomerManagerContext _repository { get; set; }

        public RepositoryBase(CustomerManagerContext context)
        {
            this._repository = context;
        }

        public async Task Create(T entity)
        {
            this._repository.Set<T>().Add(entity);
            await Save();
        }

        public async Task Delete(T entity)
        {
            this._repository.Set<T>().Remove(entity);
            Save();
        }

        public async Task<IEnumerable<T>> Get()
        {
            return this._repository.Set<T>().AsNoTracking();
        }

        public async Task Update(T entity)
        {
            this._repository.Set<T>().Update(entity);
            await Save();
        }

        public async Task<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return await this._repository.Set<T>().FirstOrDefaultAsync(expression);
        }

        public virtual async Task Save()
        {
            await this._repository.SaveChangesAsync();
        }

    }
}
