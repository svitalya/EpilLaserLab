using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Dtos.Employees.Masters
{
    public abstract class MasterDto
    {
        public int? MasterId { get; set; }

        [Required]
        public EmployeeDto Employee { get; set; }

        public int BranchId { get; set; }
    }
}
