using Application.Services.TicketService;
using Core.CustomAttribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [AllowRoles(roles: ["Customer"])]
        [HttpPost("[action]")]
        public async Task<ActionResult> CreateTicket(CreateTicketRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(roles: ["Admin"])]
        [HttpPost("[action]")]
        public async Task<ActionResult> AssignTicket(AssignTicketRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(roles: ["Agent"])]
        [HttpPost("[action]")]
        public async Task<ActionResult> AcceptTicket(AcceptTicketRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(roles: ["Admin", "Agent"])]
        [HttpPost("[action]")]
        public async Task<ActionResult> ChangeStatus(ChangeStatusRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(roles: ["Admin", "Agent", "Customer"])]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllTickets([FromQuery] GetAllMyTicketsRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(roles: ["Admin"])]
        [HttpGet("[action]")]
        public async Task<ActionResult> Search([FromQuery] TicketAdvanceSearchRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
