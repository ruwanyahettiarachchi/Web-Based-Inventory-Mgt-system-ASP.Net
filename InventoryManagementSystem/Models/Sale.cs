public class Sale
{
	public int SaleId { get; set; }
	public int ProductId { get; set; }
	public int QuantitySold { get; set; }
	public DateTime SaleDate { get; set; }

	public required Product? Product { get; set; }
}
