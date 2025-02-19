using Data.Entities;
using Data.Interfaces;
using Presentation.ConsoleApp.Dialogs;
using Presentation.ConsoleApp.Interfaces;

public class CustomerDialogs : ICustomerDialogs
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerDialogs(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("---------- CUSTOMER MENU ----------");
            Console.WriteLine("1. Add new customer");
            Console.WriteLine("2. Show all customers");
            Console.WriteLine("3. Show customer details");
            Console.WriteLine("4. Update customer");
            Console.WriteLine("5. Delete customer");
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
        var customerEntity = new CustomerEntity();
        Console.WriteLine("---------- ADD NEW CUSTOMER ----------");
        Console.Write("Enter customer name: ");
        customerEntity.CustomerName = Console.ReadLine()!;

        var result = await _customerRepository.CreateAsync(customerEntity);
        if (result != null)
        {
            Console.WriteLine($"Customer: {customerEntity.CustomerName} created successfully");
        }
        else
        {
            Console.WriteLine("Error creating customer.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

    }

    private async Task GetAllAsync()
    {
        Console.Clear();
        var customers = await _customerRepository.GetAllAsync();
        if (customers == null)
        {
            Console.WriteLine("No customers found.");
            return;
        }
        foreach (var customer in customers)
        {
            Console.WriteLine($"Id: {customer.Id}, Name: {customer.CustomerName}");
        }
        Console.ReadKey();
    }

    private async Task GetAsync()
    {
        Console.Clear();
        Console.Write("Enter customer id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var customer = await _customerRepository.GetAsync(x => x.Id == id);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }
        Console.WriteLine($"Id: {customer.Id}, Name: {customer.CustomerName}");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateAsync()
    {
        Console.Clear();
        Console.Write("Enter customer id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var customer = await _customerRepository.GetAsync(x => x.Id == id);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }
        Console.WriteLine($"Id: {customer.Id}, Name: {customer.CustomerName}");
        Console.WriteLine("Enter new customer name:");
        customer.CustomerName = Console.ReadLine()!;
        var result = await _customerRepository.UpdateAsync(x => x.Id == id, customer);
        if (result != null)
        {
            Console.WriteLine("Customer has been updated.");
        }
        else
        {
            Console.WriteLine("Error updating customer.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task DeleteAsync()
    {
        Console.Clear();

        Console.Write("Enter customer id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var customer = await _customerRepository.GetAsync(x => x.Id == id);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }
        Console.WriteLine($"Id: {customer.Id}, Name: {customer.CustomerName}");
        Console.WriteLine("Do you want to delete this customer? (y/n)");
        var choice = Console.ReadLine()!.ToLower();
        if (choice == "y")
        {
            var result = await _customerRepository.DeleteAsync(x => x.Id == id);
            if (result)
            {
                Console.WriteLine("Customer has been deleted.");
            }
            else
            {
                Console.WriteLine("Error deleting customer.");
            }
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
