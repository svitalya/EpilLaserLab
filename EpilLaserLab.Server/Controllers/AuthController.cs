using EpilLaserLab.Server.Data.Auths;
using EpilLaserLab.Server.Dtos;
using EpilLaserLab.Server.Dtos.Auths;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IUserRepository _repository,
        IAdminRepository adminRepository,
        IClientRepository _clientRepository) : ControllerBase
    {
        async Task SetTokens(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role!.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
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

            await SetTokens(user);

            return Ok(new { Message = "OK" });
        }

        [Authorize(Roles = "root, admin, client")]
        [HttpGet("user")]
        public IActionResult UserInfo()
        {
            var user = _repository.GetAuth(HttpContext);

            if (user is null)
            {
                return Ok(new { Message = "UNAUTHORIZED" });
            }


            return Ok(new { Message = "OK", User = user});
        }

        [Authorize(Roles = "root, admin, client")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { Message = "OK" });
        }

        private User RegisterUser(RegisterDto dto)
        {
            var user = new User
            {
                Login = dto.Login,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId
            };

            return user;

        }

        static object? orderById(UserInfoDto u) => u.UserId;
        static object? orderByLogin(UserInfoDto u) => u.Login;
        static object? orderByEmail(UserInfoDto u) => u.Email;
        static object? orderByRole(UserInfoDto u) => u.Role;

        [HttpGet("users")]
        public IActionResult Users(int page = 0, int limit = 10, string order = "id", string sort = "asc", string? role = null)
        {
            Dictionary<string, Func<UserInfoDto, object?>> functor = [];
            functor.Add("id", orderById);
            functor.Add("login", orderByLogin);
            functor.Add("email", orderByEmail);
            functor.Add("role", orderByRole);

            var querable = _repository.GetQuerable()
                .Select(u => new UserInfoDto
                {
                    UserId = u.UserId,
                    Login = u.Login,
                    Email = u.Email,
                    Role = u.Role.Title,
                    RoleName = u.Role.Name,
                })
                .AsQueryable();

            if(role is not null)
            {
                querable = querable.Where(u => u.RoleName == role).AsQueryable();
            }

            if (functor.TryGetValue(order, out Func<UserInfoDto, object?>? f) && f is not null)
            {
                querable = (sort == "desc"
                    ? querable.OrderByDescending(f)
                    : querable.OrderBy(f)
                ).AsQueryable();
            }
            int maxRecs = querable.Count();

            if (page + 1 * limit > maxRecs)
            {
                page = 0;
            }

            var recs = querable.Skip(page * limit)
                .Take(limit)
                .ToArray();

            return Ok(new
            {
                Data = new
                {
                    Recs = recs,
                    Page = page,
                    Max = maxRecs
                },
                Message = "OK"
            });
        }

        [HttpPost("register/clients")]
        public IActionResult RegisterClient(RegisterClientsDto dto)
        {
            try
            {
                var client = _clientRepository
                    .GetQueryable()
                    .FirstOrDefault(c => c.Phone == dto.Phone);
                if (client is null)
                {
                    client = new Client
                    {
                        Name = dto.Name,
                        Phone = dto.Phone,
                        User = RegisterUser(dto),
                    };
                    _clientRepository.Add(client);
                }
                else
                {
                    client.User = RegisterUser(dto);
                    _clientRepository.Update();
                }

                return Ok(new { Message = "OK" });
            }
            catch
            {
                return Ok(new { Message = "BLOCK" });
            }

        }

        [HttpPost("register/admins")]
        public IActionResult RegisterAdmin(RegisterAdminDto dto)
        {

            var admin = new Admin
            {
                BranchId = dto.BranchId,
                User = RegisterUser(dto),
                Employee = new Employee
                {
                    Surname = dto.Surname,
                    Name = dto.Name,
                    Patronymic = dto.Patronymic,
                    IsWork = true,
                }
            };

            if (adminRepository.CheckForDuplication(admin)) {
                adminRepository.Add(admin);
                return Ok(new { Message = "OK" });
            }

            return Ok(new { Message = "DUPLICATION" });


        }
    }
}
