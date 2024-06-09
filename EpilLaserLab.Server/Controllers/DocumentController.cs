using EpilLaserLab.Server.Data.Applications;
using EpilLaserLab.Server.Helpers;
using EpilLaserLab.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace EpilLaserLab.Server.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController(
        DocumentService testDocumentService,
        IApplicationsRepository applicationsRepository
        ) : ControllerBase
    {


        [HttpGet("test")]
        public IActionResult CreateDocument(string type)
        {
            //ICollection<Application> applications = applicationsRepository.GetQuerable().ToArray();
                            

            //Stream stream = testDocumentService.GetDocument(applications);

            //if (stream == null)
            //    return NotFound(); // returns a NotFoundResult with Status404NotFound response.

            //return File(stream, "application/pdf", name); // returns a FileStreamResult
            throw new NotImplementedException();
        }
    }
}
