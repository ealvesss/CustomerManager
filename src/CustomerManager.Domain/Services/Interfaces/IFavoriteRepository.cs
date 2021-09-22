using CustomerManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<Favorite> GetByExpression(Expression<Func<Favorite, bool>> expression);
        Task Create(Favorite entity);
        Task Update(Favorite entity);
        Task Delete(Guid id);
        Task<Favorite> GetById(Guid id);
        Task<Favorite> GetFavoriteByCustomerId(Guid id);
    }
}
