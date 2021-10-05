using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<ExecutionResult<Favorite>> Create<FavoriteValidator>(Favorite obj);

        Task Delete(Favorite entity);

        Task<Favorite> GetById(Guid id);

        Task<ExecutionResult<Favorite>> Update<FavoriteValidator>(Favorite obj, IEnumerable<Product> products);
        Task<Favorite> GetByCustomerId(Guid id);
    }
}
