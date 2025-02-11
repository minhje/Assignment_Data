using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    // Kod genererad av Chat GPT 4o för att kunna hämta manager, customer för ett projekt
    public async Task<ManagerEntity?> GetManagerAsync(int managerId)
    {
        return await _context.Set<ManagerEntity>().FindAsync(managerId);
    }

    public async Task<CustomerEntity?> GetCustomerAsync(int customerId)
    {
        return await _context.Set<CustomerEntity>().FindAsync(customerId);
    }

    public async Task<ProductEntity?> GetProductAsync(int productId)
    {
        return await _context.Set<ProductEntity>().FindAsync(productId);
    }

}
