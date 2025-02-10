using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProductService
{
    Task<ProductModel> CreateProductAsync(ProductRegistrationForm form);
    Task<IEnumerable<ProductModel>> GetAllProductsAsync();
    Task<ProductModel> GetProductAsync(Expression<Func<ProductEntity, bool>> expression);
    Task<ProductModel> UpdateProductAsync(ProductUpdateForm form);
    Task<bool> DeleteProductAsync(int id);
    //Task<bool> CheckIfProductExistsAsync(Expression<Func<ProductEntity, bool>> expression);
}