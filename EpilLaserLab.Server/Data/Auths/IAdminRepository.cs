using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Auths
{
    public interface IAdminRepository
    {
        public IEnumerable<Admin> GetAll();
        public Admin? Get(int id);
        public bool Add(Admin admin);
        public bool Update(Admin adminOld, Admin adminNew);
        public bool Delete(Admin admin);
        public bool CheckForDuplication(Admin admin);
        public bool AccessDelete(Admin admin);

        public IQueryable<Admin> GetQuarable();

        public Admin GetByUser(int userId);
    }
}
