using EpilLaserLab.Server.Data.References;
using EpilLaserLab.Server.Dtos.References;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Type = EpilLaserLab.Server.Models.Type;

namespace EpilLaserLab.Server.Controllers.References
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ITypeRepository _repository;

        public TypesController(ITypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetList(int page = 0, int limit = 10, string sort = "asc", string? search = null)
        {
            IEnumerable<ReferenceRec> data = _repository.GetAll().Select(t => new ReferenceRec()
            {
                Id = t.TypeId,
                Name = t.Name,
                AccessDelete = _repository.AccessDelete(t)
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
            var tag = _repository.Get(id);

            if (tag is null)
            {
                return Ok(new { Message = "NOT FOUND" });
            };

            return Ok(new
            {
                Rec = new ReferenceRec()
                {
                    Id = tag.TypeId,
                    Name = tag.Name,
                    AccessDelete = _repository.AccessDelete(tag)
                },
                Message = "OK"
            });

        }

        [HttpPost]
        public IActionResult Create(ReferenceRecCreate referenceRec)
        {
            Type type = new()
            {
                Name = referenceRec.Name,
            };

            try
            {
                if (_repository.CheckForDuplication(type) && _repository.Add(type))
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
                Type? typeOld = _repository.Get(id);

                if (typeOld is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                Type typeNew = new()
                {
                    Name = referenceRec.Name,
                };

                if (_repository.CheckForDuplication(typeNew) && _repository.Update(typeOld, typeNew))
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
                Type? type = _repository.Get(id);

                if (type is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if (_repository.AccessDelete(type) && _repository.Delete(type))
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
