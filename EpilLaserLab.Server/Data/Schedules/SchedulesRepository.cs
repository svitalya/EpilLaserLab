using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EpilLaserLab.Server.Data.Schedules
{
    public class SchedulesRepository(
        EpilLaserLabContext context
        ) : ISchedulesRepository
    {
        public bool AccessDelete(Schedule schedule)
        {
            CollectionEntry<Schedule, Interval> intervals = context
                .Entry(schedule)
                .Collection(i => i.Intervals);

            if(!intervals.IsLoaded ) {
                intervals.Load();
            }

            return !schedule.Intervals.Any();
        }

        public bool Add(Schedule schedule)
        {
            context.Schedules.Add(schedule);
            return context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Schedule schedule)
        {
            return !GetQueryable()
                .Where(s => s.MasterId == schedule.MasterId
                    && s.Date == schedule.Date
                    && s.ScheduleId != schedule.ScheduleId)
                .Any();
        }

        public bool Delete(Schedule schedule)
        {
            context.Schedules.Remove(schedule);
            return context.SaveChanges() > 0;
        }

        public Schedule? Get(int id)
        {
            return GetQueryable().FirstOrDefault(s => s.ScheduleId == id);
        }

        public IEnumerable<Schedule> GetAll()
        {
            return GetQueryable().ToArray();
        }

        public IQueryable<Schedule> GetQueryable()
        {
            return context.Schedules.Include(s => s.Master)
                .ThenInclude(m => m.Employee)
                .AsQueryable();
        }

        public bool Update(Schedule scheduleOld, Schedule scheduleNew)
        {
            scheduleOld.MasterId = scheduleNew.MasterId;
            scheduleOld.Date = scheduleNew.Date;
            return context.SaveChanges() > 0;
        }
    }
}
