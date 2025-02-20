using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using MainApp.ViewModels;
using MainApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

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
        /* Genererat av Chat GPT 4o för att lösa ett problem med att jag fick två olika valutor i min presentation. 
         * Sätter culture till sverige för att få svensk valuta. */
        var culture = new CultureInfo("sv-SE");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        // Försäkrar att hela applikationen använder sig av svenska språket och valutan
        FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement), 
            new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        // Slut genererad kod

        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ListViewModel>();

        var mainWindow = new MainWindow(mainViewModel);
        mainWindow.Show();
    }
}

