using EpilLaserLab.Server.Data.SeasonTicket;
using EpilLaserLab.Server.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers
{
    public class SubsciptionPriceDto
    {
        public int ColSeansons { get; set; }
        public decimal Price { get; set; }

    }

    class SubsciptionInfoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<SubsciptionPriceDto> Prices { get; set; }
    }

    class OneTimeSeasonInfo
    {
        public int? Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

        public uint TimeCost { get; set; }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class PricesController(
        ISeasonTicketRepository seasonTicketRepository,
        IServiceRepository serviceRepository,
        ISeasonTicketPriceRepository seasonTicketPriceRepository) : ControllerBase
    {
        // GET api/<PricesController>/5
        [HttpGet("{priceList}")]
        public IActionResult Get(string priceList)
        {
            if(priceList == "subsciptions")
            {
                var queryable = seasonTicketRepository.GetQuerable();

                var recs = queryable.Include(st => st.Service)
                    .Include(st => st.SeasonTicketPrices)
                    .ToArray()
                    .GroupBy(st => st.Service)
                    .Select(g => new SubsciptionInfoDto
                    {
                        Name = g.Key.Name,
                        Description = g.Key.Description ?? string.Empty,
                        Prices = g.Select(gv => new SubsciptionPriceDto
                        {
                            ColSeansons = gv.Sessions,
                            Price = seasonTicketPriceRepository.GetPrice(gv.SeasonTicketId)
                        })
                        .OrderBy(p => p.ColSeansons)
                        .ToArray()

                    })
                    .ToArray();


                return Ok(new
                {
                    Message = "OK",
                    Recs = recs
                });
            }
            else if(priceList == "acquaintance" || priceList == "standard")
            {
                return Ok(new
                {
                    Message = "OK",
                    Recs = serviceRepository.GetAll()
                        .Select( (sr) => {
                            var price = serviceRepository.GetPriceByType(sr, priceList == "acquaintance" ? 1 : 4);

                            return new OneTimeSeasonInfo {
                                Id = price?.ServicePriceId ?? null,
                                TimeCost = sr.TimeCost,
                                Header = sr.Name,
                                Description = sr.Description ?? string.Empty,
                                Price = price?.Price ?? null };
                        })
                        .Where(r => r.Id is not null)
                        .ToArray()
                });
            }


            else
            {
                return BadRequest();
            }
        }
    }
}
