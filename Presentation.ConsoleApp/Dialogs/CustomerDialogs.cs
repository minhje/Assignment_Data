using Business.Dtos;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
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
            Console.WriteLine("----------------------------");
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Add new customer");
            Console.WriteLine("2. Show all customers");
            Console.WriteLine("3. Show customer details");
            Console.WriteLine("4. Update customer");
            Console.WriteLine("5. Delete customer");
            Console.WriteLine("6. Exit application");
            Console.WriteLine("----------------------------");

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
                    return;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        }
    }

    private async Task CreateAsync()
    {
        Console.Clear();
        var customerEntity = new CustomerEntity();
        Console.WriteLine("Enter customer name:");
        customerEntity.CustomerName = Console.ReadLine()!;

        var result = await _customerRepository.CreateAsync(customerEntity);
        if (result != null)
        {
            Console.WriteLine($"Customer: {customerEntity.CustomerName} has been added.");
        }
        else
        {
            Console.WriteLine("Error creating customer.");
            return;
        }
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
        Console.WriteLine("Enter customer id:");
        var id = Convert.ToInt32(Console.ReadLine());
        var customer = await _customerRepository.GetAsync(x => x.Id == id);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }
        Console.WriteLine($"Id: {customer.Id}, Name: {customer.CustomerName}");
        Console.WriteLine("Do you want to delete this customer? (y/n)");
        var choice = Console.ReadLine();
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
        Console.ReadKey();
    }

    private async Task UpdateAsync()
    {
        Console.Clear();
        Console.WriteLine("Enter customer id:");
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
        Console.ReadKey();
    }

    private async Task DeleteAsync()
    {
        Console.Clear();
        await GetAllAsync();

        Console.Write("Enter customer id:");
        var id = Convert.ToInt32(Console.ReadLine());
        var customer = await _customerRepository.GetAsync(x => x.Id == id);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }
        Console.WriteLine($"Id: {customer.Id}, Name: {customer.CustomerName}");
        Console.WriteLine("Do you want to delete this customer? (y/n)");
        var choice = Console.ReadLine();
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
        Console.ReadKey();
    }

}
