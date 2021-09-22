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
        Task<ExecutionResult<FavoriteResultDto>> Create<TValidator>(FavoriteRequestDto obj);

        Task Delete(Guid id);

        Task<FavoriteResultDto> GetById(Guid id);

        Task<ExecutionResult<FavoriteResultDto>> Update<TValidator>(Guid id);
    }
}
