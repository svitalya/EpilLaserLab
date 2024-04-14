using EpilLaserLab.Server.Dtos.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers.References
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private Dictionary<string, string> _references = new()
        {
            {"tags", "Теги"},
            {"statuses", "Статусы"},
        };

        // GET: api/<ReferencesController>
        [HttpGet]
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
