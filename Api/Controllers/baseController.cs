using Core.GenericResultModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BaseController(IMediator Mediator) : ControllerBase
    {
        protected async Task<IActionResult> Sender<T>(IRequest<ApiResult<T>> request)
        {
            var res = await Mediator.Send(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        protected async Task<IActionResult> Sender(IRequest<ApiResult> request)
        {
            var res = await Mediator.Send(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

    }
}
