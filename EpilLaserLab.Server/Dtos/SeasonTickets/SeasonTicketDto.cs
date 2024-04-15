using EpilLaserLab.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace EpilLaserLab.Server.Dtos.SeasonTickets
{
    public class SeasonTicketDto
    {
        public int SeasonTicketId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public Service? Service { get; set; }

        [Required]
        public int Sessions { get; set; }

        [Required]
        public int ValidityPeriod { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
