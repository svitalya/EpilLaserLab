using EpilLaserLab.Server.Models;
using OfficeOpenXml;
using Org.BouncyCastle.Asn1.X509;

namespace EpilLaserLab.Server.Helpers;

public class DocumentService
{

    const string DOCS_FOLDER = "Resources/Docs";

    public Stream GetDocument(string name)
    {
        return File.OpenRead(Path.Combine(DOCS_FOLDER, name));
    }

    public string CreateDocument(DocumentData data)
    {

        using var package = new ExcelPackage();

        var worksheet = package.Workbook.Worksheets.Add(data.Title);


        worksheet.Cells[1, 1].Value = data.Title;
        var titleRange = worksheet.Cells[1, 1, 1, data.Columns.Length];
        titleRange.Style.Font.Bold = true;
        titleRange.Style.Font.Name = "Calibri";
        titleRange.Style.Font.Size = 16;
        titleRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        titleRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        titleRange.Merge = true;
        

        worksheet.Cells[2, 1].Value = $"Сформирован: {data.Created.ToString("D")}";
        var createdAtRange = worksheet.Cells[2, 1, 2, data.Columns.Length];
        createdAtRange.Style.Font.Bold = true;
        createdAtRange.Style.Font.Name = "Calibri";
        createdAtRange.Style.Font.Size = 16;
        createdAtRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
        createdAtRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        createdAtRange.Merge = true;

        for(int i = 1; i <= data.Columns.Length; i++)
        {
            worksheet.Cells[3, i].Value = data.Columns[i-1];
        }

        var headerRange = worksheet.Cells[3, 1, 3, data.Columns.Length];
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Font.Name = "Calibri";
        headerRange.Style.Font.Size = 11;
        headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        headerRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

        int tableRowsI = 4;
        for (; tableRowsI-3 <= data.Rows.Length; tableRowsI++)
        {
            string[] rows = data.Rows[tableRowsI-4];
            for(int j = 1; j <= rows.Length; j++)
            {
                worksheet.Cells[tableRowsI, j].Value = rows[j-1];
            }
        }

        var bodyRange = worksheet.Cells[4, 1, tableRowsI-2, data.Columns.Length];
        bodyRange.Style.Font.Name = "Calibri";
        bodyRange.Style.Font.Size = 11;

        var footerRange = worksheet.Cells[tableRowsI-1, 1, tableRowsI-1, data.Columns.Length];
        footerRange.Style.Font.Name = "Calibri";
        footerRange.Style.Font.Bold = true;
        footerRange.Style.Font.Size = 11;


        worksheet.Cells[tableRowsI + 1, 2].Value = "М.П.";
        worksheet.Cells[tableRowsI + 1, 2].Style.Font.Size = 14;
        worksheet.Cells[tableRowsI + 1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

        worksheet.Cells[tableRowsI + 1, data.Columns.Length-1].Value = data.Avtor;
        worksheet.Cells[tableRowsI + 1, data.Columns.Length - 1].Style.Font.UnderLine = true;
        worksheet.Cells[tableRowsI + 1, data.Columns.Length - 1].Style.Font.Size = 14;
        worksheet.Cells[tableRowsI + 1, data.Columns.Length - 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        worksheet.Cells[tableRowsI + 1, data.Columns.Length].Value = "/__________________";
        worksheet.Cells[tableRowsI + 1, data.Columns.Length].Style.Font.Size = 14;
        worksheet.Cells[tableRowsI + 1, data.Columns.Length].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

        worksheet.Cells[1, 1, tableRowsI + 1, data.Columns.Length].AutoFitColumns();

        string fileName = $"{data.Title.Replace(' ', '_').ToLower()}_{data.Created:yyyy'_'MM'_'dd'_'HH'_'mm'_'ss}.xlsx";
        package.SaveAs(new FileInfo(Path.Combine(DOCS_FOLDER, fileName)));
        return fileName;
    }
}

public class DocumentData
{
    public required string Title { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public required string Avtor { get; set; }
    public required string[] Columns { get; set; }

    public required string[][] Rows { get; set; }

}
