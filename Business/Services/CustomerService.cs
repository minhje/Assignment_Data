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
        var customerEntity = CustomerFactory.CreateEntity(form);
        await _customerRepository.CreateAsync(customerEntity);
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
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == form.Id);
        var updatedCustomerEntity = CustomerFactory.CreateEntity(customerEntity, form);
        return CustomerFactory.CreateModel(updatedCustomerEntity);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        return await _customerRepository.DeleteAsync(x => x.Id == id);
    }

}
