using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

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

        public User? GetById(int id)
        {
            return GetQuerable().FirstOrDefault(u => u.UserId == id);
        }

        public User? GetByLogin(string login)
        {
            return GetQuerable().FirstOrDefault(u => u.Login == login);
        }

        public IQueryable<User> GetQuerable()
        {
            return context.Users.Include(u => u.Role).AsQueryable();
        }
    }
}
