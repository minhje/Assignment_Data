﻿namespace Business.Dtos;

public class ProjectUpdateForm
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int ManagerId { get; set; }
    public int StatusId { get; set; }
}
