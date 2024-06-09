using EpilLaserLab.Server.Data.SeasonTicket;
using EpilLaserLab.Server.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;

namespace EpilLaserLab.Server.Controllers;
[Route("[controller]")]
public class ResourcesController(
    ImageSaveService imageSaveService,
    DumpSaveService dumpSaveService) : ControllerBase
{

    [HttpGet("images/{name}")]
    public IActionResult GetImage(string name)
    {
        Stream stream = imageSaveService.GetImage(name);

        if (stream == null)
            return NotFound();

        return File(stream, "image/jpeg", name);
    }
    [HttpGet("docs/{name}")]
    public IActionResult GetDoc(string name)
    {
        throw new NotImplementedException();
    }

    [HttpGet("dumps/{name}")]
    public IActionResult GetDump(string name)
    {
        Stream stream = dumpSaveService.GetDump(name);

        if (stream == null)
            return NotFound();

        var result = File(stream, "application/force-download", name);

        return result;
    }
}
