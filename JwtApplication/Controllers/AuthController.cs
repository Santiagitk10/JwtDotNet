using JwtApplication.Data;
using JwtApplication.DTOs;
using JwtApplication.Helpers;
using JwtApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public AuthController(IUserRepository repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        //[HttpPost("register")]
        //public IActionResult Register(RegisterDTO userDTO)
        //{
        //    var user = new User()
        //    {
        //        Name = userDTO.Name,
        //        Email = userDTO.Email,
        //        Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password)
        //    };

        //    return Created("success", _repository.Create(user));
        //}

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO userDTO)
        {
            try
            {
                var user = new User()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password)
                };

                return Created("success", _repository.Create(user));
            }
            catch
            {
                return Unauthorized(new
                {
                    message = "User Already in Database"
                });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = _repository.GetByEmail(loginDTO.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
            });

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _repository.GetById(userId);
                return Ok(user);
            }
            catch
            {
                // Si falla el método verify se lanzaría un error que se atraparía aquí enviando un unauthorized
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "success"
            });
        }
    }
}