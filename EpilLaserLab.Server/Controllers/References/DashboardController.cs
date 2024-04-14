using EpilLaserLab.Server.Dtos.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers.References
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class DashboardController : ControllerBase
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
            return Ok(new { Message = "OK"});
        }

        // GET: api/<DashboardController>
        [HttpGet("references")]
        public IActionResult Get()
        {
            var recs = _references.Select(kvp => new ReferenceDto {
                Name = kvp.Key,
                Title = kvp.Value
            });

            return Ok(new { Message = "OK", Recs = recs});
        }
    }
}
