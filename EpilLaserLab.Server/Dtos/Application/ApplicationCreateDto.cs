namespace EpilLaserLab.Server.Dtos.Application
{
    public class ApplicationCreateDto
    {
        public int ClientId { get; set; }
        public int ServiceId { get; set; }

        public int TypeId { get; set; }

        public int CategoryId { get; set; }

        public int BranchId { get; set; }

        public int MasterId { get; set; }

        public int ScheduleId { get; set; }

        public string TimeStart { get; set; }
    }
}
