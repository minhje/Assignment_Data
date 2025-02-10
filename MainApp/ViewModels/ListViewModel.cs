using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;


namespace MainApp.ViewModels;

public partial class ListViewModel : ObservableObject
{
    private readonly IProjectService _projectService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableCollection<ProjectModel> _projects;

    public ListViewModel(IProjectService projectService, IServiceProvider serviceProvider)
    {
        _projectService = projectService;
        _serviceProvider = serviceProvider;
        _projects = new ObservableCollection<ProjectModel>();
        Task.Run(async () => await GetAllProjectsAsync());
    }

    private async Task GetAllProjectsAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        Projects.Clear();
        foreach (var project in projects)
        {
            Projects.Add(project);
        }
    }

    [RelayCommand]
    private async Task DeleteProjectAsync(ProjectModel project)
    {
        await _projectService.DeleteProjectAsync(project.Id);
        await GetAllProjectsAsync();
    }

    [RelayCommand]
    private void GoToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddViewModel>();
    }

    [RelayCommand]
    private void GoToDetailsView(ProjectModel projectModel)
    {
        var detailsViewModel = _serviceProvider.GetRequiredService<DetailsViewModel>();
        detailsViewModel.ProjectModel = projectModel;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = detailsViewModel;
    }
}
