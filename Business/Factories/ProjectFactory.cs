using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ProjectFactory
{
    public static ProjectRegistrationForm CreateRegistrationForm() => new();

    public static ProjectEntity CreateEntity(ProjectRegistrationForm form) => new()
    {
        Title = form.Title,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        CustomerId = form.CustomerId,
        ProductId = form.ProductId,
        ManagerId = form.ManagerId,
        StatusId = form.StatusId
    };

    public static ProjectModel CreateModel(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        StatusType = entity.Status?.StatusName
    };

    public static ProjectUpdateForm CreateUpdateForm(ProjectModel projectModel) => new()
    {
        Title = projectModel.Title,
        Description = projectModel.Description,
        StartDate = projectModel.StartDate,
        EndDate = projectModel.EndDate,
        CustomerId = projectModel.CustomerId,
        ProductId = projectModel.ProductId,
        ManagerId = projectModel.ManagerId,
        StatusId = projectModel.StatusId
    };

    public static ProjectEntity CreateEntity(ProjectEntity projectEntity, ProjectUpdateForm form) => new()
    {
        Title = form.Title,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        CustomerId = form.CustomerId,
        ProductId = form.ProductId,
        ManagerId = form.ManagerId,
        StatusId = form.StatusId
    };

}