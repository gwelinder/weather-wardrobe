using Microsoft.AspNetCore.Mvc;

namespace WeatherWardrobeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/auth/validate
        [HttpGet("validate")]
        public IActionResult Validate()
        {
            // This method is used to validate the basic authentication credentials
            return Ok();
        }
    }
}
