using Microsoft.AspNetCore.Mvc;

namespace CustomerManager.Api.Controllers
{
    [ApiController]
    [Route("v1/customer")]
    public class CustomerController : ControllerBase
    {

        public CustomerController()
        {

        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
