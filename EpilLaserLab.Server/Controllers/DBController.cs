using EpilLaserLab.Server.Data;
using EpilLaserLab.Server.Dtos.DB;
using EpilLaserLab.Server.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Net;

namespace EpilLaserLab.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DBController(EpilLaserLabContext context,
    DumpSaveService dumpSaveService) : ControllerBase
{
    [HttpGet("data")]
    public IActionResult GetData()
    {
        return Ok(new { Message = "OK", DBConnectData.Data});
    }

    [HttpPost("dump")]
    public IActionResult GetDump()
    {
        string? fileName = CreateDump();

        if (fileName is null) return BadRequest();

        string fileURl = $"{Request.Scheme}://{Request.Host}/resources/dumps/{fileName}";
        return Ok(new { Message = "OK", Dump = fileURl });

    }

    public string? CreateDump()
    {
        if (!(context.Database.GetDbConnection() is MySqlConnection conn)) return null;
        using MySqlCommand cmd = new MySqlCommand();
        using MySqlBackup mb = new MySqlBackup(cmd);

        string fileName = $"{DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss")}_epillaserlab.sql";
        string file = Path.Combine("Resources", "Dumps", fileName);

        cmd.Connection = conn;
        conn.Open();
        mb.ExportToFile(file);
        conn.Close();

        return fileName;
    }

    [HttpPost("restore")]
    public async Task<IActionResult> RestoreDump([FromBody] RestoreDumpDto restoreDumpDto) {
        string? savedDumpFileName = CreateDump();
        if (savedDumpFileName is null) return BadRequest();

        if (!(context.Database.GetDbConnection() is MySqlConnection conn)) return BadRequest();

        var tableNames = context.Model.GetEntityTypes()
            .Select(t => t.GetTableName())
            .Distinct()
            .ToList();

        foreach (var tableName in tableNames)
        {
            context.Database.ExecuteSqlRaw($"SET FOREIGN_KEY_CHECKS = 0; TRUNCATE TABLE `{tableName}`;");
        }

        using MySqlCommand cmd = new MySqlCommand();
        using MySqlBackup mb = new MySqlBackup(cmd);

        string fileName = dumpSaveService.SaveFile(restoreDumpDto.DumpBase64);
        string filePath = dumpSaveService.GetFilePath(fileName);

        cmd.Connection = conn;
        conn.Open();


        mb.ImportFromFile(filePath);
        conn.Close();

        bool enabled = false;

        var database = context.Database;
        int pendingMigrations = database.GetPendingMigrations().Count();
        enabled = pendingMigrations == 0;

        if (enabled)
        {
            await HttpContext.SignOutAsync();

            return Ok(new
            {
                Message = "OK",
            });
        }
        else
        {
            foreach (var tableName in tableNames)
            {
                context.Database.ExecuteSqlRaw($"SET FOREIGN_KEY_CHECKS = 0; TRUNCATE TABLE `{tableName}`;");
            }

            cmd.Connection = conn;
            conn.Open();
            mb.ImportFromFile(dumpSaveService.GetFilePath(savedDumpFileName));
            conn.Close();
            return Ok(new
            {
                Message = "ERROR",
            });
        }

    }


    [HttpPost("change")]
    public async Task<IActionResult> ChangeData(DataDBDto changeDBDto)
    {
        try
        {
            bool enabled = false;

            var optionsBuilder = new DbContextOptionsBuilder<EpilLaserLabContext>();
            optionsBuilder.UseMySQL(changeDBDto.ConnectString);

            using (EpilLaserLabContext context = new EpilLaserLabContext(optionsBuilder.Options))
            {
                var database = context.Database;
                int pendingMigrations = database.GetPendingMigrations().Count();
                enabled = pendingMigrations == 0;
            }

            if (enabled)
            {
                await HttpContext.SignOutAsync();

                DBConnectData.Data = changeDBDto;

                return Ok(new
                {
                    Message = "OK",
                    DBConnectData.Data
                });
            }
            else
            {
                return Ok(new
                {
                    Message = "ERROR",
                });
            }
        }catch (Exception ex)
        {
            return Ok(new
            {
                Message = "DATA NOT VALID",
            });
        }


    }
}
