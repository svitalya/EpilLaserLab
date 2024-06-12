using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace EpilLaserLab.Server.Data.Auths
{
    public class UserRepository : IUserRepository
    {
        private readonly EpilLaserLabContext context;

        public UserRepository(EpilLaserLabContext context)
        {
            this.context = context;
        }

        public User Create(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public ICollection<User> GetAll()
        {
            return GetQuerable().ToArray();
        }

        public User? GetAuth(HttpContext httpContext)
        {
            string idStr = httpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?
                .ToString()
                .Split(':')[2]
                .Trim()
                ?? "-1";

            int userId = int.Parse(idStr);
            var authUser =  GetById(userId);

            if (authUser == null) return null;

            if (authUser.RoleId == 2 && authUser.Admin != null)
            {
                authUser.Admin.Employee = context.Employees.Include(e => e.Admin)
                    .Single(e => e.Admin == authUser.Admin);
            }

            return authUser;
        }

        public User? GetById(int id)
        {
            return GetQuerable()
                .Include(u => u.Client)
                .Include(u => u.Admin)
                .FirstOrDefault(u => u.UserId == id);
        }

        public User? GetByLogin(string login)
        {
            return GetQuerable().FirstOrDefault(u => u.Login == login);
        }

        public IQueryable<User> GetQuerable()
        {
            return context.Users
                .Include(u => u.Admin)
                    .ThenInclude(a => a.Employee)
                .Include(u => u.Admin)
                    .ThenInclude(a => a.Branch)
                .Include(u => u.Client)
                .Include(u => u.Role)
                .AsQueryable();
        }
    }
}
