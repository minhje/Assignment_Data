using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.Dialogs;
using Presentation.ConsoleApp.Interfaces;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Assignment_Data\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30"))
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IStatusRepository, StatusRepository>()
    .AddScoped<IManagerRepository, ManagerRepository>()
    .AddScoped<IProjectRepository, ProjectRepository>()

    .AddScoped<IMainMenuDialog, MainMenuDialog>()
    .AddScoped<ICustomerDialogs, CustomerDialogs>()
    .AddScoped<IProductDialogs, ProductDialogs>()
    .AddScoped<IManagerDialogs, ManagerDialogs>()
    .AddScoped<IProjectDialogs, ProjectDialogs>()
    .AddScoped<IStatusDialogs, StatusDialogs>();

var serviceProvider = services.BuildServiceProvider();
var customerDialogs = serviceProvider.GetRequiredService<ICustomerDialogs>();
var productDialogs = serviceProvider.GetRequiredService<IProductDialogs>();
var managerDialogs = serviceProvider.GetRequiredService<IManagerDialogs>();
var projectDialogs = serviceProvider.GetRequiredService<IProjectDialogs>();
var mainMenuDialog = serviceProvider.GetRequiredService<IMainMenuDialog>();

await mainMenuDialog.MainMenu();

