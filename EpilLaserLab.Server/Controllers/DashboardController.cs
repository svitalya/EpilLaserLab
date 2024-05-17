using EpilLaserLab.Server.Data.Auths;
using EpilLaserLab.Server.Dtos.References;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers
{

    public class NavItem
    {
        public string? Svg { get; set; }
        public string Text { get; set; } = string.Empty;

        public string? Link { get; set; }

        public object Params { get; set; }

        public List<NavItem>? Dropdown {  get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "root, admin")]
    public class DashboardController(IUserRepository userRepository) : ControllerBase
    {
        private Dictionary<string, string> _references = new()
        {
            {"tags", "Теги"},
            {"statuses", "Статусы"},
            {"categories", "Категории"},
            {"types", "Виды прайслистов"},
        };

        [HttpGet]
        public IActionResult AccessCheck()
        {
            return Ok(new { Message = "OK" });
        }

        // GET: api/<DashboardController>
        [HttpGet("references")]
        public IActionResult Get()
        {
            var recs = _references.Select(kvp => new ReferenceDto
            {
                Name = kvp.Key,
                Title = kvp.Value
            });

            return Ok(new { Message = "OK", Recs = recs });
        }



        [HttpGet("sidebar")]
        public IActionResult GetSideBar() {
            List<NavItem> rootNavItem = new List<NavItem>()
            {
                new NavItem
                {
                    Svg = "<path d=\"M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z\"></path><polyline points=\"9 22 9 12 15 12 15 22\"></polyline>",
                    Text = "Главная",
                    Link = "dashboard.home"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Справочники",
                    Link = "dashboard.references"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Услуги",
                    Link = "dashboard.services"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Абонименты",
                    Link = "dashboard.seasontickets"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Филиалы",
                    Link = "dashboard.branches"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Мастера",
                    Link = "dashboard.masters"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Расписание",
                    Link = "dashboard.schedules"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Пользователи",
                    Link = "dashboard.users",
                    Dropdown = new List<NavItem>
                    {
                        new NavItem
                        {
                            Text = "Все",
                            Params = new {}
                        },
                        new NavItem
                        {
                            Text = "Администраторы",
                            Params = new { Role = "admin" }
                        },
                        new NavItem
                        {
                            Text = "Клиенты",
                            Params = new { Role = "client" }
                        },
                    }
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 15H21M3 19H13M21 7H13M21 11H13M4.6 11H7.4C7.96005 11 8.24008 11 8.45399 10.891C8.64215 10.7951 8.79513 10.6422 8.89101 10.454C9 10.2401 9 9.96005 9 9.4V6.6C9 6.03995 9 5.75992 8.89101 5.54601C8.79513 5.35785 8.64215 5.20487 8.45399 5.10899C8.24008 5 7.96005 5 7.4 5H4.6C4.03995 5 3.75992 5 3.54601 5.10899C3.35785 5.20487 3.20487 5.35785 3.10899 5.54601C3 5.75992 3 6.03995 3 6.6V9.4C3 9.96005 3 10.2401 3.10899 10.454C3.20487 10.6422 3.35785 10.7951 3.54601 10.891C3.75992 11 4.03995 11 4.6 11Z\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/>",
                    Text = "Документы",
                    Link = "dashboard.docs"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 15H21M3 19H13M21 7H13M21 11H13M4.6 11H7.4C7.96005 11 8.24008 11 8.45399 10.891C8.64215 10.7951 8.79513 10.6422 8.89101 10.454C9 10.2401 9 9.96005 9 9.4V6.6C9 6.03995 9 5.75992 8.89101 5.54601C8.79513 5.35785 8.64215 5.20487 8.45399 5.10899C8.24008 5 7.96005 5 7.4 5H4.6C4.03995 5 3.75992 5 3.54601 5.10899C3.35785 5.20487 3.20487 5.35785 3.10899 5.54601C3 5.75992 3 6.03995 3 6.6V9.4C3 9.96005 3 10.2401 3.10899 10.454C3.20487 10.6422 3.35785 10.7951 3.54601 10.891C3.75992 11 4.03995 11 4.6 11Z\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/>",
                    Text = "БД",
                    Link = "dashboard.db"
                },

            };
            List<NavItem> adminNavItem = new List<NavItem>()
            {
                new NavItem
                {
                    Svg = "<path d=\"M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z\"></path><polyline points=\"9 22 9 12 15 12 15 22\"></polyline>",
                    Text = "Главная",
                    Link = "dashboard.home"
                },  
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Мастера",
                    Link = "dashboard.masters"
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 9.5H21M3 14.5H21M8 4.5V19.5M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V7.7C21 6.5799 21 6.01984 20.782 5.59202C20.5903 5.21569 20.2843 4.90973 19.908 4.71799C19.4802 4.5 18.9201 4.5 17.8 4.5H6.2C5.0799 4.5 4.51984 4.5 4.09202 4.71799C3.71569 4.90973 3.40973 5.21569 3.21799 5.59202C3 6.01984 3 6.57989 3 7.7V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5Z\" stroke-width=\"2\"/>",
                    Text = "Клиенты",
                    Link = "dashboard.users",
                    Params = new { Role = "client"}
                },
                new NavItem
                {
                    Svg = "<path xmlns=\"http://www.w3.org/2000/svg\" d=\"M3 15H21M3 19H13M21 7H13M21 11H13M4.6 11H7.4C7.96005 11 8.24008 11 8.45399 10.891C8.64215 10.7951 8.79513 10.6422 8.89101 10.454C9 10.2401 9 9.96005 9 9.4V6.6C9 6.03995 9 5.75992 8.89101 5.54601C8.79513 5.35785 8.64215 5.20487 8.45399 5.10899C8.24008 5 7.96005 5 7.4 5H4.6C4.03995 5 3.75992 5 3.54601 5.10899C3.35785 5.20487 3.20487 5.35785 3.10899 5.54601C3 5.75992 3 6.03995 3 6.6V9.4C3 9.96005 3 10.2401 3.10899 10.454C3.20487 10.6422 3.35785 10.7951 3.54601 10.891C3.75992 11 4.03995 11 4.6 11Z\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/>",
                    Text = "Документы",
                    Link = "dashboard.docs"
                },

            };

            Dictionary<string, List<NavItem>> navItemsByRole = new Dictionary<string, List<NavItem>>()
            {
                {"root", rootNavItem},
                {"admin", adminNavItem},
            };


            string idStr = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?
                .ToString()
                .Split(':')[2]
                .Trim()
                ?? "-1";

            int userId = int.Parse(idStr);
            var user = userRepository.GetById(userId);

            return Ok(new { Message = "OK", NavItems = navItemsByRole[user.Role.Name] });

        }
    }
}
