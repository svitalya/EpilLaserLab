using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Employees
{
    public interface IMasterRepository
    {
        public IEnumerable<Master> GetAll();
        public Master? Get(int id);
        public bool Add(Master master);
        public bool Update(Master masterOld, Master masterNew);
        public bool Delete(Master master);
        public bool CheckForDuplication(Master master);
        public bool AccessDelete(Master master);
        public IQueryable<Master> GetQueryable();
    }
}
