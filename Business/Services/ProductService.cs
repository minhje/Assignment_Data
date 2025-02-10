using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ProductModel> CreateProductAsync(ProductRegistrationForm form)
    {
        if (form != null)
        {
            Console.WriteLine("Product already exists");
        }

        try
        {
            var product = ProductFactory.CreateEntity(form);
            var createdProduct = await _productRepository.CreateAsync(product);
            var productModel = ProductFactory.CreateModel(createdProduct);
            return productModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productModels = products.Select(ProductFactory.CreateModel);
        return productModels;

    }

    public async Task<ProductModel> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        if (expression == null)
        {
            Console.WriteLine("Product not found");
            return null!;
        }
        var productEntity = await _productRepository.GetAsync(expression);
        return ProductFactory.CreateModel(productEntity);


    }

    public async Task<ProductModel> UpdateProductAsync(ProductUpdateForm form)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == form.Id);
        if (productEntity == null)
        {
            throw new Exception("Product not found");
        }
        var updatedProductEntity = ProductFactory.CreateEntity(productEntity, form);
        return ProductFactory.CreateModel(updatedProductEntity);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == id);
        if (productEntity == null)
        {
            throw new Exception("Product not found");
        }
        return await _productRepository.DeleteAsync(x => x.Id == id); 
    }
}
