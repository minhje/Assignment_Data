using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    // Genererat av Chat GPT 4o för kunna hämta manager och customer för ett projekt
    Task<ManagerEntity?> GetManagerAsync(int managerId);
    Task<CustomerEntity?> GetCustomerAsync(int customerId);
    Task<ProductEntity?> GetProductAsync(int productId);
}

