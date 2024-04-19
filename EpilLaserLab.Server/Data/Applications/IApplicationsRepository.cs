using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Applications
{
    public interface IApplicationsRepository
    {
        public IEnumerable<Application> GetAll();
        public Application? Get(int id);
        public bool Add(Application application);
        public bool Update(Application applicationOld, Application applicationNew);
        public bool Delete(Application application);
        public bool CheckForDuplication(Application application);
        public bool AccessDelete(Application application);
        public IQueryable<Application> GetQuerable();
    }
}
