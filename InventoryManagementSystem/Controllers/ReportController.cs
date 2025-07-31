using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace InventoryManagementSystem.Controllers
{
	public class ReportController : Controller
	{
		private readonly InventoryDbContext _context;

		public ReportController(InventoryDbContext context)
		{
			_context = context;
		}

        public IActionResult Index()
        {
            var report = _context.Sales
                .Include(s => s.Product)
                .GroupBy(s => s.SaleDate.Date)
                .Select(g => new SalesReportViewModel
                {
                    Date = g.Key,
                    TotalSales = g.Sum(s => s.QuantitySold * s.Product.Price)
                }).ToList();

            return View(report);
        }

        public IActionResult ExportToExcel()
        {
            var salesData = _context.Sales
                .Include(s => s.Product)
                .GroupBy(s => s.SaleDate.Date)
                .Select(g => new {
                    Date = g.Key.ToShortDateString(),
                    TotalSales = g.Sum(s => s.QuantitySold * s.Product.Price)
                }).ToList();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("SalesReport");

            worksheet.Cells[1, 1].Value = "Date";
            worksheet.Cells[1, 2].Value = "Total Sales";

            for (int i = 0; i < salesData.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = salesData[i].Date;
                worksheet.Cells[i + 2, 2].Value = salesData[i].TotalSales;
            }

            var stream = new MemoryStream(package.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesReport.xlsx");
        }


    }

}
