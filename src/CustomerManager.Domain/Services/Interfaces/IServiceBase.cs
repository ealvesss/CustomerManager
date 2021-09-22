using CustomerManager.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<ExecutionResult<T>> Create<TValidator>(T obj);

        Task Delete(T entity);

        //Task<IEnumerable<T>> Get();

        Task<T> GetByExpression(Expression<Func<T,bool>> Id);

        Task<ExecutionResult<T>> Update<TValidator>(T obj);

    }
}
