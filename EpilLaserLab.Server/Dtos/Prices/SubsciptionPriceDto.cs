using EpilLaserLab.Server.Models;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Dtos.Prices;

public class SubsciptionPriceDto
{
    public int ColSeansons { get; set; }
    public decimal Price { get; set; }

}
