using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public uint TimeCost { get; set; } = 0;

        public ICollection<ServicePrice> ServicePrices { get; set; } = [];
    }
}
