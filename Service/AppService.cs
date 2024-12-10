using WebApplicationElevator.DTOs;
using WebApplicationElevator.Models;
using WebApplicationElevator.Repositories;

namespace WebApplicationElevator.Service;

public class AppService : IAppService
{
    private static PriorityQueue<ElevatorRequest, int> priorityQueueRequests = new PriorityQueue<ElevatorRequest, int>();
    public static ElevatorState elevatorState = new ElevatorState();
    private static readonly object LockObject = new object();
    private bool isMove = false;

    public readonly IElevatorRequestRepository _elevatorRequestRepository;
    public readonly IElevatorStateRepository _elevatorStateRepository;

    public AppService(IElevatorRequestRepository elevatorRequestRepository,
        IElevatorStateRepository elevatorStateRepository)
    {
        _elevatorRequestRepository = elevatorRequestRepository;
        _elevatorStateRepository = elevatorStateRepository;
    }

    public async Task<bool> CallTheElevatorAsync(ElevatorRequestDTO requestDTO)
    {
        if (!RequestVerification(requestDTO))
        {
            return false;
        }

        ElevatorRequest request = new ElevatorRequest();

        request.CurrentFloor = requestDTO.CurrentFloor;
        request.TargetFloor = requestDTO.TargetFloor;

        priorityQueueRequests.Enqueue(request, Math.Abs(elevatorState.TargetFloor - request.CurrentFloor));

        var elRequest = new ElevatorRequest();
        if (!isMove)
        {
            isMove = true;
            await Processing();
        }

        return true;
    }

    private async Task Processing()
    {
        ElevatorRequest elRequest;
        var requests = priorityQueueRequests;
        var targetFloor = elevatorState.TargetFloor;

        requests.TryDequeue(out var req, out int priority);

        lock (LockObject)
        {

            elevatorState.CurrentFloor = req!.CurrentFloor;
            elevatorState.TargetFloor = req.TargetFloor;

            if (elevatorState.CurrentFloor < elevatorState.TargetFloor)
            {
                elevatorState.Direction = "Up";
            }
            else
            {
                elevatorState.Direction = "Down";
            }
        }

        await _elevatorStateRepository.CreateAsync(elevatorState);
        await _elevatorRequestRepository.CreateAsync(req);

        lock (LockObject)
        {
            while (elevatorState.CurrentFloor != elevatorState.TargetFloor)
            {
                Thread.Sleep(2000);
                if (elevatorState.Direction == "Up")
                {
                    elevatorState.CurrentFloor++;
                }
                else
                {
                    elevatorState.CurrentFloor--;
                }
            }
        }

        priorityQueueRequests = null;
        while (requests.TryDequeue(out var req1, out int priority1))
        {
            priorityQueueRequests!.Enqueue(req1, Math.Abs(elevatorState.TargetFloor - req1.CurrentFloor + targetFloor));
        }

        isMove = false;
    }

    public IEnumerable<ElevatorRequest> GetElevatorRequests()
    {
        var allRequests = new List<ElevatorRequest>();
        var requests = priorityQueueRequests;
        if (requests != null)
        {
            while (requests.TryDequeue(out var request, out int priority))
            {
                allRequests.Add(request);
            }


            return allRequests;
        }

        return null!;
    }

    public int GetCurrentFloor()
    {
        return elevatorState.CurrentFloor;
    }

    private static bool RequestVerification(ElevatorRequestDTO requestDTO)
    {
        return requestDTO != null && requestDTO.CurrentFloor < 10 && requestDTO.CurrentFloor > 0
                    && requestDTO.TargetFloor < 10 && requestDTO.TargetFloor > 0 && requestDTO.TargetFloor != requestDTO.CurrentFloor;
    }
}
