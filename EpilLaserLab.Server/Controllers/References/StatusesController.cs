using EpilLaserLab.Server.Data.References;
using EpilLaserLab.Server.Dtos.References;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace EpilLaserLab.Server.Controllers.References
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusRepository _repository;

        public StatusesController(IStatusRepository repository)
        {
            _repository = repository;
        }




        [HttpGet]
        public IActionResult GetList(int page = 0, int limit = 10, string sort = "asc", string? search = null)
        {
            IEnumerable<ReferenceRec> data = _repository.GetAll().Select(s => new ReferenceRec()
            {
                Id = s.StatusId,
                Name = s.Name
            }).AsEnumerable();

            var querable = (sort == "asc" ? data.OrderBy(r => r.Name) : data.OrderByDescending(r => r.Name)).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                querable = querable.Where(r => r.Name.StartsWith(search));
            }

            var maxRecs = querable.Count();

            if((page+1 * limit) > maxRecs)
            {
                page = 0;
            } 


            return Ok(new { Data = querable.AsQueryable().Skip(page * limit).Take(limit), Page = page, Max = maxRecs });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var status = _repository.Get(id);

            if (status is null)
            {
                return NotFound();
            };

            return Ok(new ReferenceRec()
            {
                Id = status.StatusId,
                Name = status.Name
            });

        }

        [HttpPost]
        public IActionResult Create(ReferenceRecCreate referenceRec)
        {
            Status status = new Status()
            {
                Name = referenceRec.Name,
            };

            try
            {
                _repository.Add(status);
                return Ok(new { message = "OK"});
            }catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }          
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ReferenceRecCreate referenceRec)
        {
            Status status = new Status()
            {
                Name = referenceRec.Name,
            };

            try
            {
                _repository.Update(id, status);
                return Ok(new { message = "OK" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok(new { message = "OK" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
