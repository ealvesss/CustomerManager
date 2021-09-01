using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerManager.Infra.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected CustomerManagerContext RepositoryContext { get; set; }

        public RepositoryBase(CustomerManagerContext context)
        {
            this.RepositoryContext = context;
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
            Save();
        }

        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
            Save();
        }

        public IQueryable<T> FindById(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        private void Save()
        {
            this.RepositoryContext.SaveChanges();
        }
    }
}
