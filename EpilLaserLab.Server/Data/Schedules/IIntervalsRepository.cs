using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Schedules
{
    public interface IIntervalsRepository
    {
        public bool DeleteRangeInSchedule(int scheduleId);
        public bool SetRengeInSchedule(int scheduleId, ICollection<Interval> intervals);
        public ICollection<Interval> GetIntervalsInSchedule(int scheduleId);
        public IQueryable<Interval> GetQurable();
        bool AccessSet(int scheduleId, List<Interval> intervals);

        public Interval? InsertIntervalInInterval(int scheduleId, int timeStart, int timeEnd);
    }
}
