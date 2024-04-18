using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Schedules
{
    public interface ISchedulesRepository
    {
        public IEnumerable<Schedule> GetAll();
        public Schedule? Get(int id);
        public bool Add(Schedule schedule);
        public bool Update(Schedule scheduleOld, Schedule scheduleNew);
        public bool Delete(Schedule schedule);
        public bool CheckForDuplication(Schedule schedule);
        public bool AccessDelete(Schedule schedule);
        public IQueryable<Schedule> GetQueryable();
    }
}
