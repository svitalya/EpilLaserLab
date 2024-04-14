using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
