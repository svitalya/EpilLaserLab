using EpilLaserLab.Server.Data.UserData;
using EpilLaserLab.Server.Dtos.Auth;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;

        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = _repository.GetByLogin(loginDto.Login);

            if (user == null)
            {
                return Ok(new { Message = "INVALID CREDENTIALS" });
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Ok(new { Message = "INVALID CREDENTIALS" });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role!.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return Ok(new { Message = "OK" });
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("user")]
        public IActionResult UserInfo()
        {
            string idStr = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?
                .ToString()
                .Split(':')[2]
                .Trim()
                ?? "-1";

            int userId = int.Parse(idStr);
            var user = _repository.GetById(userId);

            if (user is null)
            {
                return Ok(new { Message = "UNAUTHORIZED" });
            }

            return Ok(new { Message = "OK", User = user});
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { Message = "OK" });
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
