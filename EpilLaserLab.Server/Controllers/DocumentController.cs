using EpilLaserLab.Server.Data.Applications;
using EpilLaserLab.Server.Data.Auths;
using EpilLaserLab.Server.Data.Employees;
using EpilLaserLab.Server.Data.Services;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController(
        DocumentService documentService,
        IUserRepository userRepository,
        IMasterRepository masterRepository,
        IServiceRepository serviceRepository
        ) : ControllerBase
    {

        public string GetUrl(string fileName) => $"{Request.Scheme}://{Request.Host}/resources/docs/{fileName}";

        [HttpPost("returnability")]
        public IActionResult CreateDocumentReturnability()
        {
            try
            {

                int countAppResult = 0;
                int countAppNewResult = 0;
                int clientsOldResult = 0;
                int clientsReturnabilityResult = 0;

                string[] Selector(IGrouping<Employee, Master> g)
                {
                    var countApp = g.Key.Master!.Schedules.Sum(s => s.Intervals.Where(i => i.Application is not null && i.Application.DateTimeClosed is not null).Count());
                    var countAppNew = g.Key.Master!.Schedules.Sum(s => s.Intervals.Where(
                        i => i.Application is not null && i.Application.DateTimeClosed is not null
                        && i.Application == (i.Application.Client.Applications.OrderBy(a => a.DateTimeCreated)
                        .FirstOrDefault())).Count());

                    var client = g.Key.Master!.Schedules.Select(
                            s => s.Intervals.Where(i => i.Application is not null
                                && i.Application.DateTimeClosed is not null
                                && i.Application.DateTimeClosed >= DateTime.Now.AddMonths(-1).AddDays(-62)
                                && i.Application.DateTimeClosed < DateTime.Now.AddMonths(-1)
                            )
                            .Select(i => i.Application!.Client)
                            .ToArray()
                    ).SelectMany(cs => cs)
                        .GroupBy(c => c)
                        .Select(c => c.Key)
                        .ToArray();



                    countAppResult += countApp;
                    countAppNewResult += countAppNew;
                    int clientsOld = client.Count();
                    int clientsReturnability = client.Where(c => c.Applications.Where(a => a.DateTimeClosed >= DateTime.Now.AddMonths(-1)).Count() > 0).Count();
                    clientsOldResult += clientsOld;
                    clientsReturnabilityResult += clientsReturnability;

                    return
                    [
                        g.Key.FIO,
                        countApp.ToString(),
                        countAppNew.ToString(),
                        (countApp > 0 ? (decimal)countAppNew / (decimal)countApp * 100 : 0).ToString("N2"),
                        clientsOld.ToString(),
                        clientsReturnability.ToString()
                     ];
                }

                string[][] rows = masterRepository.GetQueryable()
                        .Include(m => m.Employee)
                        .Include(m => m.Schedules)
                            .ThenInclude(s => s.Intervals)
                                .ThenInclude(i => i.Application)
                                    .ThenInclude(a => a.Client)
                        .GroupBy(m => m.Employee)
                        .ToArray()
                        .Select(Selector)
                        .ToArray();

                rows = rows.Append(new string[] {
                    "Всего",
                    countAppResult.ToString(),
                    countAppNewResult.ToString(),
                    ((decimal)countAppNewResult / (decimal)countAppResult * 100).ToString("N2"),
                    clientsOldResult.ToString(),
                    clientsReturnabilityResult.ToString()
                    })
                    .ToArray();

                string avtor = GetAvtor();

                string fileName = documentService.CreateDocument(new DocumentData
                {
                    Title = "Возвращаемость",
                    Avtor = avtor,
                    Columns = ["Сотрудник",
                        "Всего", "Новые",
                        "Новые %", $"Клиентов за 62 дня до {DateTime.Now.AddMonths(-1):dd.MM.yyyy}",
                        $"Из них вернулись с {DateTime.Now.AddMonths(-1):dd.MM.yyyy} по {DateTime.Now:dd.MM.yyyy}"],
                    Rows = rows
                });

                return Ok(new { Message = "OK", FileUrl = GetUrl(fileName) });
            }
            catch
            {
                return Ok(new { Message = "BAD REQUEST" });
            }

        }

        private string GetAvtor()
        {
            User authUser = userRepository!.GetAuth(HttpContext);

            if (authUser == null)
            {
                throw new Exception();
            }

            return authUser.Admin?.Employee.FIO ?? "Чучин А.М.";
        }

        [HttpPost("salesbyemployees")]
        public IActionResult CreateDocumentSalesByEmployees()
        {
            try
            {
                decimal sumAppResult = 0;
                decimal countAppResult = 0;
                decimal sumPurshedResult = 0;
                decimal workHourResult = 0;
                decimal allProfit = 0;

                string[] Selector(IGrouping<Employee, Master> g)
                {
                    var sumApp = g.Key.Master!.Schedules.Sum(s => s.Intervals
                            .Where(i => i.Application is not null
                                    && i.Application.DateTimeClosed is not null
                                    && i.Application.PurchasedSeasonTicket is null)
                            .Sum(i => i.Application!.ServicePrice.Price));

                    var countApp = g.Key.Master!.Schedules.Sum(s => s.Intervals
                                .Where(i => i.Application is not null
                                        && i.Application.DateTimeClosed is not null)
                                .Count());

                    var sumPurshed = g.Key.Master!.Schedules.Sum(s => s.Intervals
                                    .Where(i => i.Application is not null
                                            && i.Application.DateTimeClosed is not null
                                            && i.Application.PurchasedSeasonTicket is not null)
                                    .Sum(i => i.Application!.PurchasedSeasonTicket!.SeasonTicketPrice.Price 
                                            / i.Application!.PurchasedSeasonTicket.SeasonTicketPrice.SeasonTicket.Sessions));

                    var workHour = (g.Key.Master!.Schedules.Sum(s => s.Intervals
                                    .Where(i => i.Application is not null
                                            && i.Application.DateTimeClosed is not null)
                                    .Sum(i => i.Application!.ServicePrice.Service.TimeCost))) / 60;

                    sumAppResult += sumApp;
                    countAppResult += countApp;
                    sumPurshedResult += sumPurshed;
                    workHourResult += workHour;

                    allProfit += (sumApp + sumPurshed);

                    return
                    [
                        g.Key.FIO,
                        sumApp.ToString("N2"),
                        countApp.ToString(),
                        sumPurshed.ToString("N2"),
                        workHour.ToString("N2"),
                        (sumApp + sumPurshed).ToString()
                     ];
                }

                string[] CalculatingPercentage(string[] r)
                {
                    r[5] = (allProfit > 0 ? decimal.Parse(r[5]) / allProfit * 100 : 0).ToString("N2");
                    return r;
                }

                string[][] rows = masterRepository.GetQueryable()
                        .Include(m => m.Employee)
                        .Include(m => m.Schedules)
                            .ThenInclude(s => s.Intervals)
                                .ThenInclude(i => i.Application)
                                    .ThenInclude(a => a.ServicePrice)
                                        .ThenInclude(s => s.Service)
                        .Include(m => m.Schedules)
                            .ThenInclude(s => s.Intervals)
                                .ThenInclude(i => i.Application)
                                    .ThenInclude(a => a.PurchasedSeasonTicket)
                                        .ThenInclude(pst => pst.SeasonTicketPrice)
                                            .ThenInclude(stp => stp.SeasonTicket)
                        .GroupBy(m => m.Employee)
                        .ToArray()
                        .Select(Selector)
                        .ToArray();

                rows = rows.Select(CalculatingPercentage).ToArray();

                rows = rows.Append(new string[] {
                    "Всего",
                    sumAppResult.ToString("N2"),
                    countAppResult.ToString(),
                    sumPurshedResult.ToString("N2"),
                    workHourResult.ToString(),
                    ""
                    })
                    .ToArray();

                var avtor = GetAvtor();


                string fileName = documentService.CreateDocument(new DocumentData
                {
                    Title = "Продажи по сотрудникам",
                    Avtor = avtor,
                    Columns = ["Сотрудник",
                        "Сумма оказанных услуг", "Количество оказанных услуг",
                        "Абонементы", $"Отработано часов",
                        $"% от общей выручки"],
                    Rows = rows
                });

                return Ok(new { Message = "OK", FileUrl = GetUrl(fileName) });

            }
            catch
            {
                return Ok(new { Message = "BAD REQUEST" });
            }
        }

        [HttpPost("salesbyservices")]
        public IActionResult CreateDocumentSalesByServices()
        {
            try
            {
                int colAppResult = 0;
                decimal sumSeasonTicketResult = 0;
                decimal sumPriceResult = 0;
                decimal allSum = 0;

                string[] Selector(Service service)
                {
                    var colApp = service.ServicePrices.Sum(sp => sp.Applications
                        .Where(a => a.DateTimeClosed is not null).Count());

                    var sumSeasonTicket = service.ServicePrices
                        .SelectMany(sp => sp.Applications)
                        .Where(a => a.DateTimeClosed is not null)
                        .Where(a => a.PurchasedSeasonTicket is not null)
                        .Sum(a => a.PurchasedSeasonTicket!.SeasonTicketPrice.Price);

                    var sumPrice = service.ServicePrices
                        .SelectMany(sp => sp.Applications)
                        .Where(a => a.DateTimeClosed is not null)
                        .Where(a => a.PurchasedSeasonTicket is null)
                        .Sum(a => a.ServicePrice.Price);



                    colAppResult += colApp;
                    sumSeasonTicketResult += sumSeasonTicket;
                    sumPriceResult += sumPrice;

                    var allSumByService = (sumPrice + sumSeasonTicket);
                    allSum += allSumByService;

                    return
                    [
                        service.Name,
                        colApp.ToString(),
                        sumSeasonTicket.ToString("N2"),
                        sumPrice.ToString("N2"),
                        allSumByService.ToString()
                    ];
                }

                string[] CalculatingPercentage(string[] r)
                {
                    r[4] = (allSum > 0 ? decimal.Parse(r[4]) / allSum * 100 : 0).ToString("N2");
                    return r;
                }

                string[][] rows = serviceRepository.GetQuerable()
                        .Include(s => s.ServicePrices)
                            .ThenInclude(sp => sp.Applications)
                                .ThenInclude(a => a.PurchasedSeasonTicket)
                        .Select(Selector)
                        .ToArray();

                rows = rows.Select(CalculatingPercentage).ToArray();

                rows = rows.Append(new string[] {
                    "Всего",
                    colAppResult.ToString(),
                    sumSeasonTicketResult.ToString("N2"),
                    sumPriceResult.ToString("N2"),
                    ""
                    })
                    .ToArray();

                var avtor = GetAvtor();


                string fileName = documentService.CreateDocument(new DocumentData
                {
                    Title = "Продажи по услугам",
                    Avtor = avtor,
                    Columns = ["Услуга", "Количество оказанных услуг",
                        "Абонементы", $"Оплачено деньгами",
                        $"% от общей выручки"],
                    Rows = rows
                });

                return Ok(new { Message = "OK", FileUrl = GetUrl(fileName) });

            }
            catch
            {
                return Ok(new { Message = "BAD REQUEST" });
            }
        }
    }
}
