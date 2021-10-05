using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services
{
    public class FavoriteService : ServiceBase<Favorite>, IFavoriteService
    {

        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IValidator<Favorite> _validator;
        private readonly IProductService _productService;

        public FavoriteService(IFavoriteRepository favoriteRepository, IFavoriteRepository repository, IValidator<Favorite> validator, IProductService productService)
           : base(favoriteRepository, validator)
        {
            _favoriteRepository = repository;
            _validator = validator;
            _productService = productService;
        }


        public override async Task<ExecutionResult<Favorite>> Create<FavoriteValidator>(Favorite obj)
        {
            var validationResult = base.Validate(obj, _validator);

            if (!validationResult.IsValid)
                return new ExecutionResult<Favorite> { Data = obj, ValidationResult = validationResult };

            var duplicatedItens = obj.Products.GroupBy(p => p.ExternalProductId)
                                        .Where(g => g.Count() > 1)
                                        .Select(x => x.FirstOrDefault().ExternalProductId);

            if (duplicatedItens.Count() > 0)
            {
                foreach (var item in duplicatedItens)
                {
                    validationResult.Errors.Add(new ValidationFailure("duplicatedItens", $"Duplicated product found in favorite list [{item}]!"));
                }
                return new ExecutionResult<Favorite> { Data = obj, ValidationResult = validationResult };
            }


            var result = await _favoriteRepository.GetFavoriteByCustomerId(obj.CustomerId);

            if (result != null)
            {
                validationResult.Errors.Add(new ValidationFailure("ExistsFavoritesForCustomer", "Customer already have a favorite!"));
                return new ExecutionResult<Favorite> { Data = obj, ValidationResult = validationResult };
            }

            foreach (var product in obj.Products)
            {
                var existProduct = await GetProduct(product.ExternalProductId);

                if (existProduct == null)
                {
                    validationResult.Errors.Add(new ValidationFailure("ProductDoesNotExists", $"Product {product.ExternalProductId} does not exists!"));
                }
            }

            if (validationResult.Errors.Count() > 0) return new ExecutionResult<Favorite>() { Data = obj, ValidationResult = validationResult };


            await _favoriteRepository.Create(obj);

            return new ExecutionResult<Favorite>() { Data = obj, ValidationResult = validationResult };
        }

        public override async Task Delete(Favorite entity)
        {
            await this._favoriteRepository.Delete(entity);
        }

        public async Task<Favorite> GetByCustomerId(Guid id)
        {
            var result = await _favoriteRepository.GetFavoriteByCustomerId(id);

            return result;
        }

        public override async Task<Favorite> GetById(Guid id)
        {
            var result = await _favoriteRepository.GetById(id);

            return result;
        }

        public Task<ExecutionResult<Favorite>> Update<FavoriteValidator>(Favorite entity, IEnumerable<Product> products)
        {
            entity.OverwriteProducts(products);

            var validationResult = base.Validate(entity, _validator);

            if (!validationResult.IsValid)
                return Task.FromResult(new ExecutionResult<Favorite> { Data = entity, ValidationResult = validationResult });

            return Task.FromResult(new ExecutionResult<Favorite> { Data = entity, ValidationResult = validationResult });
        }

        private async Task<Product> GetProduct(Guid id)
        {
            try
            {
                return await _productService.GetProduct(id);
            }
            catch (Exception)
            {
                return default(Product);
            }
        }

        public async Task<Favorite> GetDetailById(Guid id)
        {
            var result = await _favoriteRepository.GetById(id);


            return result;
        }

        private async Task<IEnumerable<Product>> GetProducts(List<Guid> products)
        {
            var resultProducts = new List<Product>();

            foreach (var prod in products)
            {
                var product = await _productService.GetProduct(prod);
                resultProducts.Add(product);
            }

            return resultProducts;
        }
    }
}
