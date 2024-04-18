namespace EpilLaserLab.Server.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public int MasterId { get; set; }
        public Master Master { get; set; }

        public DateTime Date {  get; set; }

        public ICollection<Interval> Intervals { get; set; }
    }
}
