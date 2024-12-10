using Application.Utils;
using Core.Entities;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AddAgentRequest : IRequest<ApiResult>
    {
        [Required(ErrorMessage = "آیدی کاربر اجباری است!")]
        public long? UserId { get; set; }
        [Required(ErrorMessage = "آیدی دپارتمان اجباری است!")]
        public long? DepartmentId { get; set; }
        public class AddAgentRequestHandler(DBContext _context, IActionLogHandler _actionLog) : IRequestHandler<AddAgentRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(AddAgentRequest request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
                if (user is null)
                    return new ApiResult(400, false);

                user.Role = Core.enums.EUser_Role.Agent;
                var agent = new SupportAgent()
                {
                    DepartmentId = (long)request.DepartmentId,
                    Name = user.UserName,
                    User = user,
                };

                _context.SupportAgents.Add(agent);

                var res = await _context.SaveChangesAsync(cancellationToken);

                await _actionLog.Handle($"new Agent Added agentId: {agent.Id}");

                return res > 0 ? new ApiResult(200, true) : new ApiResult(400, false);
            }
        }
    }
}
