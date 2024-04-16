using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Surname {  get; set; }

        [Required]
        public string Name { get; set; }

        public string? Patronymic { get; set; }

        public bool IsWork { get; set; } = true;


    }
}
