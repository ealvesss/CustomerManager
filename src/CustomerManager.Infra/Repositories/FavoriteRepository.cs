using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CustomerManager.Infra.Repositories
{
    public class FavoriteRepository :  RepositoryBase<Favorite>, IFavoriteRepository
    {
        protected CustomerManagerContext _ctx { get; set; }

        public FavoriteRepository(CustomerManagerContext context)
            : base (context)
        {
            this._ctx = context;
        }

        public override async Task Create(Favorite entity)
        {
            await base.Create(entity);
        }

        public override async Task Delete(Favorite entity)
        {
            entity.Products.Clear();
            await Task.FromResult(this._ctx.Set<Favorite>().Remove(entity));
        }

        public override async Task<Favorite> GetById(Guid id)
        {
            return await this._ctx.Favorite.Include(p => p.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Favorite> GetFavoriteByCustomerId(Guid id)
        {
            return await this._ctx.Favorite.Include(p => p.Products).FirstOrDefaultAsync(x => x.CustomerId == id);
        }
    }
}
