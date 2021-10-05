using CustomerManager.Domain.Entities;
using Refit;
using System;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services.Interfaces
{
    public interface IProductService
    {
        //refit notation to bind external api
        [Get("/api/product/{id}/")]
        Task<Product> GetProduct(Guid id);


    }
}
