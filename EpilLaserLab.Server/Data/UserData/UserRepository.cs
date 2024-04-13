﻿using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Data.UserData
{
    public class UserRepository : IUserRepository
    {
        private readonly EpilLaserContext context;

        public UserRepository(EpilLaserContext context)
        {
            this.context = context;
        }

        public User Create(User user)
        {
            context.Users.Add(user);
            user.UserId = context.SaveChanges();
            return user;
        }

        public ICollection<User> GetAll()
        {
            return context.Users.Include(u => u.Role).ToArray();
        }

        public User? GetById(int id)
        {
            return context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);
        }

        public User? GetByLogin(string login)
        {
            return context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == login);
        }
    }
}