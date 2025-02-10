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

    public async Task<StatusModel> CreateStatusAsync(StatusRegistrationForm form)
    {
        if (form != null)
        {
            Console.WriteLine("Status already exists");
        }
        try
        {
            var statusEntity = StatusFactory.CreateEntity(form);
            var createdStatus = await _statusRepository.CreateAsync(statusEntity);
            var statusModel = StatusFactory.CreateModel(createdStatus);
            return statusModel;
        }
        catch (Exception ex)
        {
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
        var statusEntity = await _statusRepository.GetAsync(x => x.Id == form.Id);
        if (statusEntity == null)
        {
            throw new Exception("Status not found");
        }
        var updatedStatusEntity = StatusFactory.CreateEntity(statusEntity, form);
        return StatusFactory.CreateModel(updatedStatusEntity);
    }

    public async Task<bool> DeleteStatusAsync(int id)
    {
        var statusEntity = await _statusRepository.GetAsync(x => x.Id == id);
        if (statusEntity == null)
        {
            throw new Exception("Status not found");
        }
        return await _statusRepository.DeleteAsync(x => x.Id == id);
    }

}