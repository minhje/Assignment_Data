using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class ManagerService(IManagerRepository managerRepository) : IManagerService
{
    private readonly IManagerRepository _managerRepository = managerRepository;

    public async Task CreateManagerAsync(ManagerRegistrationForm form)
    {
        var managerEntity = ManagerFactory.CreateEntity(form);
        await _managerRepository.CreateAsync(managerEntity);
    }

    public async Task<IEnumerable<ManagerModel>> GetAllManagersAsync()
    {
        var managerEntities = await _managerRepository.GetAllAsync();
        return managerEntities.Select(ManagerFactory.CreateModel);
    }

    public async Task<ManagerModel> GetManagerAsync(Expression<Func<ManagerEntity, bool>> expression)
    {
        var managerEntity = await _managerRepository.GetAsync(expression);
        return ManagerFactory.CreateModel(managerEntity!);
    }

    public async Task<ManagerModel> UpdateManagerAsync(ManagerUpdateForm form)
    {
        var managerEntity = await _managerRepository.GetAsync(x => x.Id == form.Id);
        if (managerEntity == null)
        {
            throw new Exception("Manager not found");
        }
        var updatedManagerEntity = ManagerFactory.CreateEntity(managerEntity, form);
        return ManagerFactory.CreateModel(updatedManagerEntity);
    }
    public async Task<bool> DeleteManagerAsync(int id)
    {
        var managerEntity = await _managerRepository.GetAsync(x => x.Id == id);
        if (managerEntity == null)
        {
            throw new Exception("Manager not found");
        }

        return await _managerRepository.DeleteAsync(x => x.Id == id); // Generarat av chat GPT 4o för att kunna radera en manager
    }

}
