using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Type
    {
        public int TypeId { get; set; }

        [Required]
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<ServicePrice> ServicePrices { get; set; } = [];
    }
}
