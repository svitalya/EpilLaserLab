using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EpilLaserLab.Server.Data.SeasonTicket
{
    public class PurchasedSeasonTicketsRepository(
        EpilLaserLabContext context): IPurchasedSeasonTicketsRepository
    {
        public bool AccessDelete(PurchasedSeasonTicket purchasedSeasonTicket)
        {
            CollectionEntry<PurchasedSeasonTicket, Models.Application> applications = context.Entry(purchasedSeasonTicket)
                .Collection(p => p.Applications);

            if (!applications.IsLoaded) applications.Load();

            return !purchasedSeasonTicket.Applications.Any();
        }

        public bool Add(PurchasedSeasonTicket purchasedSeasonTicket)
        {
            context.PurchasedSeasonTickets.Add(purchasedSeasonTicket);
            return context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(PurchasedSeasonTicket purchasedSeasonTicket)
        {
            var seasonTicketPrice = context.SeasonTicketsPrice
                .Include(stp => stp.SeasonTicket)
                .FirstOrDefault(stp => stp.SeasonTicketPriceId == purchasedSeasonTicket.SeasonTicketPriceId);

            if (seasonTicketPrice is null) return false;

            return !GetQueryable().Where(pst =>
                purchasedSeasonTicket.PurchasedSeasonTicketId != pst.PurchasedSeasonTicketId
                && pst.SeasonTicketPrice.SeasonTicket.ServiceId == seasonTicketPrice.SeasonTicket.ServiceId
                && pst.ClientId == purchasedSeasonTicket.ClientId
                && pst.Remains > 0
                && pst.DateOfCombustion > DateTime.Now)
                .Any();
        }

        public bool Delete(PurchasedSeasonTicket purchasedSeasonTicket)
        {
            context.PurchasedSeasonTickets.Remove(purchasedSeasonTicket);
            return context.SaveChanges() > 0;
        }

        public PurchasedSeasonTicket? Get(int id)
        {
            return GetQueryable().FirstOrDefault(p => p.PurchasedSeasonTicketId == id);
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
            purchasedSeasonTicketOld.SeasonTicketPriceId = purchasedSeasonTicketNew.SeasonTicketPriceId;
            purchasedSeasonTicketOld.ClientId = purchasedSeasonTicketNew.ClientId;
            purchasedSeasonTicketOld.Remains = purchasedSeasonTicketNew.Remains;
            purchasedSeasonTicketOld.DateOfPurchased = purchasedSeasonTicketNew.DateOfPurchased;
            purchasedSeasonTicketOld.DateOfCombustion = purchasedSeasonTicketNew.DateOfCombustion;
            return context.SaveChanges() > 0;
        }
    }
}
