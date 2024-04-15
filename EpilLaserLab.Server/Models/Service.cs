using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public uint TimeCost { get; set; } = 0;

        [JsonIgnore]
        public ICollection<ServicePrice> ServicePrices { get; set; } = [];
        [JsonIgnore]
        public ICollection<SeasonTicket> SeasonTickets { get; set; } = [];
    }
}
