using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Branch
    {
        public int BranchId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhotoPath {  get; set; }

        [JsonIgnore]
        public ICollection<Master> Masters { get; set; }

        [JsonIgnore]
        public ICollection<Admin> Admins { get; set; }
    }
}
