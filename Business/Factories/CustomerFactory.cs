using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class CustomerFactory
{
    public static CustomerRegistrationForm CreateRegistrationForm() => new();

    public static CustomerEntity CreateEntity(CustomerRegistrationForm form) => new()
    {
        CustomerName = form.CustomerName
    };

    public static CustomerModel CreateModel(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName
    };

    public static CustomerUpdateForm CreateUpdateForm(CustomerModel customerModel) => new()
    {
        Id = customerModel.Id,
        CustomerName = customerModel.CustomerName
    };

    public static CustomerEntity CreateEntity(CustomerEntity customerEntity, CustomerUpdateForm form) => new()
    {
        Id = form.Id,
        CustomerName = form.CustomerName
    };
}
