using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Auths
{
    public class AdminRepository(EpilLaserLabContext context) : IAdminRepository
    {
        public bool Add(Admin admin)
        {
            context.Admins.Add(admin);
            return context.SaveChanges() > 0;
        }
    }
}
