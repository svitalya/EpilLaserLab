using EpilLaserLab.Server.Models;


namespace EpilLaserLab.Server.Data.SeasonTicket
{
    public interface ISeasonTicketRepository
    {
        public IEnumerable<Models.SeasonTicket> GetAll();
        public Models.SeasonTicket? Get(int id);
        public bool Add(
            Models.SeasonTicket seasonTicket);
        public bool Update(
            Models.SeasonTicket seasonTicketOld,
            Models.SeasonTicket seasonTicketNew);
        public bool Delete(
            Models.SeasonTicket seasonTicket);
        public bool CheckForDuplication(
            Models.SeasonTicket seasonTicket);
        public bool AccessDelete(
            Models.SeasonTicket seasonTicket);
        public IQueryable<Models.SeasonTicket> GetQuerable();
    }
}
