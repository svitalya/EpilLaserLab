using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Dtos.Services
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public uint TimeCost { get; set; } = 0;

        public ICollection<KeyValuePair<int, decimal>> PriceByType { get; set; } = [];
    }
}
