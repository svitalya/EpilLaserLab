using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public string FIO => ($"{Surname} " +
            $"{(Name.Length > 0 ? Name[..1].ToUpper()+'.' : "")} " +
            $"{(Patronymic is not null && Patronymic.Length > 0 ? Patronymic[..1].ToUpper() + '.' : "")}").Trim();

        public bool IsWork { get; set; } = true;

        public Master? Master { get; set; }
    }
}
