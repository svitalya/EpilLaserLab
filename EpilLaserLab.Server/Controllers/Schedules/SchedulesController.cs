using EpilLaserLab.Server.Data.Branches;
using EpilLaserLab.Server.Data.Employees;
using EpilLaserLab.Server.Data.Schedules;
using EpilLaserLab.Server.Dtos.Employees;
using EpilLaserLab.Server.Dtos.Employees.Masters;
using EpilLaserLab.Server.Dtos.Schedules;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EpilLaserLab.Server.Controllers.Schedules
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController(
        ISchedulesRepository schedulesRepository,
        IMasterRepository masterRepository
        ) : ControllerBase
    {

        object? OrderById(Schedule s) => s.ScheduleId;
        object? OrderByMaster(Schedule s) => s.Master.Employee.FIO;
        object? OrderByDate(Schedule s) => s.Date;

        [HttpGet]
        public IActionResult GetList(int page = 0, int limit = 10, string order = "scheduleId", string sort = "asc", int? masterId = null)
        {
            Dictionary<string, Func<Schedule, object?>> functor = [];

            functor.Add("scheduleId", OrderById);
            functor.Add("master", OrderByMaster);
            functor.Add("date", OrderByDate);

            var querable = schedulesRepository.GetQueryable();
            if (functor.TryGetValue(order, out Func<Schedule, object?>? f) && f is not null)
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

            if(masterId is not null)
            {
                querable = querable.Where(s => s.MasterId == masterId);
            }

            var now = DateTime.Now.ToDateOnly().ToDateTime(new TimeOnly(0, 0, 0));

            var recs = querable.AsQueryable()
                .Where(s => s.Date >= now)
                .Skip(page * limit)
                .Take(limit)
                .Select(s => new ScheduleRecTableDto
                {
                    ScheduleId = s.ScheduleId,
                    MasterId = s.MasterId,
                    Date = s.Date,
                    Master = new MasterRecTableDto() { FIO = s.Master.Employee.FIO }
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
            var rec = schedulesRepository.Get(id);

            if (rec is null)
            {
                return Ok(new { Message = "NOT FOUND" });
            };

            return Ok(new
            {
                Rec = new ScheduleDto
                {
                    ScheduleId = rec.ScheduleId,
                    MasterId = rec.MasterId,
                    Date = rec.Date,
                },
                Message = "OK"
            });

        }

        [HttpPost]
        public IActionResult Create([FromBody] ScheduleDto scheduleDto)
        {
            try
            {

                DateOnly dateo = DateTime.Now.ToDateOnly();
                DateTime now = dateo.ToDateTime(new TimeOnly(0, 0, 0));

                if (masterRepository.Get(scheduleDto.MasterId) is null
                    || scheduleDto.Date < now)
                {
                    return Ok(new { Message = "DATA NOT VALID" });
                }

                Schedule schedule = new Schedule()
                {
                    MasterId = scheduleDto.MasterId,
                    Date = scheduleDto.Date,
                };


                if (schedulesRepository.CheckForDuplication(schedule))
                {
                    schedulesRepository.Add(schedule);
                    return Ok(new { Message = "OK" });
                }

                return Ok(new { Message = "DUPLICATION" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ScheduleDto scheduleDto)
        {
            try
            {
                Schedule? scheduleOld = schedulesRepository.Get(id);

                if (scheduleOld is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                DateOnly dateo = DateTime.Now.ToDateOnly();
                DateTime now = dateo.ToDateTime(new TimeOnly(0, 0, 0));

                if (masterRepository.Get(scheduleDto.MasterId) is null
                    || scheduleDto.Date < now)
                {
                    return Ok(new { Message = "DATA NOT VALID" });
                }


                Schedule scheduleNew = new Schedule()
                {
                    ScheduleId = scheduleOld.ScheduleId,
                    Date = scheduleDto.Date,
                    MasterId = scheduleDto.MasterId
                };

                bool isChanged = false;

                if (!(scheduleNew.Date == scheduleOld.Date
                    && scheduleNew.MasterId == scheduleOld.MasterId))
                {
                    if (schedulesRepository.CheckForDuplication(scheduleNew))
                    {
                        isChanged = true;
                    }
                    else
                    {
                        return Ok(new { Message = "DUPLICATION" });
                    }
                }

                if (!isChanged)
                {
                    return Ok(new { Message = "NOT CHANGED" });
                }

                schedulesRepository.Update(scheduleOld, scheduleNew);

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
                Schedule? schedule = schedulesRepository.Get(id);

                if (schedule is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if (!schedulesRepository.AccessDelete(schedule))
                {

                    return Ok(new { Message = "BLOCK" });
                }


                schedulesRepository.Delete(schedule);

                return Ok(new { Message = "OK" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}
