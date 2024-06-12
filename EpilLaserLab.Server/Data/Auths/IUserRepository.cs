﻿using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Auths
{
    public interface IUserRepository
    {
        User Create(User user);
        User? GetByLogin(string login);
        User? GetById(int id);

        User? GetAuth(HttpContext context);

        IQueryable<User> GetQuerable(); 

        ICollection<User> GetAll();
    }
}
