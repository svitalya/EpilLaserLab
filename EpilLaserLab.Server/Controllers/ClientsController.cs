using EpilLaserLab.Server.Data.Auths;
using EpilLaserLab.Server.Dtos.References;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController(
        IClientRepository clientRepository): ControllerBase
    {
        [HttpGet]
        public IActionResult GetList(int page = 0, int limit = 10)
        {

            var queryable = clientRepository.GetQueryable();

            int maxRecs = queryable.Count();

            if ((page + 1 * limit) > maxRecs)
            {
                page = 0;
            }


            return Ok(new
            {
                Data = new
                {
                    Recs = queryable.AsQueryable()
                    .Skip(page * limit)
                    .Take(limit),
                    Page = page,
                    Max = maxRecs
                },
                Message = "OK"
            });
        }
    }
}
