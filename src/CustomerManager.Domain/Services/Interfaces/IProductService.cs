using CustomerManager.Domain.Entities;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface IProductService
    {
        [Get("/api/product/{idProduct}")]
        Task<IEnumerable<Favorites>> GetFavorites();
    }
}
