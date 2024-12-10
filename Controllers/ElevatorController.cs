using ActualLab.Fusion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationElevator.Context;
using WebApplicationElevator.DTOs;
using WebApplicationElevator.Models;
using WebApplicationElevator.Service;

namespace WebApplicationElevator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorController : ControllerBase
    {
        private readonly IAppService service;
        public ElevatorController(IAppService service)
        {
            this.service = service;
        }

        [HttpPost("CallTheElevator")]
        public async Task<ActionResult> CallTheElevatorAsync([FromBody]ElevatorRequestDTO requestDTO)
        {
            if (await service.CallTheElevatorAsync(requestDTO))
            {
                return Ok("The elevator has been called, please wait.");
            }
            return BadRequest();
        }

        [HttpGet("GetAllRequests")]
        public ActionResult GetAllElevatorRequests()
        {
            if (service.GetElevatorRequests() is not null)
            {
                return Ok(service.GetElevatorRequests());
            }
            return NoContent();
        }

        [HttpGet("GetElevatorCurrentFloor")]
        public ActionResult GetElevatorCurrentFloor()
        {
            return Ok(service.GetCurrentFloor());
        }
    }
}
