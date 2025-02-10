using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;

public partial class AddViewModel(IProjectService projectService, IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IProjectService _projectService = projectService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private ProjectRegistrationForm _projectRegistrationForm = new();

    [RelayCommand]
    private async Task SaveAsync()
    {
        var result = await _projectService.CreateProjectAsync(ProjectRegistrationForm);
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
}
