using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using MainApp.ViewModels;
using MainApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MainApp;

public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddDbContext<DataContext>((x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Assignment_Data\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30")));

                services.AddScoped<ICustomerRepository, CustomerRepository>();
                services.AddScoped<CustomerService>();

                services.AddScoped<IStatusRepository, StatusRepository>();
                services.AddScoped<StatusService>();

                services.AddScoped<IProjectRepository, ProjectRepository>();
                services.AddScoped<IProjectService, ProjectService>();
                services.AddSingleton<ProjectModel>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainView>();

                services.AddTransient<ListViewModel>();
                services.AddTransient<ListView>();

                services.AddTransient<EditViewModel>();
                services.AddTransient<EditView>();

                services.AddTransient<AddViewModel>();
                services.AddTransient<AddView>();

                services.AddTransient<DetailsViewModel>();
                services.AddTransient<DetailsView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ListViewModel>();

        var mainWindow = new MainWindow(mainViewModel);
        mainWindow.Show();
    }
}

