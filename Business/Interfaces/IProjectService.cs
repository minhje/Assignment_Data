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
    Task<bool> CheckIfProjectExistsAsync(Expression<Func<ProjectEntity, bool>> expression);
}
