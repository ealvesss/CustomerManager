using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Services
{
    public class FavoriteService : ServiceBase<Favorite>, IFavoriteService
    {

        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IValidator<Favorite> _validator;
        private readonly IProductService _productService;

        public FavoriteService(IFavoriteRepository repository, IValidator<Favorite> validator, IProductService productService)
           : base(null, validator)
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

            await _favoriteRepository.Create(obj);

            return new ExecutionResult<Favorite>() { Data = obj, ValidationResult = validationResult };
        }

        public override async Task Delete(Favorite entity)
        {
            throw new NotImplementedException();
        }

        public Task<Favorite> GetByExpression(Expression<Func<Favorite, bool>> Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Favorite> GetById(Guid id)
        {
            var result = await _favoriteRepository.GetById(id);

            List<Product> products = new List<Product>();

            foreach (var product in result.Products)
            {
                var itemResult = (Product) await _productService.GetProduct(product.ExternalProductId);
                products.Add(new Product()
                {
                    Brand = itemResult.Brand,
                    ExternalProductId = product.ExternalProductId,
                    Image = itemResult.Image,
                    Price = itemResult.Price,
                    Title = itemResult.Title
                });
            }

            result.Products = products;

            return result;
        }

        public async Task<ExecutionResult<Favorite>> Update<TValidator>(Favorite obj)
        {
            throw new NotImplementedException();
        }
    }
}
