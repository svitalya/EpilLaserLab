namespace EpilLaserLab.Server.Dtos
{
    public class BranchCreateDto
    {
        public string Address { get; set; }

        public string PhotoFile { get; set; }
    }

    public class BranchUpdateDto
    {
        public required string Address { get; set; }

        public string? PhotoFile { get; set; }
    }
}
