using Data.Entities;
using Data.Interfaces;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class ProjectDialogs : IProjectDialogs
{
    private readonly IProjectRepository _projectRepository;

    public ProjectDialogs(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("---------- PROJECT MENU ----------");
            Console.WriteLine("1. Add new project");
            Console.WriteLine("2. Show all projects");
            Console.WriteLine("3. Show project details");
            Console.WriteLine("4. Update project");
            Console.WriteLine("5. Delete project");
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
        var projectEntity = new ProjectEntity();

        Console.WriteLine("---------- ADD NEW PROJECT ----------");
        Console.Write("Enter project name: ");
        projectEntity.Title = Console.ReadLine()!;
        Console.Write("Enter project description: ");
        projectEntity.Description = Console.ReadLine();
        /* - START - Kod genererad av Chat GPT 4o för att få in datumet korrekt */
        Console.Write("Enter project start date (yyyy-MM-dd): ");
        projectEntity.StartDate = DateTime.ParseExact(Console.ReadLine()!, "yyyy-MM-dd", null);
        Console.Write("Enter project end date (yyyy-MM-dd): ");
        projectEntity.EndDate = DateTime.ParseExact(Console.ReadLine()!, "yyyy-MM-dd", null);
        /* - SLUT - */


        // While loop genererad av Chat GPT 4o för att kunna ange status smidigt. 
        while (true)
        {
            Console.WriteLine("Enter status: ");
            Console.WriteLine("1. Not started");
            Console.WriteLine("2. Ongoing");
            Console.WriteLine("3. Completed");
            Console.Write("Choose an option: ");
            var status = Console.ReadLine();
            if (status == "1" || status == "2" || status == "3")
            {
                projectEntity.StatusId = Convert.ToInt32(status);
                break;
            }
            else
            {
                Console.WriteLine("Invalid option, try again.");
            }
        }

        /* - START - Kod genererad av chat GPT 4o, tilldelar manager & customer efter ID samt kontrollerar om dessa hittas, annars fås ett nytt försök */
        while (true)
        {
            Console.Write("Enter project manager ID: ");
            var managerId = int.Parse(Console.ReadLine()!);
            var manager = await _projectRepository.GetManagerAsync(managerId);
            if (manager == null)
            {
                Console.WriteLine("Manager not found. Do you want to try again? (y/n): ");
                var retry = Console.ReadLine()!.ToLower();
                if (retry != "y")
                {
                    return;
                }
            }
            else
            {
                projectEntity.ManagerId = managerId;
                projectEntity.Manager = manager;
                break;
            }
        }

        while (true)
        {
            Console.Write("Enter project customer ID: ");
            var customerId = int.Parse(Console.ReadLine()!);
            var customer = await _projectRepository.GetCustomerAsync(customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found. Do you want to try again? (y/n): ");
                var retry = Console.ReadLine()!.ToLower();
                if (retry != "y")
                {
                    return;
                }
            }
            else
            {
                projectEntity.CustomerId = customerId;
                projectEntity.Customer = customer;
                break;
            }
        }
        // - SLUT -

        while (true)
        {
            Console.Write("Enter project product ID: ");
            var productId = int.Parse(Console.ReadLine()!);
            var product = await _projectRepository.GetProductAsync(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found. Do you want to try again? (y/n): ");
                var retry = Console.ReadLine()!.ToLower();
                if (retry != "y")
                {
                    return;
                }
            }
            else
            {
                projectEntity.ProductId = productId;
                projectEntity.Product = product;
                break;
            }
        }


        try
        {
            var project = await _projectRepository.CreateAsync(projectEntity);

            if (project == null)
            {
                Console.WriteLine("Error adding project.");
            }
            else
            {
                Console.WriteLine($"Project: {projectEntity.Title} added successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task GetAllAsync()
    {
        Console.Clear();
        var projects = await _projectRepository.GetAllAsync();
        if (projects == null)
        {
            Console.WriteLine("No projects found.");
        }
        else
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"Project ID: {project.Id}");
                Console.WriteLine($"Project name: {project.Title}");
                Console.WriteLine($"Project description: {project.Description}");
                Console.WriteLine($"Project start date: {project.StartDate}");
                Console.WriteLine($"Project end date: {project.EndDate}");
                Console.WriteLine($"Project status: {project.StatusId}");
                Console.WriteLine("-----------------------------------");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task GetAsync()
    {
        Console.Clear();
        Console.Write("Enter project id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
        {
            Console.WriteLine("Project not found.");
        }
        else
        {
            Console.WriteLine($"Project ID: {project.Id}");
            Console.WriteLine($"Project name: {project.Title}");
            Console.WriteLine($"Project description: {project.Description}");
            Console.WriteLine($"Project start date: {project.StartDate}");
            Console.WriteLine($"Project end date: {project.EndDate}");
            Console.WriteLine($"Project status: {project.StatusId}");
            Console.WriteLine($"Project customer: {project.CustomerId}");
            Console.WriteLine($"Project product: {project.ProductId}");
            Console.WriteLine($"Project manager: {project.ManagerId}");


        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateAsync()
    {
        Console.Clear();
        Console.Write("Enter project id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
        {
            Console.WriteLine("Project not found.");
            return;
        }
        else
        {
            Console.WriteLine("---------- UPDATE PROJECT ----------");
            Console.Write("Enter project name: ");
            project.Title = Console.ReadLine()!;
            Console.Write("Enter project description: ");
            project.Description = Console.ReadLine();
            Console.Write("Enter project start date: ");
            project.StartDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter project end date: ");
            project.EndDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter status: ");
            Console.WriteLine("1. Not started");
            Console.WriteLine("2. Ongoing");
            Console.WriteLine("3. Completed");
            Console.Write("Choose an option: ");
            var status = Console.ReadLine();
            project.StatusId = Convert.ToInt32(status);
            var result = await _projectRepository.UpdateAsync(x => x.Id == id, project);
            if (result != null)
            {
                Console.WriteLine($"Project {project.Title} updated successfully.");
            }
            else
            {
                Console.WriteLine("Error updating project.");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task DeleteAsync()
    {
        Console.Clear();
        Console.Write("Enter project id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
        {
            Console.WriteLine("Project not found.");
        }

        Console.WriteLine($"Project ID: {project.Id}");
        Console.WriteLine($"Project name: {project.Title}");
        Console.WriteLine($"Project description: {project.Description}");
        Console.WriteLine($"Project start date: {project.StartDate}");
        Console.WriteLine($"Project end date: {project.EndDate}");
        Console.WriteLine($"Project status: {project.Status}");
        Console.Write("Are you sure you want to delete this project? (y/n): ");

        var choice = Console.ReadLine()!.ToLower();
        if (choice == "y")
        {
            var result = await _projectRepository.DeleteAsync(x => x.Id == id);
            if (result)
            {
                Console.WriteLine("Project deleted successfully.");
            }
            else
            {
                Console.WriteLine("Error deleting project.");
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
