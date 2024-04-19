using EpilLaserLab.Server.Data.Applications;
using EpilLaserLab.Server.Data.Schedules;
using EpilLaserLab.Server.Data.Services;
using EpilLaserLab.Server.Dtos;
using EpilLaserLab.Server.Dtos.ApplicationCreate;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController(
        IApplicationsRepository applicationsRepository,
        IServiceRepository serviceRepository,
        IServicePricesRepository servicePricesRepository,
        IIntervalsRepository intervalsRepository,
        ISchedulesRepository schedulesRepository): ControllerBase
    {

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

                int getMinuts(string time)
                {
                    string[] timeData = time.Split(":");

                    int hours = int.Parse(timeData[0]);
                    int minuts = int.Parse(timeData[1]);

                    return hours * 60 + minuts;
                }

                var timeStart = getMinuts(applicationCreateDto.TimeStart);
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
            throw new Exception();
        }
    }
}
