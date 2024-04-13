using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.References
{
    public interface IStatusRepository
    {
        public IEnumerable<Status> GetAll();
        public Status? Get(int id);
        public bool Add(Status status);
        public bool Update(int id, Status status);
        public bool Delete(int id);
    }
}
