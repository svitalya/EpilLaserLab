using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class SeasonTicket
    {
        public int SeasonTicketId {  get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int Sessions { get; set; }

        [Required]
        public int ValidityPeriod { get; set; }

        public Service Service { get; set; }

        [JsonIgnore]
        public ICollection<SeasonTicketPrice> SeasonTicketPrices { get; set; }
    }
}
