using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Data.Schedules
{
    public class IntervalsRepository(
        EpilLaserLabContext context
        ) : IIntervalsRepository
    {

        public bool AccessSet(int scheduleId, List<Interval> intervals)
        {

            intervals = intervals.OrderBy(i => i.TimeStart).ToList();

            int timeStart = 0;

            foreach(var interval in intervals)
            {
                if(timeStart == interval.TimeEnd)
                {
                    return false;
                }

                timeStart = interval.TimeEnd;
            }

            var intervalsWithApplication = GetIntervalsInScheduleQurable(scheduleId)
                .Where(i => i.Application != null)
                .ToArray();

            foreach ( var interval in intervalsWithApplication) {
                var isAny = intervals.Where(i =>
                    (i.TimeStart >= interval.TimeStart && i.TimeStart <= interval.TimeEnd)
                    || (i.TimeEnd >= interval.TimeStart && i.TimeEnd <= interval.TimeEnd)).Any();

                if(isAny) return false;
            }

            return true;
        }

        public bool Delete(Interval interval)
        {
            context.Remove(interval);
            return context.SaveChanges() > 0;
        }

        public bool DeleteRangeInSchedule(int scheduleId)
        {
            var intervals = GetIntervalsInScheduleQurable(scheduleId)
                .Where(i => i.Application == null)
                .ToArray();

            context.RemoveRange(intervals);

            return intervals.Length==0 || context.SaveChanges() > 0;
        }

        public ICollection<Interval> GetIntervalsInSchedule(int scheduleId)
        {
            return GetIntervalsInScheduleQurable(scheduleId).ToArray();
        }

        public IQueryable<Interval> GetIntervalsInScheduleQurable(int scheduleId)
        {
            return GetQurable().OrderBy(i => i.TimeStart).Where(i => i.ScheduleId == scheduleId);
        }

        public IQueryable<Interval> GetQurable()
        {
            return context.Intervals.Include(i => i.Application).AsQueryable();
        }

        public Interval? InsertIntervalInInterval(int scheduleId, int timeStart, int timeEnd)
        {
            if(timeStart >= timeEnd) return null;

            var interval = GetIntervalsInScheduleQurable(scheduleId).Where(i =>
                (timeStart >= i.TimeStart && timeStart <= i.TimeEnd)
                && (timeEnd >= i.TimeStart && timeEnd <= i.TimeEnd)
                && i.Application == null).Single();

            if (interval is null) return null;

            if(timeStart > interval.TimeStart)
            {
                context.Intervals.Add(new Interval()
                {
                    TimeStart = interval.TimeStart,
                    TimeEnd = timeStart,
                    ScheduleId = scheduleId,
                });            
            }

            if(timeEnd < interval.TimeEnd)
            {
                context.Intervals.Add(new Interval()
                {
                    TimeStart = timeEnd,
                    TimeEnd = interval.TimeEnd,
                    ScheduleId = scheduleId,
                });
            }

            Interval newInterval = new() { TimeEnd = timeEnd, ScheduleId = scheduleId, TimeStart = timeStart };

            context.Remove(interval);
            context.Add(newInterval);

            context.SaveChanges();

            return newInterval;
        }

        public bool SetRengeInSchedule(int scheduleId, ICollection<Interval> intervals)
        {
            context.AddRange(intervals);
            return context.SaveChanges() > 0;
        }
    }
}
