using EpilLaserLab.Server.Data.Applications;
using EpilLaserLab.Server.Data.Employees;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController(
        DocumentService documentService,
        IMasterRepository masterRepository
        ) : ControllerBase
    {

        public string GetUrl(string fileName) => $"{Request.Scheme}://{Request.Host}/resources/docs/{fileName}";

        [HttpPost("returnability")]
        public IActionResult CreateDocumentReturnability()
        {
            //try
            //{

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
                        clientsOld.ToString()
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


                string fileName = documentService.CreateDocument(new DocumentData
                {
                    Title = "Возвращаемость",
                    Avtor = "Чучин А.М.",
                    Columns = ["Сотрудник",
                        "Всего", "Новые",
                        "Новые %", $"Клиентов за 62 дня до {DateTime.Now.AddMonths(-1):dd.MM.yyyy}",
                        $"Из них вернулись с {DateTime.Now.AddMonths(-1):dd.MM.yyyy} по {DateTime.Now:dd.MM.yyyy}"],
                    Rows = rows
                });

                return Ok(new { Message = "OK", FileUrl = GetUrl(fileName) });
            //}
            //catch
            //{
            //    return Ok(new { Message = "BAD REQUEST" });
            //}

        }
    }
}
