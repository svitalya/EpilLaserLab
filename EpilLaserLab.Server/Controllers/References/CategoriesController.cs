using EpilLaserLab.Server.Data.References;
using EpilLaserLab.Server.Dtos.References;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpilLaserLab.Server.Controllers.References
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetList(int page = 0, int limit = 10, string sort = "asc", string? search = null)
        {
            IEnumerable<ReferenceRec> data = _repository.GetAll().Select(t => new ReferenceRec()
            {
                Id = t.CategoryId,
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
                    Id = tag.CategoryId,
                    Name = tag.Name,
                    AccessDelete = _repository.AccessDelete(tag)
                },
                Message = "OK"
            });

        }

        [HttpPost]
        public IActionResult Create(ReferenceRecCreate referenceRec)
        {
            Category category = new()
            {
                Name = referenceRec.Name,
            };

            try
            {
                if (_repository.CheckForDuplication(category) && _repository.Add(category))
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
                Category? categoryOld = _repository.Get(id);

                if (categoryOld is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                Category categoryNew = new()
                {
                    Name = referenceRec.Name,
                };

                if (_repository.CheckForDuplication(categoryNew) && _repository.Update(categoryOld, categoryNew))
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
                Category? category = _repository.Get(id);

                if (category is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if (_repository.AccessDelete(category) && _repository.Delete(category))
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
