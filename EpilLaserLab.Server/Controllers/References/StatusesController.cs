using EpilLaserLab.Server.Data.References;
using EpilLaserLab.Server.Dtos.References;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authorization;
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
                Name = s.Name,
                AccessDelete = _repository.AccessDelete(s),
            }).AsEnumerable();

            var querable = (sort == "asc" ? data.OrderBy(r => r.Name) : data.OrderByDescending(r => r.Name)).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                querable = querable.Where(r => r.Name.StartsWith(search, StringComparison.CurrentCultureIgnoreCase));
            }

            var maxRecs = querable.Count();

            if ((page + 1 * limit) > maxRecs)
            {
                page = 0;
            }


            return Ok(new
            {
                Data = new
                {
                    Recs = querable.AsQueryable()
                    .Skip(page * limit)
                    .Take(limit),
                    Page = page,
                    Max = maxRecs
                },
                Message = "OK"
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var status = _repository.Get(id);

            if (status is null)
            {
                return Ok(new { Message = "NOT FOUND" });
            };

            return Ok(new
            {
                Rec = new ReferenceRec()
                {
                    Id = status.StatusId,
                    Name = status.Name,
                    AccessDelete = _repository.AccessDelete(status)
                },
                Message = "OK"
            });

        }

        [HttpPost]
        public IActionResult Create(ReferenceRecCreate referenceRec)
        {
            Status status = new()
            {
                Name = referenceRec.Name,
            };

            try
            {
                if (_repository.CheckForDuplication(status) && _repository.Add(status))
                {
                    return Ok(new { Message = "OK" });
                }
                else
                {
                    return Ok(new { Message = "DUPLICATION" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ReferenceRecCreate referenceRec)
        {
            try
            {
                Status? statusOld = _repository.Get(id);

                if (statusOld is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                Status statusNew = new()
                {
                    Name = referenceRec.Name,
                };

                if (_repository.CheckForDuplication(statusNew) && _repository.Update(statusOld, statusNew))
                {
                    return Ok(new { Message = "OK" });
                }
                else
                {
                    return Ok(new { Message = "DUPLICATION" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Status? tag = _repository.Get(id);

                if (tag is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if (_repository.AccessDelete(tag) && _repository.Delete(tag))
                {
                    return Ok(new { Message = "OK" });
                }
                else
                {
                    return Ok(new { Message = "BLOCK" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
