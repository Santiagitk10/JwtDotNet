using Microsoft.AspNetCore.Mvc;

namespace JwtApplication.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("Success");
        }
    }
}