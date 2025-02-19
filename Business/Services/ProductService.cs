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

    // Tog hjälp av ChatGTP 4o för att få in Transaction Management 
    public async Task<ProductModel> CreateProductAsync(ProductRegistrationForm form)
    {
        await _productRepository.BeginTransactionAsync();

        try
        {
            var product = ProductFactory.CreateEntity(form);
            await _productRepository.CreateAsync(product);
            await _productRepository.CommitTransactionAsync();
            return ProductFactory.CreateModel(product);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _productRepository.RollbackTransactionAsync();
            throw;
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

        try 
        { 
            var productEntity = await _productRepository.GetAsync(expression);
            return ProductFactory.CreateModel(productEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;
        }


    }

    public async Task<ProductModel> UpdateProductAsync(ProductUpdateForm form)
    {
        await _productRepository.BeginTransactionAsync();

        try
        {
           var productEntity = await _productRepository.GetAsync(x => x.Id == form.Id);
           if (productEntity == null)
           {
               throw new Exception("Product not found");
           }
        var updatedProductEntity = ProductFactory.CreateEntity(productEntity, form);
            await _productRepository.CommitTransactionAsync();
            return ProductFactory.CreateModel(updatedProductEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _productRepository.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        await _productRepository.BeginTransactionAsync();

        try
        {
            var productEntity = await _productRepository.GetAsync(x => x.Id == id);
            if (productEntity == null)
            {
                throw new Exception("Product not found");
            }
            var result = await _productRepository.DeleteAsync(x => x.Id == id);
            await _productRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _productRepository.RollbackTransactionAsync();
            throw;
        }
    }
}
