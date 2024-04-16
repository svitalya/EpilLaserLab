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
}
