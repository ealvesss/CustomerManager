using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Application.Services.Interfaces;
using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManager.Application.Services
{
    public class FavoriteAppService : IFavoriteAppService
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IMapper _mapper; 
        private readonly CustomerManagerContext _context;

        public FavoriteAppService(IFavoriteService service, IMapper mapper, CustomerManagerContext ctx)
        {
            _favoriteService = service;
            _mapper = mapper;
            _context = ctx;
        }

        public async Task<ExecutionResult<FavoriteResponseDto>> Create<FavoriteValidator>(FavoriteRequestDto entity)
        {
            var favorite = _mapper.Map<FavoriteRequestDto, Favorite>(entity);

            var result = await _favoriteService.Create<Favorite>(favorite);

            if (result.ValidationResult.Errors.Count == 0) await _context.SaveChangesAsync();

            var favoriteResult = _mapper.Map<Favorite, FavoriteResponseDto>(result.Data);

            return new ExecutionResult<FavoriteResponseDto>() { Data = favoriteResult, ValidationResult = result.ValidationResult };
        }

        public async Task<ExecutionResult<FavoriteResponseDto>> Delete(Guid id)
        {
            var validationResult = new ValidationResult();
            var entity = await this._favoriteService.GetById(id);

            await this._favoriteService.Delete(entity);

            if (await _context.SaveChangesAsync() == 0)
            {
                validationResult = new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("deleteFailed", $"Error on delete Favorite {id}") });
                return await Task.FromResult(new ExecutionResult<FavoriteResponseDto>() { Data = null, ValidationResult = validationResult });
            }

            return await Task.FromResult(new ExecutionResult<FavoriteResponseDto>() { Data = null, ValidationResult = validationResult });
        }

        public async Task<FavoriteResponseDto> GetById(Guid id)
        {
            var result = await _favoriteService.GetById(id);

            var dto = _mapper.Map<Favorite, FavoriteResponseDto>(result);

            return dto;

        }

        public async Task<FavoriteResponseDto> GetByCustomerId(Guid id)
        {
            var result = await _favoriteService.GetByCustomerId(id);

            var dto = _mapper.Map<Favorite, FavoriteResponseDto>(result);

            return dto;
        }

        public async Task<ExecutionResult<FavoriteResponseDto>> Update<TValidator>(FavoriteRequestUpdateDto request)
        {
            var favorite = await _favoriteService.GetById(request.Id);

            if (favorite == null)
            {
                var validationResult = new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("NotExistsFavorite", "Favorite Not Found!") });
                return new ExecutionResult<FavoriteResponseDto> { Data = null, ValidationResult = validationResult };
            }

            var products = _mapper.Map<IEnumerable<Product>>(request.Products);

            var result = await _favoriteService.Update<TValidator>(favorite, products);
            
            if (result.ValidationResult.Errors.Count == 0) await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<Favorite, FavoriteResponseDto>(result.Data);

            return new ExecutionResult<FavoriteResponseDto>()
            {
                Data = resultDto,
                ValidationResult = result.ValidationResult
            };
        }
    }
}
