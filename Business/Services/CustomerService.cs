using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository) : ICustomerService
{
    private readonly CustomerRepository _customerRepository = customerRepository;


    public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    {
        await _customerRepository.BeginTransactionAsync();

        try
        { 
            var customerEntity = CustomerFactory.CreateEntity(form);
            await _customerRepository.CreateAsync(customerEntity);

            await _customerRepository.CommitTransactionAsync();
        }

        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
        }
    }
    public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAllAsync();
        return customerEntities.Select(CustomerFactory.CreateModel);
    }

    public async Task<CustomerModel> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var customerEntity = await _customerRepository.GetAsync(expression);
        return CustomerFactory.CreateModel(customerEntity!);
    }

    public async Task<CustomerModel> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        await _customerRepository.BeginTransactionAsync();
        try
        {
            var customerEntity = await _customerRepository.GetAsync(x => x.Id == form.Id);
            var updatedCustomerEntity = CustomerFactory.CreateEntity(customerEntity, form);
            var result = await _customerRepository.UpdateAsync(x => x.Id == form.Id, updatedCustomerEntity);

            await _customerRepository.CommitTransactionAsync();
            return CustomerFactory.CreateModel(result);

        }

        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        await _customerRepository.BeginTransactionAsync();

        try
        {
            var result = await _customerRepository.DeleteAsync(x => x.Id == id);
            await _customerRepository.CommitTransactionAsync();
            return result;

        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}
