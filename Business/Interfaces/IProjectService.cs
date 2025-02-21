using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<ProjectModel> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
    Task<ProjectModel> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<ProjectModel> UpdateProjectAsync(ProjectUpdateForm form);
    Task<bool> DeleteProjectAsync(int id);


    /* Genererat av Chat GPT 4o för att kunna ladda in modellerna för ComboBox i min WPF applikation  */
    Task<IEnumerable<ManagerModel>> GetManagersAsync();
    Task<IEnumerable<CustomerModel>> GetCustomersAsync();
    Task<IEnumerable<ProductModel>> GetProductsAsync();
    Task<IEnumerable<StatusModel>> GetStatusesAsync();


    // Genererat av chat GPT 4o för att kunna visa i WPF presentationen vilket ID som projektet kommer att få. 
    Task<int> GetNextProjectIdAsync(); // New method to get the next available project ID
}
