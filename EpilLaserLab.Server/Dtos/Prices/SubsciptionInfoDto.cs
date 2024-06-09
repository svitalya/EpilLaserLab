// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Dtos.Prices;

class SubsciptionInfoDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<SubsciptionPriceDto> Prices { get; set; }
}
