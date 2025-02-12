using Data.Entities;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; } = DateTime.Now;

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int ManagerId { get; set; }

    public int StatusId { get; set; }
}
