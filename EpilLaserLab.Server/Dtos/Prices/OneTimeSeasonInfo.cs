// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Dtos.Prices;

class OneTimeSeasonInfo
{
    public int? Id { get; set; }
    public string Header { get; set; }
    public string Description { get; set; }
    public decimal? Price { get; set; }

    public uint TimeCost { get; set; }

}
