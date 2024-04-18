using EpilLaserLab.Server.Dtos.Employees;

namespace EpilLaserLab.Server.Dtos.Auths
{
    public class RegisterAdminDto : RegisterDto
    {
        public int BranchId { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public override int RoleId => 2;
    }
}
 