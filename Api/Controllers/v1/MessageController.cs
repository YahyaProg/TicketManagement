using Application.Services.MessageService;
using Core.CustomAttribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowRoles(["Admin", "Agent", "Customer"])]
        [HttpPost("[action]")]
        public async Task<ActionResult> AddMessageToTicket(AddMessageToTicketRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(["Admin", "Agent", "Customer"])]
        [HttpGet("[action]")]
        public async Task<ActionResult> GellAllMessages([FromQuery] GetAllMessagesRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
