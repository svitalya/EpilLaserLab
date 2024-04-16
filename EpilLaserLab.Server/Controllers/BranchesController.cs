using EpilLaserLab.Server.Data.Branches;
using EpilLaserLab.Server.Dtos;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController(
        IBranchRepository branchRepository,
        ImageSaveService imageSaveService) : ControllerBase
    {
        private readonly IBranchRepository _branchRepository = branchRepository;
        private readonly ImageSaveService _imageSaveService = imageSaveService;

        [HttpGet]
        public IActionResult GetList(int page = 0, int limit = 10, string sort = "asc")
        {
            var querable = _branchRepository.GetQuerable();

            querable = (sort == "desc"
                ? querable.OrderByDescending(b => b.Address)
                : querable.OrderBy(b => b.Address)).AsQueryable();

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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var branch = _branchRepository.Get(id);

            if (branch is null)
            {
                return Ok(new { Message = "NOT FOUND" });
            };

            return Ok(new
            {
                Rec = branch,
                Message = "OK"
            });

        }

        [HttpPost]
        public IActionResult Create([FromBody] BranchCreateDto branchCreateDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(branchCreateDto.Address)
                    || string.IsNullOrWhiteSpace(branchCreateDto.PhotoFile)
                    )
                {
                    return Ok(new { Message = "DATA NOT VALID" });
                }

                Branch branch = new Branch()
                {
                    Address = branchCreateDto.Address
                };

                if (!(_branchRepository.CheckForDuplication(branch)))
                {
                    return Ok(new { Message = "DUPLICATION" });
                }
                string filePath = _imageSaveService.SaveFile(branchCreateDto.PhotoFile);
                branch.PhotoPath = filePath;
                _branchRepository.Add(branch);

                return Ok(new { Message = "OK" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BranchUpdateDto branchUpdateDto)
        {
            try
            {
                Branch? branchOld = _branchRepository.Get(id);

                if (branchOld is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if(string.IsNullOrWhiteSpace(branchUpdateDto.Address))
                {
                    return Ok(new { Message = "DATA NOT VALID" });
                }

                Branch branchNew = new()
                {
                    BranchId = branchOld.BranchId,
                    Address = branchUpdateDto.Address,
                    PhotoPath = string.Empty
                };

                bool isChanged = false;
                if (!(branchOld.Address == branchNew.Address))
                {
                    if (_branchRepository.CheckForDuplication(branchNew))
                    {

                        isChanged = true;
                    }
                    else
                    {
                        return Ok(new { Message = "DUPLICATION" });
                    }
                }

                if (branchUpdateDto.PhotoFile != string.Empty && branchUpdateDto.PhotoFile is not null)
                {
                    isChanged = true;

                    _imageSaveService.DeleteFile(branchOld.PhotoPath);

                    string filePath = _imageSaveService.SaveFile(branchUpdateDto.PhotoFile);
                    branchNew.PhotoPath = filePath;
                }

                if (!isChanged)
                {
                    return Ok(new { Message = "NOT CHANGED" });
                }

                _branchRepository.Update(branchOld, branchNew);

                return Ok(new { Message = "OK" });
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
                Branch? branch = _branchRepository.Get(id);

                if (branch is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if (!(_branchRepository.AccessDelete(branch) && _branchRepository.Delete(branch)))
                {

                    return Ok(new { Message = "BLOCK" });
                }

                _imageSaveService.DeleteFile(branch.PhotoPath);

                return Ok(new { Message = "OK" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
