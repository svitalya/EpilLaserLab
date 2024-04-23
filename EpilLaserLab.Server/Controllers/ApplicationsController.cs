using EpilLaserLab.Server.Data.Applications;
using EpilLaserLab.Server.Data.Schedules;
using EpilLaserLab.Server.Data.Services;
using EpilLaserLab.Server.Dtos;
using EpilLaserLab.Server.Dtos.Application;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController(
        IApplicationsRepository applicationsRepository,
        IServiceRepository serviceRepository,
        IServicePricesRepository servicePricesRepository,
        IIntervalsRepository intervalsRepository,
        ISchedulesRepository schedulesRepository) : ControllerBase
    {

        object? orderById(ApplicationRecTableDto a) => a.ApplicationId;
        object? orderByBranch(ApplicationRecTableDto a) => a.Branch;
        object? orderByMaster(ApplicationRecTableDto a) => a.Master;
        object? orderByClient(ApplicationRecTableDto a) => a.Client;
        object? orderByDateTime(ApplicationRecTableDto a) => a.DateTime;
        object? orderByService(ApplicationRecTableDto a) => a.Service;
        object? orderByPrice(ApplicationRecTableDto a) => a.Price;

        [HttpGet]
        public IActionResult GetList(
            [FromQuery] int page = 0,
            [FromQuery] int limit = 10,
            [FromQuery] string order = "dateTime",
            [FromQuery] string sort = "desc")
        {
            Dictionary<string, Func<ApplicationRecTableDto, object?>> functor = [];

            functor.Add("applicationId", orderById);
            functor.Add("branch", orderByBranch);
            functor.Add("master", orderByMaster);
            functor.Add("client", orderByClient);
            functor.Add("dateTime", orderByDateTime);
            functor.Add("service", orderByService);
            functor.Add("price", orderByPrice);

            var querable = applicationsRepository.GetQuerable()
                .Include(a => a.Client)
                .Include(a => a.ServicePrice)
                    .ThenInclude(sp => sp.Service)
                .Include(a => a.ServicePrice)
                    .ThenInclude(sp => sp.Type)
                .Include(a => a.Interval)
                    .ThenInclude(i => i.Schedule)
                        .ThenInclude(s => s.Master)
                            .ThenInclude(m => m.Branch)
                .Include(a => a.Interval)
                    .ThenInclude(i => i.Schedule)
                        .ThenInclude(s => s.Master)
                            .ThenInclude(m => m.Employee)
                .Select(a => new ApplicationRecTableDto
                {
                    ApplicationId = a.ApplicationId,
                    Branch = a.Interval.Schedule.Master.Branch.Address,
                    Master = a.Interval.Schedule.Master.Employee.FIO,
                    Client = a.Client.Name,
                    DateTime = (a.Interval.Schedule.Date.AddMinutes(a.Interval.TimeStart)).ToString("dd.MM HH:mm"),
                    Service = $"{a.ServicePrice.Service.Name}:{a.ServicePrice.Type.Name}",
                    Price = a.ServicePrice.Price.ToString("C")
                })
                .AsQueryable();

            Func<ApplicationRecTableDto, object?>? f;
            if (!functor.TryGetValue(order, out f) || f is null)
            {
                f = functor["dateTime"];
            }

            querable = (sort == "asc"
                ? querable.OrderBy(f)
                : querable.OrderByDescending(f)
            ).AsQueryable();

            int maxRecs = querable.Count();

            if (page + 1 * limit > maxRecs)
            {
                page = 0;
            }


            var recs = querable.AsQueryable()
                .Skip(page * limit)
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

        [HttpPost]
        public IActionResult Create(ApplicationCreateDto applicationCreateDto)
        {
            try
            {
                var service = serviceRepository.Get(applicationCreateDto.ServiceId);

                var servicePrice = servicePricesRepository.GetQueryable()
                        .Where(sp => sp.TypeId == applicationCreateDto.TypeId
                            && sp.ServiceId == applicationCreateDto.ServiceId)
                        .OrderByDescending(sp => sp.DateTime)
                        .FirstOrDefault();



                if (service is null || servicePrice is null) return BadRequest();


                var timeStart = applicationCreateDto.TimeStart.GetMinuts();
                var timeEnd = service.TimeCost + timeStart;
                var interval = intervalsRepository.InsertIntervalInInterval(
                    applicationCreateDto.ScheduleId, timeStart, (int)timeEnd);

                if (interval is null) return BadRequest();

                Application application = new Application()
                {
                    ClientId = applicationCreateDto.ClientId,
                    Interval = interval,
                    CategoryId = applicationCreateDto.CategoryId,
                    ServicePrice = servicePrice,
                    DateTimeCreated = DateTime.Now
                };

                if (!(applicationsRepository.CheckForDuplication(application)
                    && applicationsRepository.Add(application)))
                {

                    return Ok(new { Message = "DUPLICATION" });
                }



                return Ok(new { Message = "OK" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [HttpPut("{id}")]
        public IActionResult Create(int id, ApplicationCreateDto applicationCreateDto)
        {
            try
            {
                var oldApplication = applicationsRepository.Get(id);

                if(oldApplication is null)
                {
                    return Ok(new { Message = "NOT FOUND"});
                }


                var service = serviceRepository.Get(applicationCreateDto.ServiceId);

                var servicePrice = servicePricesRepository.GetQueryable()
                        .Where(sp => sp.TypeId == applicationCreateDto.TypeId
                            && sp.ServiceId == applicationCreateDto.ServiceId)
                        .OrderByDescending(sp => sp.DateTime)
                        .FirstOrDefault();

                if (service is null || servicePrice is null) return BadRequest();

                bool isUpdateInterval = false;
                Interval? interval = oldApplication.Interval;
                if (applicationCreateDto.TimeStart.GetMinuts() != oldApplication.Interval.TimeStart)
                {
                    var timeStart = applicationCreateDto.TimeStart.GetMinuts();
                    var timeEnd = service.TimeCost + timeStart;
                    interval = intervalsRepository.InsertIntervalInInterval(
                        applicationCreateDto.ScheduleId, timeStart, (int)timeEnd);

                    if (interval is null) return BadRequest();
                    isUpdateInterval = true;
                }

                Application newApplication = new Application()
                {
                    ApplicationId = oldApplication.ApplicationId,
                    ClientId = applicationCreateDto.ClientId,
                    Interval = interval,
                    CategoryId = applicationCreateDto.CategoryId,
                    ServicePrice = servicePrice,
                    DateTimeCreated = DateTime.Now
                };

                if (!(applicationsRepository.CheckForDuplication(newApplication)))
                {

                    return Ok(new { Message = "DUPLICATION" });
                }

                if(isUpdateInterval) intervalsRepository.Delete(oldApplication.Interval);
                applicationsRepository.Update(oldApplication, newApplication);
                
                return Ok(new { Message = "OK" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
