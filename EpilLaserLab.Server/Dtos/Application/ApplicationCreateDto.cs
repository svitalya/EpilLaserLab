namespace EpilLaserLab.Server.Dtos.Application
{
    public class CLientInApplicationDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class ApplicationCreateDto
    {
        public int? ClientId { get; set; }

        public CLientInApplicationDto Client { get; set; }


        public int? ServiceId { get; set; }

        public int? TypeId { get; set; }

        public int? CategoryId { get; set; }

        public int? PriceId { get; set; }

        public int BranchId { get; set; } //234

        public int MasterId { get; set; } //234

        public int ScheduleId { get; set; } //234

        public string TimeStart { get; set; } // 234
    }
}
