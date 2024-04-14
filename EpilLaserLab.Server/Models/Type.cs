using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Models
{
    public class Type
    {
        public int TypeId { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
