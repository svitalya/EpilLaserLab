using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Models
{
    public class ServicePrice
    {
        public int ServicePriceId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [DataType("decimal (10,2)")]
        [Required]
        public decimal Price { get; set; }

        public virtual Service Service { get; set; }
        public virtual Type Type { get; set; }


    }
}
