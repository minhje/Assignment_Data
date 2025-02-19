namespace Business.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? StatusName { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int ManagerId { get; set; }
    public int StatusId { get; set; }
    public string? CustomerName { get; set; }
    public string? ManagerName { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; } // Skapad med hjälp av ChatGPT 4o för att kunna visa både namn och pris tillsammans
}