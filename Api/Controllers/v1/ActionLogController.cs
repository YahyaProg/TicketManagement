using Application.Services.ActionLogService;
using Core.CustomAttribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionLogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ActionLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowRoles(["Admin"])]
        [HttpGet("[action]")]
        public async Task<ActionResult> Search([FromQuery] GetAllActionLogsRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

    }
}
