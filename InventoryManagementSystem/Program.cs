using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Models; // Your namespace where InventoryDbContext is

namespace InventoryManagementSystem
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

			// Register InventoryDbContext with MySQL
			builder.Services.AddDbContext<InventoryDbContext>(options =>
				options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))) // change version as needed
			);

			// Keep your MVC services
			builder.Services.AddControllersWithViews();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
