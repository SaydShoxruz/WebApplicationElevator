using WebApplicationElevator.DTOs;
using WebApplicationElevator.Models;

namespace WebApplicationElevator.Service;

public interface IAppService
{
    public IEnumerable<ElevatorRequest> GetElevatorRequests();
    public Task<bool> CallTheElevatorAsync(ElevatorRequestDTO requestDTO);
    public int GetCurrentFloor();
}