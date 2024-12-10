using Application.Utils;
using Core.Entities;
using Core.enums;
using Core.GenericResultModel;
using DocumentFormat.OpenXml.Math;
using Infrastructure;
using Irony.Parsing;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.TicketService
{
    public class AssignTicketRequest: IRequest<ApiResult>
    {
        public long AgentId { get; set; }
        public long TicketId { get; set; }
        public ETicket_Priority Priority { get; set; }

        public class AssignTicketRequestHandler(DBContext _context, IActionLogHandler _actionLog) : IRequestHandler<AssignTicketRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(AssignTicketRequest request, CancellationToken cancellationToken)
            {
                var exist = await _context.SupportAgents.FirstOrDefaultAsync(x => x.Id == request.AgentId, cancellationToken);

                if (exist is null)
                    return new ApiResult(400, false)
                    {
                        Message = "کاربر یافت نشد!"
                    };
                var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.TicketId, cancellationToken);

                ticket.SupportAgentId = request.AgentId;
                ticket.Status = ETicket_Status.Assigned;
                ticket.UpdatedAt = DateTime.Now;

                _context.Tickets.Update(ticket);

                var res = await _context.SaveChangesAsync(cancellationToken);

                await _actionLog.Handle($"ticket assigned to agent: {exist.Id}");


                return res > 0 ? new ApiResult(200, true) : new ApiResult(400, false);
            }
        }
    }
}
