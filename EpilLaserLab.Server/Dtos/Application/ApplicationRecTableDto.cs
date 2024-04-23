namespace EpilLaserLab.Server.Dtos.Application
{
    public class ApplicationRecTableDto
    {
        public int ApplicationId { get; set; }

        public string Branch {  get; set; }
        public string Master { get; set; }
        public string Client { get; set; }

        // день и время вместе
        public string DateTime { get; set; }

        public string Service { get; set; }
        public string Price { get; set; }
    }
}
