using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Auths
{
    public interface IAdminRepository
    {
        public bool Add(Admin admin);
    }
}
