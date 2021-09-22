using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface IRepositoryBase<T>
    {
        //Task<IEnumerable<T>> Get();
        Task<T> GetByExpression(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<T> GetById(Guid id);
    }
}
