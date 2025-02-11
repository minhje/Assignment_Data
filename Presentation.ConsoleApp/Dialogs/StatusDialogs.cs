using Data.Entities;
using Data.Interfaces;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class StatusDialogs : IStatusDialogs
{
    private readonly IStatusRepository _statusRepository;

    public StatusDialogs(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }
    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("---------- STATUS MENU ----------");
            Console.WriteLine("1. Add new status");
            Console.WriteLine("2. Show all statuses");
            Console.WriteLine("3. Show status details");
            Console.WriteLine("4. Update status");
            Console.WriteLine("5. Delete status");
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
        var statusEntity = new StatusEntity();
        Console.WriteLine("---------- ADD NEW STATUS ----------");
        Console.Write("Enter status name: ");
        statusEntity.StatusName = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(statusEntity.StatusName))
        {
            Console.WriteLine("Status name cannot be empty.");
            return;
        }

        var result = await _statusRepository.CreateAsync(statusEntity);
        if (result != null)
        {
            Console.WriteLine("Status added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add status.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task GetAllAsync()
    {
        Console.Clear();
        var statuses = await _statusRepository.GetAllAsync();
        if (statuses == null)
        {
            Console.WriteLine("No statuses found.");
        }
        else
        {
            foreach (var status in statuses)
            {
                Console.WriteLine($"Id: {status.Id}, Name: {status.StatusName}");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task GetAsync()
    {
        Console.Clear();
        Console.Write("Enter status id: ");
        var id = int.Parse(Console.ReadLine()!);
        var status = await _statusRepository.GetAsync(x => x.Id == id);
        if (status == null)
        {
            Console.WriteLine("Status not found.");
        }
        else
        {
            Console.WriteLine($"Id: {status.Id}, Name: {status.StatusName}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateAsync()
    {
        Console.Clear();
        Console.Write("Enter status id: ");
        var id = int.Parse(Console.ReadLine()!);
        var status = await _statusRepository.GetAsync(x => x.Id == id);
        if (status == null)
        {
            Console.WriteLine("Status not found.");
            return;
        }

        Console.Write("Enter new status name: ");
        status.StatusName = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(status.StatusName))
        {
            Console.WriteLine("Status name cannot be empty.");
            return;
        }
        var result = await _statusRepository.UpdateAsync(x => x.Id == id, status);
        if (result != null)
        {
            Console.WriteLine("Status updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to update status.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task DeleteAsync()
    {
        Console.Clear();
        Console.Write("Enter status id: ");
        var id = int.Parse(Console.ReadLine()!);
        var status = await _statusRepository.GetAsync(x => x.Id == id);
        if (status == null)
        {
            Console.WriteLine("Status not found.");
            return;
        }
        var result = await _statusRepository.DeleteAsync(x => x.Id == id);
        if (result)
        {
            Console.WriteLine("Status deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to delete status.");
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
