using EpilLaserLab.Server.Data.SeasonTicket;
using EpilLaserLab.Server.Dtos.SeasonTickets;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;

namespace EpilLaserLab.Server.Controllers.SeasonTickets
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class SeasonTicketsController(
        ISeasonTicketPriceRepository seasonTicketPriceRepository,
        ISeasonTicketRepository seasonTicketRepository
        ): ControllerBase
    {
        private readonly ISeasonTicketPriceRepository 
            _seasonTicketPriceRepository = seasonTicketPriceRepository;
        private readonly ISeasonTicketRepository 
            _seasonTicketRepository = seasonTicketRepository;

        static object? orderById(SeasonTicketDto s) => s.SeasonTicketId;
        static object? orderByService(SeasonTicketDto s) => s.Service?.Name;
        static object? orderBySessions(SeasonTicketDto s) => s.Sessions;
        static object? orderByValidityPeriod(SeasonTicketDto s) => s.ValidityPeriod;

        [HttpGet]
        public IActionResult GetList(int page = 0, int limit = 10, string order = "id", string sort = "asc")
        {
            Dictionary<string, Func<SeasonTicketDto, object?>> functor = [];
            functor.Add("id", orderById);
            functor.Add("service", orderByService);
            functor.Add("sessions", orderBySessions);
            functor.Add("validityPeriod", orderByValidityPeriod);
            functor.Add("price", orderByValidityPeriod);

            var querable = _seasonTicketRepository.GetQuerable()
                .Include(st => st.Service)
                .ToArray()
                .Select(s => new SeasonTicketDto
                {
                    SeasonTicketId = s.SeasonTicketId,
                    Service = s.Service,
                    ServiceId = s.ServiceId,
                    Sessions = s.Sessions,
                    ValidityPeriod = s.ValidityPeriod,
                    Price = _seasonTicketPriceRepository.GetPrice(s.SeasonTicketId),
                })
                .AsQueryable();

            if (functor.TryGetValue(order, out Func<SeasonTicketDto, object?>? f) && f is not null)
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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var s = _seasonTicketRepository.Get(id);

            if (s is null)
            {
                return Ok(new { Message = "NOT FOUND" });
            };

            return Ok(new
            {
                Rec = new SeasonTicketDto
                {
                    SeasonTicketId = s.SeasonTicketId,
                    ServiceId = s.ServiceId,
                    Service = s.Service,
                    Sessions = s.Sessions,
                    ValidityPeriod = s.ValidityPeriod,
                    Price = _seasonTicketPriceRepository.GetPrice(s.SeasonTicketId),
                },
                Message = "OK"
            });

        }

        [HttpPost]
        public IActionResult Create(SeasonTicketDto seasonTicketDto)
        {
            try
            {
                SeasonTicket seasonTicket = new SeasonTicket()
                {
                    ServiceId = seasonTicketDto.ServiceId,
                    Sessions = seasonTicketDto.Sessions,
                    ValidityPeriod = seasonTicketDto.ValidityPeriod
                };

                var seasonTicketPrice = new SeasonTicketPrice()
                {
                    Price = seasonTicketDto.Price,
                    DateTime = DateTime.Now,
                };
                
                seasonTicket.SeasonTicketPrices = [seasonTicketPrice];

                if (!(_seasonTicketRepository.CheckForDuplication(seasonTicket) && _seasonTicketRepository.Add(seasonTicket)))
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, SeasonTicketDto seasonTicketDto)
        {
            try
            {
                SeasonTicket? seasonTicketOld = _seasonTicketRepository.Get(id);

                if (seasonTicketOld is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                SeasonTicket seasonTicketNew = new()
                {
                    SeasonTicketId = seasonTicketOld.SeasonTicketId,
                    ServiceId = seasonTicketDto.ServiceId,
                    Sessions = seasonTicketDto.Sessions,
                    ValidityPeriod = seasonTicketDto.ValidityPeriod
                };

                bool isChanged = false;
                if (!(seasonTicketOld.ServiceId == seasonTicketNew.ServiceId
                    && seasonTicketOld.Sessions == seasonTicketNew.Sessions
                    && seasonTicketOld.ValidityPeriod == seasonTicketNew.ValidityPeriod))
                {
                    if (_seasonTicketRepository.CheckForDuplication(seasonTicketNew)
                        && _seasonTicketRepository.Update(seasonTicketOld, seasonTicketNew))
                    {
                        
                        isChanged = true;
                    }
                    else
                    {
                        Debug.WriteLine(seasonTicketNew.SeasonTicketId);
                        return Ok(new { Message = "DUPLICATION" });
                    }
                }

                SeasonTicketPrice seasonTicketPrice = new()
                {
                    SeasonTicketId = seasonTicketOld.SeasonTicketId,
                    Price = seasonTicketDto.Price,
                    DateTime = DateTime.Now,
                };

                if (_seasonTicketPriceRepository.CheckForDuplication(seasonTicketPrice))
                {
                    isChanged = true;
                    _seasonTicketPriceRepository.Add(seasonTicketPrice);
                }   

                if (!isChanged)
                {
                    return Ok(new { Message = "NOT CHANGED" });
                }

                return Ok(new { Message = "OK" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                SeasonTicket? service = _seasonTicketRepository.Get(id);

                if (service is null)
                {
                    return Ok(new { Message = "NOT FOUND" });
                }

                if (!(_seasonTicketRepository.AccessDelete(service) && _seasonTicketRepository.Delete(service)))
                {

                    return Ok(new { Message = "BLOCK" });
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
