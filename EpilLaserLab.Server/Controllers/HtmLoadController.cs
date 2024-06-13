using Microsoft.AspNetCore.Mvc;

namespace EpilLaserLab.Server.Controllers;

[Route("/")]
public class HtmLoadController : Controller
{
    [HttpGet]
    public ContentResult GetHtmlClient()
    {

        return new ContentResult
        {
            ContentType = "text/html",
            Content = System.IO.File.ReadAllText("../epillaserlab.client/dist/index.html")
        };
    }

    [HttpGet("/dashboard/{path?}/{subpath?}/{subsubpath?}/{subsubsubpath?}")]
    public ContentResult GetDashboardClient(string? path = null, string? subpath = null, string? subsubpath = null, string? subsubsubpath = null)
    {

        return new ContentResult
        {
            ContentType = "text/html",
            Content = System.IO.File.ReadAllText("../epillaserlab.client/dist/index.html")
        };
    }

    [HttpGet("/dashboard-login")]
    public ContentResult GetDashboardLoginClient()
    {

        return new ContentResult
        {
            ContentType = "text/html",
            Content = System.IO.File.ReadAllText("../epillaserlab.client/dist/index.html")
        };
    }

    [HttpGet("/assets/{filename}.js")]
    public ContentResult GetJSClient(string fileName)
    {

        return new ContentResult
        {
            ContentType = "text/jscript",
            Content = System.IO.File.ReadAllText($"../epillaserlab.client/dist/assets/{fileName}.js")
        };
    }

    [HttpGet("/assets/{filename}.css")]
    public ContentResult GetCSSClient(string fileName)
    {

        return new ContentResult
        {
            ContentType = "text/css",
            Content = System.IO.File.ReadAllText($"../epillaserlab.client/dist/assets/{fileName}.css")
        };
    }

    [HttpGet("/assets/{filename}.png")]
    public IActionResult GetPNGClient(string fileName)
    {
        return File(System.IO.File.OpenRead($"../epillaserlab.client/dist/assets/{fileName}.png"), "image/png", fileName);
    }

    [HttpGet("/favicon.ico")]
    public IActionResult GetFaviconIcoClient(string fileName)
    {
        return File(System.IO.File.OpenRead($"../epillaserlab.client/dist/favicon.ico"), "image/x-icon", fileName);
    }
}
