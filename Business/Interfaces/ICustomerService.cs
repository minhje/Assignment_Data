﻿using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task CreateCustomerAsync(CustomerRegistrationForm form);
    Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
    Task<CustomerModel> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
    Task<CustomerModel> UpdateCustomerAsync(CustomerUpdateForm form);
    Task<bool> DeleteCustomerAsync(int id);
}
