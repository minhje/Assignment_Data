using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IManagerService
{
    Task<ManagerModel> CreateManagerAsync(ManagerRegistrationForm form);
    Task<IEnumerable<ManagerModel>> GetAllManagersAsync();
    Task<ManagerModel> GetManagerAsync(Expression<Func<ManagerEntity, bool>> expression);
    Task<ManagerModel> UpdateManagerAsync(ManagerUpdateForm form);
    Task<bool> DeleteManagerAsync(int id);
    Task<bool> CheckIfManagerExistsAsync(Expression<Func<ManagerEntity, bool>> expression);
}
