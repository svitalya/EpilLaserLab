using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User? GetByLogin(string login);
        User? GetById(int id);

        ICollection<User> GetAll();
    }
}
