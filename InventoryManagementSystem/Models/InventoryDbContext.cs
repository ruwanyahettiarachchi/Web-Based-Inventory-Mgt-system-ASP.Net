namespace InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

public class InventoryDbContext : DbContext
{
	public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Sale> Sales { get; set; }
}
