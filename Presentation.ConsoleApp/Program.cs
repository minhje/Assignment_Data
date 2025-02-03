using Business.Dtos;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.Interfaces;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Assignment_Data\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30"))
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IStatusRepository, StatusRepository>()
    .AddScoped<IManagerRepository, ManagerRepository>()
    .AddScoped<IProjectRepository, ProjectRepository>()
    .AddScoped<ICustomerDialogs, CustomerDialogs>();

var serviceProvider = services.BuildServiceProvider();
var customerDialogs = serviceProvider.GetRequiredService<ICustomerDialogs>();

await customerDialogs.MenuOptions();
