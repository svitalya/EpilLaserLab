﻿namespace EpilLaserLab.Server.Models
{
    public class Interval
    {
        public int IntervalId { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }

        public Application? Application { get; set; }
    }
}
