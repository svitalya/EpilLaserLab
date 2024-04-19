using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.SeasonTicket
{
    public interface IPurchasedSeasonTicketsRepository
    {
        public IEnumerable<PurchasedSeasonTicket> GetAll();
        public PurchasedSeasonTicket? Get(int id);
        public bool Add(PurchasedSeasonTicket purchasedSeasonTicket);
        public bool Update(PurchasedSeasonTicket purchasedSeasonTicketOld, PurchasedSeasonTicket purchasedSeasonTicketNew);
        public bool Delete(PurchasedSeasonTicket purchasedSeasonTicket);
        public bool CheckForDuplication(PurchasedSeasonTicket purchasedSeasonTicket);
        public bool AccessDelete(PurchasedSeasonTicket purchasedSeasonTicket);
        public IQueryable<PurchasedSeasonTicket> GetQueryable();
    }
}
