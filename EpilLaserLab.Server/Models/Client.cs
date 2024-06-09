using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }

        [JsonIgnore]
        public ICollection<PurchasedSeasonTicket> PurchasedSeasonTickets { get; set; }
    }
}
