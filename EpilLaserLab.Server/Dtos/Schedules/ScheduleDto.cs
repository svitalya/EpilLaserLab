using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Dtos.Schedules
{

    public class ScheduleDto
    {
        public int ScheduleId { get; set; }

        public int MasterId { get; set; }

        public DateTime Date { get; set; }

        public string DateString => Date.ToString("dd.MM.yyyy");
        public string DateShortString => Date.ToString("dd.MM");
    }
}
