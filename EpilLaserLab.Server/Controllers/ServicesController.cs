using EpilLaserLab.Server.Data.References;
using EpilLaserLab.Server.Data.Services;
using EpilLaserLab.Server.Dtos;
using EpilLaserLab.Server.Dtos.References;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using System.Linq.Expressions;

namespace EpilLaserLab.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController(
        IServiceRepository repository,
        IServicePricesRepository servicePricesRepository
    ) : ControllerBase
{
    private readonly IServiceRepository _repository = repository;
    private readonly IServicePricesRepository _servicePricesRepository = servicePricesRepository;

    object? orderById(Service s) => s.ServiceId;
    object? orderByName(Service s) => s.Name;
    object? orderByDescription(Service s) => s.Description;
    object? orderByTimeCost(Service s) => s.TimeCost;

    [HttpGet]
    public IActionResult GetList(int page = 0, int limit = 10, string order = "id", string sort = "asc")
    {
        Dictionary<string, Func<Service, object?>> functor = [];

        functor.Add("id", orderById);
        functor.Add("name", orderByName);
        functor.Add("description", orderByDescription);
        functor.Add("timeCost", orderByTimeCost);

        var querable = _repository.GetQuerable();
        if (functor.TryGetValue(order, out Func<Service, object?>? f) && f is not null)
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


        var recs = querable.AsQueryable()
            .Skip(page * limit)
            .Take(limit)
            .ToArray();


        return Ok(new
        {
            Data = new
            {
                Recs = recs.Select(s => new ServiceDto
                {
                    ServiceId = s.ServiceId,
                    Description = s.Description,
                    Name = s.Name,
                    TimeCost = s.TimeCost,
                    PriceByType = _servicePricesRepository.GetPriceByTypes(s.ServiceId),
                }),
                Page = page,
                Max = maxRecs
            },
            Message = "OK"
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var rec = _repository.Get(id);

        if (rec is null)
        {
            return Ok(new { Message = "NOT FOUND" });
        };

        return Ok(new
        {
            Rec = new ServiceDto
            {
                ServiceId = rec.ServiceId,
                Name = rec.Name,
                Description = rec.Description,
                TimeCost = rec.TimeCost,
                PriceByType = _servicePricesRepository.GetPriceByTypes(rec.ServiceId),
            },
            Message = "OK"
        });

    }

    [HttpPost]
    public IActionResult Create(ServiceDto serviceDto)
    {
        try
        {
            Service service = new Service()
            {
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                TimeCost = serviceDto.TimeCost
            };


            var count = serviceDto.PriceByType.Count;
            List<ServicePrice> servicePrices = new List<ServicePrice>(count);

            foreach (var priceInType in serviceDto.PriceByType)
            {
                servicePrices.Add(new ServicePrice()
                {
                    ServiceId = service.ServiceId,
                    TypeId = priceInType.Key,
                    Price = priceInType.Value,
                    DateTime = DateTime.Now,
                });
            }

            service.ServicePrices = servicePrices;

            if (!(_repository.CheckForDuplication(service) && _repository.Add(service)))
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
    public IActionResult Update(int id, ServiceDto serviceDto)
    {
        try
        {
            Service? serviceOld = _repository.Get(id);

            if (serviceOld is null)
            {
                return Ok(new { Message = "NOT FOUND" });
            }

            Service serviceNew = new()
            {
                ServiceId = id,
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                TimeCost = serviceDto.TimeCost,
            };

            bool isChanged = false;
            if (!(serviceOld.Name == serviceNew.Name
                && serviceOld.Description == serviceNew.Description
                && serviceOld.TimeCost == serviceNew.TimeCost))
            {
                if (_repository.CheckForDuplication(serviceNew)
                    && _repository.Update(serviceOld, serviceNew))
                {
                    isChanged = true;
                }
                else
                {
                    return Ok(new { Message = "DUPLICATION" });
                }
            }

            var count = serviceDto.PriceByType.Count;
            List<ServicePrice> servicePrices = new(count);

            foreach (var priceInType in serviceDto.PriceByType)
            {
                var servicePrice = new ServicePrice()
                {
                    ServiceId = serviceOld.ServiceId,
                    TypeId = priceInType.Key,
                    Price = priceInType.Value,
                    DateTime = DateTime.Now,
                };

                if (!_servicePricesRepository.CheckForDuplication(servicePrice)) continue;

                isChanged = true;
                servicePrices.Add(servicePrice);
            }

            if (!isChanged)
            {
                return Ok(new { Message = "NOT CHANGED" });
            }

            _servicePricesRepository.Add(servicePrices);

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
            Service? service = _repository.Get(id);

            if (service is null)
            {
                return Ok(new { Message = "NOT FOUND" });
            }

            if (!(_repository.AccessDelete(service) && _repository.Delete(service)))
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
