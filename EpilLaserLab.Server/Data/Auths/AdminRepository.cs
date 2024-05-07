using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Data.Auths
{
    public class AdminRepository(EpilLaserLabContext context) : IAdminRepository
    {
        public bool AccessDelete(Admin admin)
        {
            return true;
        }

        public bool Add(Admin admin)
        {
            context.Admins.Add(admin);
            return context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Admin admin)
        {
            return !GetQuarable().Where(a =>
                a.AdminId != admin.AdminId
                && a.User.Login == admin.User.Login
                && a.User.Email == admin.User.Email
            ).Any();
        }

        public bool Delete(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Admin? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Admin adminOld, Admin adminNew)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Admin> GetQuarable()
        {
            return context.Admins
                .Include(a => a.User)
                .Include(a => a.Employee)
                .AsQueryable();
        }

        public Admin GetByUser(int userId)
        {
            return GetQuarable().Where(a => a.UserId == userId).Single();
        }
    }
}
