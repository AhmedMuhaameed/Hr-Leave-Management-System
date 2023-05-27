using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDTO>>> Get(bool isLoggedInUser = false)
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationQuery());
            return Ok(leaveAllocations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDetailsDTO>> Get(int id)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsQuery{ Id = id});
            return Ok(leaveAllocation);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
        {
            var response = await _mediator.Send(leaveAllocation);
            return CreatedAtAction(nameof(Get),new {id = response});
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
        {
            await _mediator.Send(leaveAllocation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
