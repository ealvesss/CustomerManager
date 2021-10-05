using CustomerManager.Application.Dtos;
using CustomerManager.Application.Services.Interfaces;
using CustomerManager.Domain.Services.Validator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManager.Api.Controllers
{
    [ApiController]
    [Route("api/v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService _appService;

        public CustomerController(ICustomerAppService service)
        {
            this._appService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerRequestDto customer)
        {
            if (customer == null)
                return BadRequest();

            var result = await _appService.Create<CustomerValidator>(customer);

            if (!result.ValidationResult.IsValid)
                return BadRequest(result.ValidationResult.ToString(Environment.NewLine));

            return Ok(result.Data.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var result = await _appService.GetById(id);

            if (result == null)
                return NotFound("Customer Not Found!");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (Id == Guid.Empty)
                return BadRequest();

            var result = await _appService.Delete<CustomerValidator>(Id);

            if (!result.ValidationResult.IsValid)
                return BadRequest(result.ValidationResult.ToString(Environment.NewLine));

            return Ok(result.Data);
            
        }

        [HttpPut()]
        public async Task<IActionResult> Put(CustomerRequestDto entity)
        {
            if (entity == null)
                return BadRequest();

            var result = await _appService.Update<CustomerValidator>(entity);

            if (result.ValidationResult.Errors.Count > 0) return BadRequest(result);

            return Ok(result);
        }

    }
}
