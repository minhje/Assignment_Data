using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, DataContext context) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly DataContext _context = context;

    public async Task<ProjectModel> CreateProjectAsync(ProjectRegistrationForm form)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            var project = ProjectFactory.CreateEntity(form);
            var createdProject = await _projectRepository.CreateAsync(project);
            var projectModel = ProjectFactory.CreateModel(createdProject);

            await _projectRepository.CommitTransactionAsync();
            return projectModel;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    // Generarat av chat GPT 4o för att hämta alla project. 
    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        try
        {
            var projects = await _projectRepository.GetAllAsync();
            var projectModels = projects.Select(ProjectFactory.CreateModel);
            return projectModels;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    // Genererat av Chat GTP 4o för att hämta ett project
    public async Task<ProjectModel> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    { 
        var projectEntity = await _projectRepository.GetAsync(expression);
        if (projectEntity == null)
        {
            return null;
        }

        return new ProjectModel
        {
            Id = projectEntity.Id,
            Title = projectEntity.Title,
            Description = projectEntity.Description,
            StartDate = projectEntity.StartDate,
            EndDate = projectEntity.EndDate,
            CustomerId = projectEntity.CustomerId,
            ProductId = projectEntity.ProductId,
            ManagerId = projectEntity.ManagerId,
            StatusId = projectEntity.StatusId,
            CustomerName = projectEntity.Customer?.CustomerName,
            ManagerName = $"{projectEntity.Manager?.FirstName} {projectEntity.Manager?.LastName}",
            ProductName = projectEntity.Product?.ProductName,
            StatusName = projectEntity.Status?.StatusName
        };
    }



    // Genererat av chatGTP 4o för att kunna uppdatera ett project
    public async Task<ProjectModel> UpdateProjectAsync(ProjectUpdateForm form)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            if (form == null)
            {
                Console.WriteLine("Invalid project update form");
                return null!;
            }

            var existingProject = await _projectRepository.GetAsync(x => x.Id == form.Id);
            if (existingProject == null)
            {
                Console.WriteLine("Project not found");
                return null!;
            }

            existingProject.Title = form.Title;
            existingProject.Description = form.Description;
            existingProject.StartDate = form.StartDate;
            existingProject.EndDate = form.EndDate;
            existingProject.CustomerId = form.CustomerId;
            existingProject.ProductId = form.ProductId;
            existingProject.ManagerId = form.ManagerId;
            existingProject.StatusId = form.StatusId;

            var updatedProject = await _projectRepository.UpdateAsync(x => x.Id == form.Id, existingProject);
            var projectModel = ProjectFactory.CreateModel(updatedProject);

            await _projectRepository.CommitTransactionAsync();
            return projectModel;
        }

        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            var result = await _projectRepository.DeleteAsync(x => x.Id == id);
            await _projectRepository.CommitTransactionAsync();
            return result;

        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    /* Dessa nedanför är genererade av Chat GTP 4o för att kunna ladda in dessa till min WPF applikation. 
     * Metoden hämtar från databasen och skapar en lista av modeller som sedan returneras.*/
    public async Task<IEnumerable<ManagerModel>> GetManagersAsync()
    {
        var managers = await _context.Managers.ToListAsync();
        return managers.Select(m => new ManagerModel
        {
            Id = m.Id,
            FirstName = m.FirstName,
            LastName = m.LastName
        });
    }

    public async Task<IEnumerable<CustomerModel>> GetCustomersAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return customers.Select(c => new CustomerModel
        {
            Id = c.Id,
            CustomerName = c.CustomerName
        });
    }

    public async Task<IEnumerable<ProductModel>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products.Select(p => new ProductModel
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Price = p.Price
        });
    }

    public async Task<IEnumerable<StatusModel>> GetStatusesAsync()
    {
        var statuses = await _context.Statuses.ToListAsync();
        return statuses.Select(s => new StatusModel
        {
            Id = s.Id,
            Status = s.StatusName
        });
    }
}
