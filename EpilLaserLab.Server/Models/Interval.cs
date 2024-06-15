using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Interval
    {
        public int IntervalId { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }

        [JsonIgnore]
        public Application? Application { get; set; }
    }
}
