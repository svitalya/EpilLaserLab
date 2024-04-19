using EpilLaserLab.Server.Data.Schedules;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EpilLaserLab.Server.Controllers.Schedules
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntervalsController(
        IIntervalsRepository intervalsRepository,
        ISchedulesRepository schedulesRepository
        ): ControllerBase
    {

        public DateTime Now { get
            {
                DateOnly dateo = DateTime.Now.ToDateOnly();
                DateTime now = dateo.ToDateTime(new TimeOnly(0, 0, 0));
                return now;
            } }

        [HttpPost("{id}")]
        public IActionResult SetIntervalsInSchedule(int id, [FromBody] string intervalsStr)
        {
            try
            {
                Schedule? schedult = schedulesRepository.Get(id);

                if (schedult is null || schedult.Date < Now) {
                    return Ok(new { Message = "NOT FOUND" });
                }

                try
                {
                    var intervalsStrData = intervalsStr.Trim(';').Split(';');

                    List<Interval> intervals = new List<Interval>(intervalsStrData.Length);

                    foreach (string intervalStr in intervalsStrData)
                    {
                        int getMinuts(string time)
                        {
                            string[] timeData = time.Split(":");

                            int hours = int.Parse(timeData[0]);
                            int minuts = int.Parse(timeData[1]);

                            return hours * 60 + minuts;
                        }

                        var intervalData = intervalStr.Split('-');

                        var timeStart = getMinuts(intervalData[0]);
                        var timeEnd = getMinuts(intervalData[1]);

                        if (timeStart >= timeEnd) throw new Exception();

                        intervals.Add(new Interval
                        {
                            Schedule = schedult,
                            TimeStart = timeStart,
                            TimeEnd = timeEnd,
                        });
                    }

                    if (intervalsRepository.AccessSet(id, intervals)
                        && intervalsRepository.DeleteRangeInSchedule(id) 
                        && intervalsRepository.SetRengeInSchedule(id, intervals)){
                        
                        return Ok(new { Message = "OK" });
                    }

                    return Ok("BLOCK");
                }catch (Exception ex)
                {
                    return Ok("DATA NOT VALID");
                }

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetIntervalsString(int id, int? timeCost = null)
        {
            try
            {
                Schedule? schedult = schedulesRepository.Get(id);

                if (schedult is null ||  schedult.Date <= Now)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                StringBuilder IntervalsStringBuilder = new StringBuilder();
                StringBuilder WorkIntervalsStringBuilder = new StringBuilder();
                var intervals = intervalsRepository.GetIntervalsInSchedule(schedult.ScheduleId);

                if(timeCost is not null)
                {
                    intervals = intervals.Where(i => (i.TimeStart + timeCost) <= i.TimeEnd).ToList();
                }
                

                foreach (var interval in intervals)
                {
                    string minutsToTimeStr(int minuts) => $"{(minuts / 60).ToString().PadLeft(2, '0')}" +
                        $":{(minuts % 60).ToString().PadLeft(2, '0')}";

                    StringBuilder stringBuilder = interval.Application is null ?
                        IntervalsStringBuilder : WorkIntervalsStringBuilder;

                    stringBuilder.Append(minutsToTimeStr(interval.TimeStart));
                    stringBuilder.Append('-');
                    stringBuilder.Append(minutsToTimeStr(interval.TimeEnd - (timeCost ?? 0)));
                    stringBuilder.Append(';');
                }

                return Ok(new {
                    Message = "OK",
                    IntervalsString= IntervalsStringBuilder.ToString(),
                    WorkIntervals = WorkIntervalsStringBuilder.ToString()});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
