using AutoMapper;
using CustomerManager.Application.Dtos;
using CustomerManager.Application.Services.Interfaces;
using CustomerManager.Domain.Base;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Services.Interfaces;
using CustomerManager.Infra.Context;
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


        public async Task<ExecutionResult<FavoriteResultDto>> Create<FavoriteValidator>(FavoriteRequestDto entity)
        {

            var favorite = _mapper.Map<FavoriteRequestDto, Favorite>(entity);

            var result = await _favoriteService.Create<Favorite>(favorite);

            if (result.ValidationResult.Errors.Count == 0) await _context.SaveChangesAsync();

            var favoriteResult = _mapper.Map<Favorite, FavoriteResultDto>(result.Data);

            return new ExecutionResult<FavoriteResultDto>() { Data = favoriteResult, ValidationResult = result.ValidationResult };
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<FavoriteResultDto> GetById(Guid id)
        {
            var result = await _favoriteService.GetById(id);

            var dto = _mapper.Map<Favorite, FavoriteResultDto>(result);

            return dto;

        }

        public Task<ExecutionResult<FavoriteResultDto>> Update<TValidator>(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
