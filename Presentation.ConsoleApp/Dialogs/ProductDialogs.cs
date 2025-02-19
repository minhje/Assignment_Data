using Data.Entities;
using Data.Interfaces;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class ProductDialogs : IProductDialogs
{
    private readonly IProductRepository _productRepository;

    public ProductDialogs(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("---------- PRODUCT MENU ----------");
            Console.WriteLine("1. Add new product");
            Console.WriteLine("2. Show all products");
            Console.WriteLine("3. Show product details");
            Console.WriteLine("4. Update product");
            Console.WriteLine("5. Delete product");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("-----------------------------------");

            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateAsync();
                    break;
                case "2":
                    await GetAllAsync();
                    break;
                case "3":
                    await GetAsync();
                    break;
                case "4":
                    await UpdateAsync();
                    break;
                case "5":
                    await DeleteAsync();
                    break;
                case "6":
                    await MainMenuDialog();
                    return;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }


    private async Task CreateAsync()
    {
        Console.Clear();
        var productEntity = new ProductEntity();
        Console.WriteLine("---------- ADD NEW PRODUCT ----------");
        Console.Write("Enter product name: ");
        productEntity.ProductName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(productEntity.ProductName))
        {
            Console.WriteLine("Product name is required.");
            return;
        }

        Console.Write("Enter product price/h: ");
        productEntity.Price = decimal.Parse(Console.ReadLine()!);
        if (productEntity.Price <= 0)
        {
            Console.WriteLine("Price must be greater than 0.");
            return;
        }

        var result = await _productRepository.CreateAsync(productEntity);

        if (result != null)
        {
            Console.WriteLine($"Product: {productEntity.ProductName} added successfully.");
        }
        else
        {
            Console.WriteLine("Error adding product.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task GetAllAsync()
    {
        Console.Clear();
        var products = await _productRepository.GetAllAsync();
        if (products == null)
        {
            Console.WriteLine("No products found.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Id}");
                Console.WriteLine($"Product name: {product.ProductName}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine("-----------------------------------");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task GetAsync()
    {
        Console.Clear();
        Console.Write("Enter product id: ");
        var id = int.Parse(Console.ReadLine()!);
        var product = await _productRepository.GetAsync(x => x.Id == id);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
        }
        else
        {
            Console.WriteLine($"Id: {product.Id}");
            Console.WriteLine($"Product name: {product.ProductName}");
            Console.WriteLine($"Price: {product.Price}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateAsync()
    {
        Console.Clear();
        Console.Write("Enter product id: ");
        var id = int.Parse(Console.ReadLine()!);
        var product = await _productRepository.GetAsync(x => x.Id == id);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }
        Console.WriteLine("---------- UPDATE PRODUCT ----------");
        Console.Write("Enter product name: ");
        product.ProductName = Console.ReadLine()!;
        Console.Write("Enter product price: ");
        product.Price = decimal.Parse(Console.ReadLine()!);
        var result = await _productRepository.UpdateAsync(x => x.Id == id, product);
        if (result != null)
        {
            Console.WriteLine($"Product: {product.ProductName} updated successfully.");
        }
        else
        {
            Console.WriteLine("Error updating product.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    /* Tog hjälp av ChatGPT 4o för att få hjälp att kunna radera en produkt, fick även med implementation av Transaction Management så testade lägga in det här. Har det inte i någon annan dialog men Console är ej min main presentation. */
    private async Task DeleteAsync()
    {
        Console.Clear();
        Console.Write("Enter product id: ");
        var id = int.Parse(Console.ReadLine()!);
        var product = await _productRepository.GetAsync(x => x.Id == id);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.WriteLine($"Id: {product.Id}");
        Console.WriteLine($"Product name: {product.ProductName}");
        Console.WriteLine($"Price: {product.Price}");
        Console.WriteLine("Are you sure you want to delete this product? (y/n)");
        var choice = Console.ReadLine()!.ToLower();
        if (choice != "y")
        {
            return;
        }

        await _productRepository.BeginTransactionAsync();

        try
        {
            var result = await _productRepository.DeleteAsync(x => x.Id == id);
            if (result)
            {
                await _productRepository.CommitTransactionAsync();
                Console.WriteLine($"Product: {product.ProductName} deleted successfully.");
            }
            else
            {
                throw new Exception("Error deleting product.");
            }
        }
        catch (Exception ex)
        {
            await _productRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
        private async Task MainMenuDialog()
    {
        Console.Clear();
        Console.WriteLine("Returning to main menu...");
        await Task.Delay(1000);
    }

}
