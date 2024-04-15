using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.SeasonTicket
{
    public interface ISeasonTicketPriceRepository
    {
        public IEnumerable<SeasonTicketPrice> GetAll();
        public SeasonTicketPrice? Get(int id);
        public bool Add(SeasonTicketPrice seasonTicketPrice);
        public bool CheckForDuplication(SeasonTicketPrice seasonTicketPrice);
        public IQueryable<SeasonTicketPrice> GetQueryable();
        public decimal GetPrice(int seasonTicketId);
    }
}
