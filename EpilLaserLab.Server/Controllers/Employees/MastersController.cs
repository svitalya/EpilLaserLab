using EpilLaserLab.Server.Data.Branches;
using EpilLaserLab.Server.Data.Employees;
using EpilLaserLab.Server.Dtos;
using EpilLaserLab.Server.Dtos.Branchs;
using EpilLaserLab.Server.Dtos.Employees;
using EpilLaserLab.Server.Dtos.Employees.Masters;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EpilLaserLab.Server.Controllers.Employees
{
    [Route("api/[controller]")]
    [ApiController]
    public class MastersController(
        IMasterRepository masterRepository,
        IEmployeRepository employeRepository,
        IBranchRepository branchRepository,
        ImageSaveService imageSaveService
        ): ControllerBase
    {
        object? OrderById(Master m) => m.MasterId;
        object? OrderByFIO(Master m) => m.Employee.FIO;
        object? OrderByBranch(Master m) => m.Branch.Address;

        [HttpGet]
        public IActionResult GetList(int page = 0,
            int limit = 10,
            string order = "masterId",
            string sort = "asc",
            int? branchId = null)
        {
            Dictionary<string, Func<Master, object?>> functor = [];

            functor.Add("masterId", OrderById);
            functor.Add("FIO", OrderByFIO);
            functor.Add("branch", OrderByBranch);

            var querable = masterRepository.GetQueryable();
            if (functor.TryGetValue(order, out Func<Master, object?>? f) && f is not null)
            {
                querable = (sort == "desc"
                    ? querable.OrderByDescending(f)
                    : querable.OrderBy(f)
                ).AsQueryable();
            }

            int maxRecs = querable.Count();

            if (page + 1 * limit > maxRecs)
            {
                page = 0;
            }

            if(branchId is not null)
            {
                querable = querable.Where(m => m.BranchId == branchId).AsQueryable();
            }


            var recs = querable.AsQueryable()
                .Skip(page * limit)
                .Take(limit)
                .Select(m => new MasterRecTableDto
                {
                    MasterId = m.MasterId,
                    FIO = m.Employee.FIO,
                    PhotoPath = m.PhotoPath,
                    Address = m.Branch.Address
                })
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
            var rec = masterRepository.Get(id);

            if (rec is null)
            {
                return Ok(new { Message = "NOT FOUND" });
            };

            return Ok(new
            {
                Rec = new MasterGetDto
                {
                    MasterId = rec.MasterId,
                    PhotoPath = rec.PhotoPath,
                    Employee = new EmployeeDto {
                        Surname = rec.Employee.Surname,
                        Name = rec.Employee.Name,
                        Patronymic = rec.Employee.Patronymic,
                        IsWork = rec.Employee.IsWork,
                    },
                    BranchId = rec.BranchId,

                },
                Message = "OK"
            });

        }

        [HttpPost]
        public IActionResult Create([FromBody] MasterCreateDto masterCreateDto)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(masterCreateDto.Employee.Surname)
                    || string.IsNullOrWhiteSpace(masterCreateDto.Employee.Name)
                    || string.IsNullOrWhiteSpace(masterCreateDto.Photo)
                    || branchRepository.Get(masterCreateDto.BranchId) is null
                    )
                {
                    return Ok(new { Message = "DATA NOT VALID" });
                }


                Employee employee = new Employee()
                {
                    Surname = masterCreateDto.Employee.Surname,
                    Name = masterCreateDto.Employee.Name,
                    Patronymic = masterCreateDto.Employee.Patronymic,
                };

                Master master = new Master()
                {
                    BranchId = masterCreateDto.BranchId,
                    Employee = employee,
                    
                };

            Debug.WriteLine(masterCreateDto.BranchId);

                if ((employeRepository.CheckForDuplication(employee)
                    && masterRepository.CheckForDuplication(master)))
                {
                    master.PhotoPath = imageSaveService.SaveFile(masterCreateDto.Photo);

                    employeRepository.Add(employee);
                    masterRepository.Add(master);

                    return Ok(new { Message = "OK" });

                }

                return Ok(new { Message = "DUPLICATION" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message});
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MasterUpdateDto masterUpdateDto)
        {
            try
            {
                Master? masterOld = masterRepository.Get(id);

                if (masterOld is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if (string.IsNullOrWhiteSpace(masterUpdateDto.Employee.Surname)
                    || string.IsNullOrWhiteSpace(masterUpdateDto.Employee.Name)
                    || branchRepository.Get(masterUpdateDto.BranchId) is null
                    )
                {
                    return Ok(new { Message = "DATA NOT VALID" });
                }

                Employee employeeNew = new Employee() { 
                    EmployeeId = masterOld.EmployeeId,
                    Surname = masterUpdateDto.Employee.Surname,
                    Name = masterUpdateDto.Employee.Name,
                    Patronymic = masterUpdateDto.Employee.Patronymic,
                    IsWork = masterUpdateDto.Employee.IsWork,
                };
    

                Master masterNew = new()
                {
                    MasterId = masterOld.MasterId,
                    BranchId = masterUpdateDto.BranchId,
                };

                bool isChanged = false;

                if (!(employeeNew.Surname == masterOld.Employee.Surname
                    && employeeNew.Name == masterOld.Employee.Name
                    && employeeNew.Patronymic == masterOld.Employee.Patronymic
                    && employeeNew.IsWork == masterOld.Employee.IsWork
                ))
                {
                    if (employeRepository.CheckForDuplication(employeeNew))
                    {

                        isChanged = true;
                    }
                    else
                    {
                        return Ok(new { Message = "DUPLICATION" });
                    }
                }

                if (!(masterNew.BranchId == masterOld.BranchId))
                {
                    if (masterRepository.CheckForDuplication(masterNew))
                    {

                        isChanged = true;
                    }
                    else
                    {
                        return Ok(new { Message = "DUPLICATION" });
                    }
                }


                if (masterUpdateDto.Photo != string.Empty && masterUpdateDto.Photo is not null)
                {
                    isChanged = true;

                    imageSaveService.DeleteFile(masterOld.PhotoPath);

                    string filePath = imageSaveService.SaveFile(masterUpdateDto.Photo);
                    masterNew.PhotoPath = filePath;
                }
                else {
                    masterNew.PhotoPath = masterOld.PhotoPath;
                }



                if (!isChanged)
                {
                    return Ok(new { Message = "NOT CHANGED" });
                }

                employeRepository.Update(masterOld.Employee, employeeNew);
                masterRepository.Update(masterOld, masterNew);

                return Ok(new { Message = "OK" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message});
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Master? master = masterRepository.Get(id);

                if (master is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if (!(masterRepository.AccessDelete(master) && employeRepository.AccessDelete(master.Employee)))
                {

                    return Ok(new { Message = "BLOCK" });
                }

                
                imageSaveService.DeleteFile(master.PhotoPath);

                masterRepository.Delete(master);
                employeRepository.Delete(master.Employee);

                return Ok(new { Message = "OK" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
