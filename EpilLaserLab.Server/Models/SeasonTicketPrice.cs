using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class SeasonTicketPrice
    {
        public int SeasonTicketPriceId { get; set; }
        public int SeasonTicketId { get; set; }

        [DataType("decimal(10,2)")]
        public decimal Price { get; set; }

        public DateTime DateTime { get; set; }

        public SeasonTicket SeasonTicket { get; set; }

        [JsonIgnore]
        public ICollection<PurchasedSeasonTicket> PurchasedSeasonTickets { get; set; }
    }
}
