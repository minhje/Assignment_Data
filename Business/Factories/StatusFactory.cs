using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class StatusFactory
{
    public static StatusRegistrationForm CreateRegistrationForm() => new();

    public static StatusEntity CreateEntity(StatusRegistrationForm form) => new()
    {
        StatusName = form.StatusName
    };
    public static StatusModel CreateModel(StatusEntity entity) => new()
    {
        Id = entity.Id,
        Status = entity.StatusName
    };
    public static StatusUpdateForm CreateUpdateForm(StatusModel statusModel) => new()
    {
        Id = statusModel.Id,
        Status = statusModel.Status
    };

    public static StatusEntity CreateEntity(StatusEntity statusEntity, StatusUpdateForm form) => new()
    {
        Id = form.Id,
        StatusName = form.Status
    };
}