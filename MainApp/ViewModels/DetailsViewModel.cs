using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;

public partial class DetailsViewModel(IServiceProvider serviceProvider, IProjectService projectService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IProjectService _projectService = projectService;

    [ObservableProperty]
    private ProjectModel _projectModel = new();

    [ObservableProperty]
    private IEnumerable<StatusModel> _statuses = new List<StatusModel>();

    [ObservableProperty]
    private IEnumerable<CustomerModel> _customers = new List<CustomerModel>();

    [ObservableProperty]
    private IEnumerable<ManagerModel> _managers = new List<ManagerModel>();

    [ObservableProperty]
    private IEnumerable<ProductModel> _products = new List<ProductModel>();
    public string ProductNamePrice => $"{ProjectModel.ProductName} - {ProjectModel.ProductPrice:C}/h"; // Genererat med ChatGPT 4o för att kunna visa Produktnamn & pris tillsammans

    public async Task GetProjectAsync(int projectId)
    {
        ProjectModel = await _projectService.GetProjectAsync(p => p.Id == projectId);
        if (ProjectModel == null)
        {
            return;
        }

        Statuses = await _projectService.GetStatusesAsync();
        Customers = await _projectService.GetCustomersAsync();
        Managers = await _projectService.GetManagersAsync();
        Products = await _projectService.GetProductsAsync();

        // Sätter namn på status, kund, manager och product. Genererat med ChatGPT 4o
        ProjectModel.StatusName = Statuses.FirstOrDefault(s => s.Id == ProjectModel.StatusId)?.Status;
        ProjectModel.CustomerName = Customers.FirstOrDefault(c => c.Id == ProjectModel.CustomerId)?.CustomerName;
        ProjectModel.ManagerName = Managers.FirstOrDefault(m => m.Id == ProjectModel.ManagerId)?.DisplayName;
        var product = Products.FirstOrDefault(p => p.Id == ProjectModel.ProductId);
        ProjectModel.ProductName = product?.ProductName;
        ProjectModel.ProductPrice = product?.Price ?? 0;

        OnPropertyChanged(nameof(ProductNamePrice)); // Genererat med ChatGPT 4o för att kunna visa Produktnamn & pris tillsammans
    }

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

    private ProjectUpdateForm ConvertToUpdateForm(ProjectModel model)
    {
        return new ProjectUpdateForm
        {
            Id = model.Id,
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
