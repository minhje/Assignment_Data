using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services;

public class ManagerService(IManagerRepository managerRepository) 
{
    private readonly IManagerRepository _managerRepository = managerRepository;
}
