using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Auths
{
    public class ClientRepository(
        EpilLaserLabContext context
        ): IClientRepository
    {
        public bool Add(Client client)
        {
            context.Clients.Add( client );
            return context.SaveChanges() > 0;
        }

        public IQueryable<Client> GetQueryable()
        {
            return context.Clients.AsQueryable();
        }
    }
}
