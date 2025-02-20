using System.Globalization;

namespace Business.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public string NamePrice => $"{ProductName} - {Price.ToString("C", CultureInfo.CurrentCulture)}/h";  // Skapad med hjälp av ChatGPT 4o för att kunna visa både namn och pris tillsammans
}
