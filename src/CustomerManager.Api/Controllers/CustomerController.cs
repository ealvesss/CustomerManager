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
    [Route("v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService _service;

        public CustomerController(ICustomerAppService service)
        {
            this._service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customer)
        {
            if (customer == null)
                return BadRequest();

            var result = await _service.Create<CustomerValidator>(customer);

            if (!result.ValidationResult.IsValid)
                return BadRequest(result.ValidationResult.ToString(Environment.NewLine));

            return Ok(result.Data.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] Guid Id)
        {
            //todo: fix param

            if (Id == Guid.Empty)
                return BadRequest();

            var result = await _service.GetById(Id);

            if (result == null)
                return NotFound("Customer Not Found!");

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {

            var result = await _service.Get();

            if (result == null)
                return NotFound("No Customer Found!");

            return Ok(result);
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid Id)
        //{
        //    if (Id == Guid.Empty)
        //        return BadRequest();

        //    _service.Delete();

        //    return Ok();
        //}

        //[HttpPut("{id}")]
        //public IActionResult Put(Guid Id)
        //{
        //    if (Id == Guid.Empty)
        //        return BadRequest();

        //    _service.Update();

        //    return Ok();
        //}



    }
}
