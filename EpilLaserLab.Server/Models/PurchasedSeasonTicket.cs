namespace EpilLaserLab.Server.Models
{
    public class PurchasedSeasonTicket
    {
        public int PurchasedSeasonTicketId { get; set; }

        public int SeasonTicketPriceId { get; set; }
        public SeasonTicketPrice SeasonTicketPrice { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int Remains { get; set; }

        public DateTime DateOfPurchased { get; set; }
        public DateTime DateOfCombustion {  get; set; }

        public ICollection<Application> Applications { get; set; } 
    }
}
