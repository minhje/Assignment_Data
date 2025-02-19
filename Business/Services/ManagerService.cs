using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class ManagerService(IManagerRepository managerRepository) : IManagerService
{
    private readonly IManagerRepository _managerRepository = managerRepository;

    public async Task CreateManagerAsync(ManagerRegistrationForm form)
    {
        await _managerRepository.BeginTransactionAsync();

        try
        {
            var managerEntity = ManagerFactory.CreateEntity(form);
            await _managerRepository.CreateAsync(managerEntity);
            await _managerRepository.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _managerRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<IEnumerable<ManagerModel>> GetAllManagersAsync()
    {
        var managerEntities = await _managerRepository.GetAllAsync();
        return managerEntities.Select(ManagerFactory.CreateModel);
    }

    public async Task<ManagerModel> GetManagerAsync(Expression<Func<ManagerEntity, bool>> expression)
    {
        if (expression == null)
        {
            Console.WriteLine("Manager not found");
            return null!;
        }
        try
        {
            var managerEntity = await _managerRepository.GetAsync(expression);
            return ManagerFactory.CreateModel(managerEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;

        }
    }

    public async Task<ManagerModel> UpdateManagerAsync(ManagerUpdateForm form)
    {
        await _managerRepository.BeginTransactionAsync();

        try
        {
            var managerEntity = await _managerRepository.GetAsync(x => x.Id == form.Id);
            if (managerEntity == null)
            {
                throw new Exception("Manager not found");
            }
            var updatedManagerEntity = ManagerFactory.CreateEntity(managerEntity, form);
            await _managerRepository.CommitTransactionAsync();
            return ManagerFactory.CreateModel(updatedManagerEntity);
        }
        catch (Exception ex)
        {
            await _managerRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    // Tog hjälp av ChatGPT 4o för att få hjälp att implementera Transaction Management
    public async Task<bool> DeleteManagerAsync(int id)
    {
        await _managerRepository.BeginTransactionAsync();

        try
        {
            var managerEntity = await _managerRepository.GetAsync(x => x.Id == id);
            if (managerEntity == null)
            {
            throw new Exception("Manager not found");
            }

            var result = await _managerRepository.DeleteAsync(x => x.Id == id);
            await _managerRepository.CommitTransactionAsync();
            return result;
        }

        catch (Exception ex)
        {
            await _managerRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}
