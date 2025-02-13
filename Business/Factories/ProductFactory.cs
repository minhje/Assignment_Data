using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ProductFactory 
{
    public static ProductRegistrationForm CreateRegistrationForm() => new();
    public static ProductEntity CreateEntity(ProductRegistrationForm form) => new()
    {
        ProductName = form.ProductName,
        Price = form.Price,

    };

    public static ProductModel CreateModel(ProductEntity entity) => new()
    {
        Id = entity.Id,
        ProductName = entity.ProductName,
        Price = entity.Price,
    };

    public static ProductUpdateForm CreateUpdateForm(ProductModel productModel) => new()
    {
        Id = productModel.Id,
        ProductName = productModel.ProductName,
        Price = productModel.Price,
    };

    public static ProductEntity CreateEntity(ProductEntity productEntity, ProductUpdateForm form) => new()
    {
        ProductName = form.ProductName,
        Price = form.Price,
    };
}
