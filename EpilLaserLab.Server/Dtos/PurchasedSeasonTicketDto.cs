namespace EpilLaserLab.Server.Dtos
{
    public class PurchasedSeasonTicketDto
    {
        public int PurchasedSeasonTicketId {  get; set; }
        public string Name { get; set; } 
        public string Client {  get; set; }
        public int Remains { get; set; }
        public DateTime DateOfPurchased { get; set; }
        public DateTime DateOfCombustion { get; set; }
    }

    public class PurchasedSeasonTicketCreateDto
    {
        public int ClientId { get; set; }
        public int SeasonTicketPriceId { get; set; }
    }
}
