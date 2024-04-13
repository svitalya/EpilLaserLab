using EpilLaserLab.Server.Dtos.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers.References
{
    [Route("api/[controller]")]
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
        public IEnumerable<ReferenceDto> Get()
        {
            return _references.Select(kvp => new ReferenceDto { Name = kvp.Key, Title = kvp.Value });
        }
    }
}
