using EpilLaserLab.Server.Dtos.References;
using EpilLaserLab.Server.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpilLaserLab.Server.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public int Id { get => StatusId; set => StatusId = value; }
    }
}
