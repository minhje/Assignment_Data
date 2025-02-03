using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ManagerRepository(DataContext context) : BaseRepository<ManagerEntity>(context), IManagerRepository
{
}
