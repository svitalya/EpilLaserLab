using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EpilLaserLab.Server.Data.SeasonTicket
{
    public class SeasonTicketRepository(EpilLaserContext context) : ISeasonTicketRepository
    {
        private readonly EpilLaserContext _context = context;

        public bool AccessDelete(Models.SeasonTicket seasonTicket)
        {
            CollectionEntry<Models.SeasonTicket, SeasonTicketPrice> seasonTicketPrices = _context
                .Entry(seasonTicket)
                .Collection(s => s.SeasonTicketPrices);

            if (!seasonTicketPrices.IsLoaded)
            {
                seasonTicketPrices.Load();
            }

            return !seasonTicket.SeasonTicketPrices.Any();
        }

        public bool Add(Models.SeasonTicket seasonTicket)
        {
            _context.SeasonTickets.Add(seasonTicket);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Models.SeasonTicket seasonTicket)
        {
            return !_context.SeasonTickets
                .Where(s => s.ServiceId == seasonTicket.ServiceId
                        && s.Sessions == seasonTicket.Sessions
                        && s.SeasonTicketId != seasonTicket.SeasonTicketId)
                .Any();
        }

        public bool Delete(Models.SeasonTicket seasonTicket)
        {
            _context.SeasonTickets.Remove(seasonTicket);
            return _context.SaveChanges() > 0;
        }

        public Models.SeasonTicket? Get(int id)
        {
            return _context.SeasonTickets
                .Include(st => st.Service)
                .FirstOrDefault(st => st.SeasonTicketId == id);
        }

        public IEnumerable<Models.SeasonTicket> GetAll()
        {
            return _context.SeasonTickets.ToArray();
        }

        public IQueryable<Models.SeasonTicket> GetQuerable()
        {
            return _context.SeasonTickets.AsQueryable();
        }

        public bool Update(Models.SeasonTicket seasonTicketOld, Models.SeasonTicket seasonTicketNew)
        {
            seasonTicketOld.ServiceId = seasonTicketNew.ServiceId;
            seasonTicketOld.Sessions = seasonTicketNew.Sessions;
            seasonTicketOld.ValidityPeriod = seasonTicketNew.ValidityPeriod;
            return _context.SaveChanges() > 0;
        }
    }
}
