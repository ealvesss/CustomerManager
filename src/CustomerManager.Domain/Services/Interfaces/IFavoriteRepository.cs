using CustomerManager.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface IFavoriteRepository : IRepositoryBase<Favorite>
    {
        Task<Favorite> GetFavoriteByCustomerId(Guid id);
    }
}
