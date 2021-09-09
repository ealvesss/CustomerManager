using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetBy(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
