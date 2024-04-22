using EpilLaserLab.Server.Data.Applications;
using EpilLaserLab.Server.Data.SeasonTicket;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;

namespace EpilLaserLab.Server.Controllers
{
    [Route("[controller]")]
    public class ResourcesController(
        ImageSaveService imageSaveService) : ControllerBase
    {

        private readonly ImageSaveService _imageSaveService = imageSaveService;

        [HttpGet("images/{name}")]
        public IActionResult GetImage(string name)
        {
            Stream stream = _imageSaveService.GetImage(name);

            if (stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.

            return File(stream, "image/jpeg", name); // returns a FileStreamResult
        }
    }

    [Route("api/[controller]")]
    public class DocumentController(
        TestDocumentService testDocumentService,
        IApplicationsRepository applicationsRepository
        ) : ControllerBase
    {


        [HttpGet("test")]
        public IActionResult GetImage(string name)
        {
            ICollection<Application> applications = applicationsRepository.GetQuerable().ToArray();
                            

            Stream stream = testDocumentService.GetDocument(applications);

            if (stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.

            return File(stream, "application/pdf", name); // returns a FileStreamResult
        }
    }
}
