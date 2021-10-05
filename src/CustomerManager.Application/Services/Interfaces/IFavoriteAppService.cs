using CustomerManager.Application.Dtos;
using CustomerManager.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Application.Services.Interfaces
{
    public interface IFavoriteAppService
    {
        Task<ExecutionResult<FavoriteResponseDto>> Create<TValidator>(FavoriteRequestDto obj);

        Task<ExecutionResult<FavoriteResponseDto>> Delete(Guid id);

        Task<FavoriteResponseDto> GetById(Guid id);

        Task<ExecutionResult<FavoriteResponseDto>> Update<TValidator>(FavoriteRequestUpdateDto entity);

        Task<FavoriteResponseDto> GetByCustomerId(Guid id);
    }
}
