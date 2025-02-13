using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace MainApp.ViewModels;

public partial class DetailsViewModel(IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private ProjectModel _projectModel = new();

    [RelayCommand]
    private void GoToEditView()
    {
        var editViewModel = _serviceProvider.GetRequiredService<EditViewModel>();
        editViewModel.ProjectUpdateForm = ConvertToUpdateForm(ProjectModel);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = editViewModel;
    }

    [RelayCommand]
    private void GoToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        var listViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
        mainViewModel.CurrentViewModel = listViewModel;
    }

    // Genererat av ChatGPT för att kunna uppdatera ett project. 
    private ProjectUpdateForm ConvertToUpdateForm(ProjectModel model)
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
}
