using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Auths
{
    public interface IClientRepository
    {
        public bool Add(Client client);

        public IQueryable<Client> GetQueryable();
    }
}
