using EpilLaserLab.Server.Data.Applications;
using EpilLaserLab.Server.Data.Auths;
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
        IClientRepository clientRepository,
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
        public IActionResult GetList([FromQuery] int page = 0, [FromQuery] int limit = 10, [FromQuery] string order = "dateTime", [FromQuery] string sort = "desc")
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new
            {
                Data = new
                {
                    Rec = applicationsRepository.GetQuerable()
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
                        .FirstOrDefault(a => a.ApplicationId == id),
                },
                Message = "OK"
            });
        }

        [HttpPost]
        public IActionResult Create(ApplicationCreateDto applicationCreateDto)
        {
            try
            {

                Service? service;
                ServicePrice? servicePrice;

                if (applicationCreateDto.ServiceId is not null && applicationCreateDto.TypeId is not null)
                {
                    service = serviceRepository.Get(applicationCreateDto.ServiceId.Value);

                    servicePrice = servicePricesRepository.GetQueryable()
                            .Where(sp => sp.TypeId == applicationCreateDto.TypeId
                                && sp.ServiceId == applicationCreateDto.ServiceId)
                            .OrderByDescending(sp => sp.DateTime)
                            .FirstOrDefault();
                }
                else
                {
                    servicePrice = servicePricesRepository.GetQueryable()
                        .Include(sp => sp.Service)
                        .FirstOrDefault(sp => sp.ServicePriceId == applicationCreateDto.PriceId);

                    service = servicePrice?.Service;

                }

                if(applicationCreateDto.ClientId is null && applicationCreateDto.Client is not null)
                {
                    var client = clientRepository.GetQueryable()
                        .FirstOrDefault(c => c.Phone == applicationCreateDto.Client.Phone);

                    if(client is null)
                    {
                        clientRepository.Add(new Client() { Name = applicationCreateDto.Client.Name, Phone = applicationCreateDto.Client.Phone});
                        client = clientRepository.GetQueryable()
                            .FirstOrDefault(c => c.Phone == applicationCreateDto.Client.Phone);
                    }

                    applicationCreateDto.ClientId = client!.ClientId;

                }


                if (service is null || servicePrice is null || applicationCreateDto.ClientId is null) return BadRequest();


                var timeStart = applicationCreateDto.TimeStart.GetMinuts();
                var timeEnd = service.TimeCost + timeStart;
                var interval = intervalsRepository.InsertIntervalInInterval(
                    applicationCreateDto.ScheduleId, timeStart, (int)timeEnd);

                if (interval is null) return BadRequest();

                Application application = new Application()
                {
                    ClientId = applicationCreateDto.ClientId.Value,
                    Interval = interval,
                    CategoryId = applicationCreateDto.CategoryId ?? 1,
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
        public IActionResult Update(int id, ApplicationCreateDto applicationCreateDto)
        {
            try
            {
                var oldApplication = applicationsRepository.Get(id);

                if(oldApplication is null)
                {
                    return Ok(new { Message = "NOT FOUND"});
                }

                Service? service;
                ServicePrice? servicePrice;
                if(applicationCreateDto.ServiceId is not null && applicationCreateDto.TypeId is not null)
                {
                    service = serviceRepository.Get(applicationCreateDto.ServiceId.Value);

                    servicePrice = servicePricesRepository.GetQueryable()
                            .Where(sp => sp.TypeId == applicationCreateDto.TypeId
                                && sp.ServiceId == applicationCreateDto.ServiceId)
                            .OrderByDescending(sp => sp.DateTime)
                            .FirstOrDefault();
                }
                else
                {
                    servicePrice = servicePricesRepository.GetQueryable()
                        .Include(sp => sp.Service)
                        .FirstOrDefault(sp => sp.ServicePriceId == applicationCreateDto.PriceId);

                    service = servicePrice?.Service;
                        
                }


                if (applicationCreateDto.ClientId is null && applicationCreateDto.Client is not null)
                {
                    var client = clientRepository.GetQueryable()
                        .FirstOrDefault(c => c.Phone == applicationCreateDto.Client.Phone);

                    if (client is null)
                    {
                        clientRepository.Add(new Client() { Name = applicationCreateDto.Client.Name, Phone = applicationCreateDto.Client.Phone });
                        client = clientRepository.GetQueryable()
                            .FirstOrDefault(c => c.Phone == applicationCreateDto.Client.Phone);
                    }

                    applicationCreateDto.ClientId = client!.ClientId;

                }



                if (service is null || servicePrice is null || applicationCreateDto.ClientId is null) return BadRequest();

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
                    ClientId = applicationCreateDto.ClientId.Value,
                    Interval = interval,
                    CategoryId = applicationCreateDto.CategoryId ?? 1,
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
