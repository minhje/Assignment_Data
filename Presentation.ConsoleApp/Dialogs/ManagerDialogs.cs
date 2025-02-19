using Data.Entities;
using Data.Interfaces;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class ManagerDialogs : IManagerDialogs
{
    private readonly IManagerRepository _managerRepository;

    public ManagerDialogs(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("---------- MANAGER MENU ----------");
            Console.WriteLine("1. Add new manager");
            Console.WriteLine("2. Show all manager");
            Console.WriteLine("3. Show manager details");
            Console.WriteLine("4. Update manager");
            Console.WriteLine("5. Delete manager");
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
        var managerEntity = new ManagerEntity();
        Console.Write("Enter manager firstname: ");
        managerEntity.FirstName = Console.ReadLine()!;
        Console.Write("Enter manager lastname: ");
        managerEntity.LastName = Console.ReadLine()!;

        var result = await _managerRepository.CreateAsync(managerEntity);
        if (result != null)
        {
            Console.WriteLine($"Manager {managerEntity.FirstName + " " + managerEntity.LastName}  created successfully");
        }
        else
        {
            Console.WriteLine("Error creating manager.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task GetAllAsync()
    {
        Console.Clear();
        var managers = await _managerRepository.GetAllAsync();
        if (managers.Any())
        {
            foreach (var manager in managers)
            {
                Console.WriteLine($"Id: {manager.Id} - Name: {manager.FirstName} {manager.LastName}");
            }
        }
        else
        {
            Console.WriteLine("No managers found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task GetAsync()
    {
        Console.Clear();
        Console.Write("Enter manager id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var manager = await _managerRepository.GetAsync(x => x.Id == id);
        if (manager != null)
        {
            Console.WriteLine($"Id: {manager.Id} - Name: {manager.FirstName} {manager.LastName}");
        }
        else
        {
            Console.WriteLine("Manager not found.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateAsync()
    {
        Console.Clear();
        Console.Write("Enter manager id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var manager = await _managerRepository.GetAsync(x => x.Id == id);
        if (manager == null)
        {
            Console.WriteLine("Manager not found.");
            return;
        }
        Console.WriteLine($"Id: {manager.Id} - Name: {manager.FirstName} {manager.LastName}");
        Console.Write("Enter new firstname: ");
        manager.FirstName = Console.ReadLine()!;
        Console.Write("Enter new lastname: ");
        manager.LastName = Console.ReadLine()!;

        var result = await _managerRepository.UpdateAsync(x => x.Id == id, manager);
        if (result != null)
        {
            Console.WriteLine($"Manager {manager.FirstName + manager.LastName} updated successfully");
        }
        else
        {
            Console.WriteLine("Error updating manager.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task DeleteAsync()
    {
        Console.Clear();
        Console.Write("Enter manager id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var manager = await _managerRepository.GetAsync(x => x.Id == id);
        if (manager == null)
        {
            Console.WriteLine("Manager not found.");
            return;
        }
        Console.WriteLine($"Id: {manager.Id}, Name: {manager.FirstName + " " + manager.LastName}");
        Console.WriteLine("Are you sure you want to delete this manager? (y/n)");
        var result = Console.ReadLine()!.ToLower();

        if (result == "y")
        {
            var deleteResult = await _managerRepository.DeleteAsync(x => x.Id == id);
            if (deleteResult)
            {
                Console.WriteLine($"Manager {manager.FirstName + " " + manager.LastName} deleted successfully");
            }
        }
        else
        {
            Console.WriteLine("Error deleting manager.");
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
