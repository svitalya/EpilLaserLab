using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public int IntervalId { get; set; }
        public Interval Interval { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ServicePriceId { get; set; }
        public ServicePrice ServicePrice { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? PurchasedSeasonTicketId { get;  set; }
        public PurchasedSeasonTicket? PurchasedSeasonTicket { get; set; }

        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeClosed { get; set; }

        public int? PrepaymentPercentage { get; set; }
    }
}
