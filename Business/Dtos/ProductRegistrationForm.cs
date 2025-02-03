namespace Business.Dtos;

public class ProductRegistrationForm
{
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public string StatusType { get; set; } = null!;

}
