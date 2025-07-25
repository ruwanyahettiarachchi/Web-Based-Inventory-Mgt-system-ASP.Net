public class Product
{
	public int ProductId { get; set; }
	public string Name { get; set; }
	public int CategoryId { get; set; }
	public int Quantity { get; set; }
	public decimal Price { get; set; }

	public required Category? Category { get; set; }
}
