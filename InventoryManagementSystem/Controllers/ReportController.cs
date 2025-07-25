using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
			var salesSummary = _context.Sales
				.Include(s => s.Product)
				.ThenInclude(p => p.Category)
				.GroupBy(s => s.SaleDate.Date)
				.Select(g => new {
					Date = g.Key,
					TotalSales = g.Sum(x => x.QuantitySold * x.Product.Price)
				}).ToList();

			return View(salesSummary);
		}
	}

}
