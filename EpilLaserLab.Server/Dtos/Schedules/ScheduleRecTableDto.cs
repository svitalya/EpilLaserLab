using EpilLaserLab.Server.Dtos.Employees.Masters;

namespace EpilLaserLab.Server.Dtos.Schedules
{
    public class ScheduleRecTableDto : ScheduleDto
    {
        public MasterRecTableDto Master { get; set; }
    }
}
