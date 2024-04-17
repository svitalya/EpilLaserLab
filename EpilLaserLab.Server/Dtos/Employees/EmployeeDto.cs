namespace EpilLaserLab.Server.Dtos.Employees
{
    public class EmployeeDto
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public bool IsWork { get; set; } = true;
    }
}
