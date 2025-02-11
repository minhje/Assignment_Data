using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class MainMenuDialog(ICustomerDialogs customerDialogs, IProjectDialogs projectDialogs, IProductDialogs productDialogs, IManagerDialogs managerDialogs, IStatusDialogs statusDialogs) : IMainMenuDialog
{
    public async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-------- MAIN MENU --------");
            Console.WriteLine("1. Customer options");
            Console.WriteLine("2. Project options");
            Console.WriteLine("3. Product options");
            Console.WriteLine("4. Manager options");
            Console.WriteLine("5. Status options");
            Console.WriteLine("6. Exit application");
            Console.WriteLine("---------------------------");

            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await customerDialogs.MenuOptions();
                    break;

                case "2":
                    await projectDialogs.MenuOptions();
                    break;

                case "3":
                    await productDialogs.MenuOptions();
                    break;

                case "4":
                    await managerDialogs.MenuOptions();
                    break;

                case "5":
                    await statusDialogs.MenuOptions();
                    break;


                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}
