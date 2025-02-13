using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MainApp.ViewModels;

public partial class EditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    [ObservableProperty]
    private ProjectUpdateForm _projectUpdateForm = new()
     {
        StartDate = DateTime.Now,
        EndDate = DateTime.Now
    };

/* Genererat av Chat GPT 4o för att kunna hämta in dessa till ComboBox i WPF applikationen*/
[ObservableProperty]
private ObservableCollection<ManagerModel> _managers = new();

[ObservableProperty]
private ObservableCollection<CustomerModel> _customers = new();

[ObservableProperty]
private ObservableCollection<ProductModel> _products = new();

[ObservableProperty]
private ObservableCollection<StatusModel> _statuses = new();

[ObservableProperty]
private int _selectedManagerId;
/* Slut genererad kod */

public EditViewModel(IProjectService projectService, IServiceProvider serviceProvider)
{
    _projectService = projectService;
    _serviceProvider = serviceProvider;
    LoadDataAsync().ConfigureAwait(false);
}

[RelayCommand]
    private async Task SaveAsync()
    {
        // Generarat av chat GPT 4o för att kunna uppdatera ett projekt
        var updateForm = ConvertToUpdateForm(ProjectUpdateForm);
        var result = await _projectService.UpdateProjectAsync(updateForm);
        // Slut på generarad kod

        if (result != null)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
    }

    // Generarat med chat GPT 4o för att kunna uppdatera ett projekt.  
    private ProjectUpdateForm ConvertToUpdateForm(ProjectUpdateForm model)
    {
        return new ProjectUpdateForm
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            CustomerId = model.CustomerId,
            ProductId = model.ProductId,
            ManagerId = model.ManagerId,
            StatusId = model.StatusId
        };
    }

    /* Generarat av Chat GPT 4o */
    private async Task LoadDataAsync()
    {
        var managers = await _projectService.GetManagersAsync();
        Managers = new ObservableCollection<ManagerModel>(managers);

        var customers = await _projectService.GetCustomersAsync();
        Customers = new ObservableCollection<CustomerModel>(customers);

        var products = await _projectService.GetProductsAsync();
        Products = new ObservableCollection<ProductModel>(products);

        var statuses = await _projectService.GetStatusesAsync();
        Statuses = new ObservableCollection<StatusModel>(statuses);
    }
}
