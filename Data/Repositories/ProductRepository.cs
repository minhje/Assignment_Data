using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ProductRepository(DataContext context) : BaseRepository<ProductEntity>(context), IProductRepository
{
}


    /*
    Behövs för att skriva över metoder
    private readonly DataContext _context; 
    
    EXEMPEL PÅ OVERRIDE
    public override async Task<IEnumerable<ServiceEntity>> GetAllAsync()
    {
       var entities = await _context.Services.Include(x => x.Status).ToListAsync();
        return entities;
    }
    */
