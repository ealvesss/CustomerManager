using CustomerManager.Application.Dtos;
using CustomerManager.Application.Services.Interfaces;
using CustomerManager.Domain.Services.Validator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CustomerManager.Api.Controllers
{
    [Route("api/v1/favorite")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {

        private readonly IFavoriteAppService _service;

        public FavoriteController(IFavoriteAppService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FavoriteRequestDto entity)
        {

            var result = await this._service.Create<FavoriteValidator>(entity);

            if (!result.ValidationResult.IsValid)
                return BadRequest(result.ValidationResult.ToString(Environment.NewLine));

            return Ok(result.Data.FavoriteId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetById(id);

            if (result == null)
                return NotFound("Favorite not found!");
            
            return Ok(result);
        }
    }
}
