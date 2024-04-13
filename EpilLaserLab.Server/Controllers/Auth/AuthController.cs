using EpilLaserLab.Server.Data.UserData;
using EpilLaserLab.Server.Dtos.Auth;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _repository.GetByLogin(loginDto.Login);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.UserId);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { message = "success" });
        }


        [HttpGet("user")]
        public IActionResult UserInfo()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
            {
                return Unauthorized();
            }

            var token = _jwtService.Verify(jwt);

            int userId = int.Parse(token.Issuer);
            var user = _repository.GetById(userId);

            if (user is null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "success" });
        }

        //[HttpPost("register")]
        //public IActionResult Register(RegisterDto dto)
        //{
        //    var user = new User
        //    {
        //        Login = dto.Login,
        //        Email = dto.Email ?? null,
        //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        //        RoleId = dto.RoleId
        //    };

        //    return Created("success", _repository.Create(user));
        //}
    }
}
