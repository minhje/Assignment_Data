namespace Business.Models;

public class ManagerModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string DisplayName => $"{FirstName} {LastName}";
}
