namespace EpilLaserLab.Server.Dtos.Application
{
    public class ApplicationCreateDto
    {
        public int? ClientId { get; set; }

        public CLientInDto? Client { get; set; }


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
