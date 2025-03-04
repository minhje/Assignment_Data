﻿using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace MainApp.ViewModels;

public partial class EditViewModel : ObservableObject
{
    private readonly IProjectService _projectService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ProjectUpdateForm _projectUpdateForm;

    /* Genererat av Chat GPT 4o för att kunna hämta in dessa till ComboBox i WPF applikationen*/
    [ObservableProperty]
    private ObservableCollection<ManagerModel> _managers = new();

    [ObservableProperty]
    private ObservableCollection<CustomerModel> _customers = new();

    [ObservableProperty]
    private ObservableCollection<ProductModel> _products = new();

    [ObservableProperty]
    private ObservableCollection<StatusModel> _statuses = new();
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
        try
        {
            var result = await _projectService.UpdateProjectAsync(ProjectUpdateForm);
            if (result != null)
            {
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
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
