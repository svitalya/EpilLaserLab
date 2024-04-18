using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Schedules
{
    public class IntervalsRepository(
        EpilLaserLabContext context
        ) : IIntervalsRepository
    {
        public bool AccessDeleteRange(int[] intervalIds)
        {
            return true;
        }

        public bool DeleteRangeInSchedule(int scheduleId)
        {
            var intervals = GetIntervalsInSchedule(scheduleId);
            var intervalIds = intervals.Select(i => i.IntervalId).ToArray();

            if(AccessDeleteRange(intervalIds))
            {
                context.RemoveRange(intervals);
                return context.SaveChanges() > 0 || intervalIds.Length == 0;
            }

            return false;
        }

        public ICollection<Interval> GetIntervalsInSchedule(int scheduleId)
        {
            return GetQurable().Where(i => i.ScheduleId == scheduleId).ToArray();
        }

        public IQueryable<Interval> GetQurable()
        {
            return context.Intervals.AsQueryable();
        }

        public bool SetRengeInSchedule(int scheduleId, ICollection<Interval> intervals)
        {
            context.AddRange(intervals);
            return context.SaveChanges() > 0;
        }
    }
}
