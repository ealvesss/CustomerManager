using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Infra.Repositories
{
    public class FavoriteRepository :  IFavoriteRepository
    {
        protected CustomerManagerContext _repository { get; set; }

        public FavoriteRepository(CustomerManagerContext context)
        {
            this._repository = context;
        }

        public async Task Create(Favorite entity)
        {
            await this._repository.Set<Favorite>().AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);

            if (entity != null) this._repository.Set<Favorite>().Remove(entity);
        }

        public async Task Update(Favorite entity)
        {
            this._repository.Set<Favorite>().Update(entity);
        }

        public async Task<Favorite> GetByExpression(Expression<Func<Favorite, bool>> expression)
        {
            return await this._repository.Set<Favorite>().FindAsync(expression);
        }

        public async Task<Favorite> GetById(Guid id)
        {

            var test = await this._repository.Set<Favorite>().Include(p => p.Products).FirstOrDefaultAsync(x => x.Id == id);

            return test;
        }

        public async Task<Favorite> GetFavoriteByCustomerId(Guid id)
        {
            return await this._repository.Set<Favorite>().FirstOrDefaultAsync(x => x.CustomerId == id);
        }
    }
}
