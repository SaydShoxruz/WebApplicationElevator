using WebApplicationElevator.Context;
using WebApplicationElevator.Models;

namespace WebApplicationElevator.Repositories;

public class ElevatorStateRepository : Repository<ElevatorState>, IElevatorStateRepository
{
    private readonly AppDbContext _appDbContext;
    public ElevatorStateRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }
}
