using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Models
{
    public class Branch
    {
        public int BranchId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhotoPath {  get; set; }

        public ICollection<Master> Masters { get; set; }
    }
}
