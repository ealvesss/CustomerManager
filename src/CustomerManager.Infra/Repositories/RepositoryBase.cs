using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task Create(T entity)
        {
            await this._repository.Set<T>().AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            
            if(entity != null) this._repository.Set<T>().Remove(entity);
        }

        public async Task Update(T entity)
        {
            this._repository.Set<T>().Update(entity);
            await Save();
        }

        public async Task<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return await this._repository.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T> GetById(Guid id)
        {
            return await this._repository.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task Save()
        {
            await this._repository.SaveChangesAsync();
        }

    }
}
