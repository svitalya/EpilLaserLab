using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Auths
{
    public interface IClientRepository
    {
        public bool Add(Client client);

        public bool Update();

        public IQueryable<Client> GetQueryable();
    }
}
