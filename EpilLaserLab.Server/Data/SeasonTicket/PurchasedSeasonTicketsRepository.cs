using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Data.SeasonTicket
{
    public class PurchasedSeasonTicketsRepository(
        EpilLaserLabContext context): IPurchasedSeasonTicketsRepository
    {
        public bool AccessDelete(PurchasedSeasonTicket purchasedSeasonTicket)
        {
            return false;
        }

        public bool Add(PurchasedSeasonTicket purchasedSeasonTicket)
        {
            context.PurchasedSeasonTickets.Add(purchasedSeasonTicket);
            return context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(PurchasedSeasonTicket purchasedSeasonTicket)
        {
            return !GetQueryable().Where(pst =>
                purchasedSeasonTicket.PurchasedSeasonTicketId != pst.PurchasedSeasonTicketId
                && pst.SeasonTicketPriceId == purchasedSeasonTicket.SeasonTicketPriceId
                && pst.ClientId == purchasedSeasonTicket.ClientId
                && pst.Remains > 0
                && pst.DateOfCombustion > DateTime.Now)
                .Any();
        }

        public bool Delete(PurchasedSeasonTicket purchasedSeasonTicket)
        {
            throw new NotImplementedException();
        }

        public PurchasedSeasonTicket? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PurchasedSeasonTicket> GetAll()
        {
            return GetQueryable().ToArray();
        }

        public IQueryable<PurchasedSeasonTicket> GetQueryable()
        {
            return context.PurchasedSeasonTickets
                .Include(pst => pst.Client)
                .Include(pst => pst.SeasonTicketPrice)
                    .ThenInclude(stp => stp.SeasonTicket)
                        .ThenInclude(st => st.Service)
                .AsQueryable();
        }

        public bool Update(PurchasedSeasonTicket purchasedSeasonTicketOld, PurchasedSeasonTicket purchasedSeasonTicketNew)
        {
            throw new NotImplementedException();
        }
    }
}
