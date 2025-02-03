namespace Business.Dtos;

public class ProductUpdateForm
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public string StatusType { get; set; } = null!;
}
