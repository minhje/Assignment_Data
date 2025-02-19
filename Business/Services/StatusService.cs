using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;


    // Tog hjälp av ChatGPT 4o för att lägga in Transaction Management
    public async Task<StatusModel> CreateStatusAsync(StatusRegistrationForm form)
    {
        await _statusRepository.BeginTransactionAsync();

        try
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form), "Form cannot be null");
            }

            var statusEntity = StatusFactory.CreateEntity(form);
            var createdStatus = await _statusRepository.CreateAsync(statusEntity);
            await _statusRepository.CommitTransactionAsync();
            return StatusFactory.CreateModel(createdStatus);
        }
        catch (Exception ex)
        {
            await _statusRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<StatusModel>> GetAllStatusesAsync()
    {
        var statusEntities = await _statusRepository.GetAllAsync();
        return statusEntities.Select(StatusFactory.CreateModel);
    }

    public async Task<StatusModel> GetStatusAsync(Expression<Func<StatusEntity, bool>> expression)
    {
        if (expression == null)
        {
            Console.WriteLine("Status not found");
            return null!;
        }
        try
        {
            var statusEntity = await _statusRepository.GetAsync(expression);
            return StatusFactory.CreateModel(statusEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;

        }
    }

    public async Task<StatusModel> UpdateStatusAsync(StatusUpdateForm form)
    {
        await _statusRepository.BeginTransactionAsync();

        try
        {
            var statusEntity = await _statusRepository.GetAsync(x => x.Id == form.Id);
            if (statusEntity == null)
            {
                throw new Exception("Status not found");
            }
            var updatedStatusEntity = StatusFactory.CreateEntity(statusEntity, form);
            await _statusRepository.CommitTransactionAsync();
            return StatusFactory.CreateModel(updatedStatusEntity);
        }
        catch (Exception ex)
        {
            await _statusRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return null!;
        }

    }

    public async Task<bool> DeleteStatusAsync(int id)
    {
        await _statusRepository.BeginTransactionAsync();

        try
        {
            var statusEntity = await _statusRepository.GetAsync(x => x.Id == id);
            if (statusEntity == null)
            {
                throw new Exception("Status not found");
            }

            var result = await _statusRepository.DeleteAsync(x => x.Id == id);
            await _statusRepository.CommitTransactionAsync();
            return result;
        }

        catch (Exception ex)
        {
            await _statusRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}