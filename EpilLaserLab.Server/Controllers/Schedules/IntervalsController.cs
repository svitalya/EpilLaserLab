using EpilLaserLab.Server.Data.Schedules;
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
        [HttpPost("{id}")]
        public IActionResult SetIntervalsInSchedule(int id, [FromBody] string intervalsStr)
        {
            try
            {
                Schedule? schedult = schedulesRepository.Get(id);

                if(schedult is null) {
                    return Ok("NOT FOUND");
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

                    if (intervalsRepository.DeleteRangeInSchedule(id)){
                        intervalsRepository.SetRengeInSchedule(id, intervals);
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
        public IActionResult GetIntervalsString(int id)
        {
            try
            {
                Schedule? schedult = schedulesRepository.Get(id);

                if (schedult is null)
                {
                    return Ok("NOT FOUND");
                }

                StringBuilder stringBuilder = new StringBuilder();
                var intervals = intervalsRepository.GetIntervalsInSchedule(schedult.ScheduleId);

                foreach(var interval in intervals)
                {
                    string minutsToTimeStr(int minuts) => $"{(minuts / 60).ToString().PadLeft(2, '0')}" +
                        $":{(minuts % 60).ToString().PadLeft(2, '0')}";

                    stringBuilder.Append(minutsToTimeStr(interval.TimeStart));
                    stringBuilder.Append('-');
                    stringBuilder.Append(minutsToTimeStr(interval.TimeEnd));
                    stringBuilder.Append(';');
                }

                return Ok(new { Message = "OK", IntervalsString=stringBuilder.ToString()});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
