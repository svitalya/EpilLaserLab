using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.SeasonTicket
{
    public class SeasonTicketPriceRepository(EpilLaserContext context) : ISeasonTicketPriceRepository
    {
        private readonly EpilLaserContext _context = context;

        public bool Add(SeasonTicketPrice seasonTicketPrice)
        {
            _context.SeasonTicketsPrice.Add(seasonTicketPrice);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(SeasonTicketPrice seasonTicketPrice)
        {
            return GetPrice(seasonTicketPrice.SeasonTicketId) != seasonTicketPrice.Price;
        }

        public SeasonTicketPrice? Get(int id)
        {
            return _context.SeasonTicketsPrice.FirstOrDefault(stp => stp.SeasonTicketPriceId == id);
        }

        public IEnumerable<SeasonTicketPrice> GetAll()
        {
            return _context.SeasonTicketsPrice.ToArray();
        }

        public decimal GetPrice(int seasonTicketId)
        {
            return _context.SeasonTicketsPrice
                .Where(stp => stp.SeasonTicketId == seasonTicketId)
                .OrderByDescending(stp => stp.DateTime)
                .FirstOrDefault()?.Price ?? -1; 
        }

        public IQueryable<SeasonTicketPrice> GetQueryable()
        {
            return _context.SeasonTicketsPrice.AsQueryable();
        }
    }
}
