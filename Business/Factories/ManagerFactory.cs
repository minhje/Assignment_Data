using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ManagerFactory
{
    public static ManagerRegistrationForm CreateRegistrationForm() => new();


    public static ManagerEntity Create(ManagerRegistrationForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName
    };

    public static ManagerModel Create(ManagerEntity manager) => new()
    {
        Id = manager.Id,
        FirstName = manager.FirstName,
        LastName = manager.LastName
    };
    public static ManagerEntity Create(ManagerUpdateForm form) => new()
    {
        Id = form.Id,
        FirstName = form.FirstName,
        LastName = form.LastName
    };
}
