using EpilLaserLab.Server.Data.SeasonTicket;
using EpilLaserLab.Server.Dtos;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedSeasonTicketsController(
        IPurchasedSeasonTicketsRepository purchasedSeasonTicketsRepository,
        ISeasonTicketPriceRepository seasonTicketPriceRepository) : ControllerBase
    {

        object? orderById(PurchasedSeasonTicketDto pstd) => pstd.PurchasedSeasonTicketId;
        object? orderByName(PurchasedSeasonTicketDto pstd) => pstd.Name;
        object? orderByClient(PurchasedSeasonTicketDto pstd) => pstd.Client;
        object? orderByRemains(PurchasedSeasonTicketDto pstd) => pstd.Remains;
        object? orderByDateOfPurchased(PurchasedSeasonTicketDto pstd) => pstd.DateOfPurchased;
        object? orderByDateOfCombustion(PurchasedSeasonTicketDto pstd) => pstd.DateOfCombustion;

        [HttpGet]
        public IActionResult GetList(int page = 0, int limit = 10, string order = "purchasedSeasonTicketId", string sort = "asc")
        {
            Dictionary<string, Func<PurchasedSeasonTicketDto, object?>> functor = [];
            functor.Add("purchasedSeasonTicketId", orderById);
            functor.Add("name", orderByName);
            functor.Add("client", orderByClient);
            functor.Add("remains", orderByRemains);
            functor.Add("dateOfPurchased", orderByDateOfPurchased);
            functor.Add("dateOfCombustion", orderByDateOfCombustion);

            var querable = purchasedSeasonTicketsRepository.GetQueryable()
                .Select(pst => new PurchasedSeasonTicketDto
                {
                    PurchasedSeasonTicketId = pst.PurchasedSeasonTicketId,
                    Name = $"{pst.SeasonTicketPrice.SeasonTicket.Service.Name}:{pst.SeasonTicketPrice.SeasonTicket.Sessions}",
                    Client = $"{pst.Client.Name}",
                    Remains = pst.Remains,
                    DateOfPurchased = pst.DateOfPurchased,
                    DateOfCombustion = pst.DateOfCombustion,
                })
                .AsQueryable();

            if (functor.TryGetValue(order, out Func<PurchasedSeasonTicketDto, object?>? f) && f is not null)
            {
                querable = (sort == "desc"
                    ? querable.OrderByDescending(f)
                    : querable.OrderBy(f)
                ).AsQueryable();
            }

            int maxRecs = querable.Count();

            if (page + 1 * limit > maxRecs)
            {
                page = 0;
            }

            var recs = querable.Skip(page * limit)
                .Take(limit)
                .ToArray();

            return Ok(new
            {
                Data = new
                {
                    Recs = recs,
                    Page = page,
                    Max = maxRecs
                },
                Message = "OK"
            });
        }

        [HttpPost]
        public IActionResult Create(PurchasedSeasonTicketCreateDto dto)
        {
            try
            {
                var seasonTicketPrice = seasonTicketPriceRepository.GetQueryable()
                    .Include(stp => stp.SeasonTicket)
                    .Single(stp => stp.SeasonTicketPriceId == dto.SeasonTicketPriceId);


                PurchasedSeasonTicket purchasedSeasonTicket = new PurchasedSeasonTicket()
                {
                    SeasonTicketPriceId = dto.SeasonTicketPriceId,
                    ClientId = dto.ClientId,
                    Remains = seasonTicketPrice.SeasonTicket.Sessions,
                    DateOfPurchased = Utils.NowDate(),
                    DateOfCombustion = Utils.NowDate().AddDays(seasonTicketPrice.SeasonTicket.ValidityPeriod+1),

                };


                if (!(purchasedSeasonTicketsRepository.CheckForDuplication(purchasedSeasonTicket)
                    && purchasedSeasonTicketsRepository.Add(purchasedSeasonTicket)))
                {

                    return Ok(new { Message = "DUPLICATION" });
                }

                return Ok(new { Message = "OK" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
