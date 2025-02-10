using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;

public partial class EditViewModel(IServiceProvider serviceProvider, IProjectService projectService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IProjectService _projectService = projectService;

    [ObservableProperty]
    private ProjectModel _projectModel = new();

    [RelayCommand]
    private async Task SaveAsync()
    {
        // Generarat av chat GPT 4o för att kunna uppdatera ett projekt
        var updateForm = ConvertToUpdateForm(ProjectModel);
        var result = await _projectService.UpdateProjectAsync(updateForm);
        // Slut på generarad kod

        if (result != null)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
        }
    }

    [RelayCommand]
    private void GoToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
    }

    // Generarat med chat GPT 4o för att kunna uppdatera ett projekt.  
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
