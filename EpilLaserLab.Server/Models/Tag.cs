using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Models
{
    public record class Tag
    {
        public int TagId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
