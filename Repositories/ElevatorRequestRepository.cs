using WebApplicationElevator.Context;
using WebApplicationElevator.Models;

namespace WebApplicationElevator.Repositories;

public class ElevatorRequestRepository : Repository<ElevatorRequest>, IElevatorRequestRepository
{
    private readonly AppDbContext _appDbContext;

    public ElevatorRequestRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }
}
