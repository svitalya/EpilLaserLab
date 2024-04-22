using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Applications
{
    public class ApplicationsRepository(
        EpilLaserLabContext context) : IApplicationsRepository
    {
        public bool AccessDelete(Application application)
        {
            throw new NotImplementedException();
        }

        public bool Add(Application application)
        {
            context.Applications.Add(application);
            return context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Application application)
        {
            return true;
        }

        public bool Delete(Application application)
        {
            throw new NotImplementedException();
        }

        public Application? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Application> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Application> GetQuerable()
        {
            return context.Applications.AsQueryable();
        }

        public bool Update(Application applicationOld, Application applicationNew)
        {
            throw new NotImplementedException();
        }
    }
}
