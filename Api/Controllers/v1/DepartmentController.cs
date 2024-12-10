using Application.Services.DepartmentService;
using Application.Services.MessageService;
using Core.CustomAttribute;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowRoles(["Admin"])]
        [HttpPost("[action]")]
        public async Task<ActionResult> AddDepartment([FromBody] AddDepartmentRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(["Admin"])]
        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateDepartment([FromBody] UpdateDepartmantRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(["Admin"])]
        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteDepartment([FromQuery] DeleteDepartmentRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [AllowRoles(["Admin", "Agent", "Customer"])]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllDepartments([FromQuery] GetAllDepartmentsRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
