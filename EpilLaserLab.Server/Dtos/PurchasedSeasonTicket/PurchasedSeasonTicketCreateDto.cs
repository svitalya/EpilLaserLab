namespace EpilLaserLab.Server.Dtos.PurchasedSeasonTicket
{
    public class PurchasedSeasonTicketCreateDto
    {
        public int? ClientId { get; set; }

        public CLientInDto? Client { get; set; }

        public int SeasonTicketPriceId { get; set; }
    }
}
