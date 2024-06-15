using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Master
    {
        public int MasterId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        public Branch Branch { get; set; }

        public Employee Employee { get; set; }

        [JsonIgnore]
        public ICollection<Schedule> Schedules { get; set; }

    }
}
