using Application.Utils;
using Core.Entities;
using Core.enums;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.TicketService
{
    public class ChangeStatusRequest : IRequest<ApiResult>
    {
        public long TicketId { get; set; }
        public ETicket_Status TicketStatus { get; set; }

        public class ChangeStatusRequestHandler(DBContext _context, IActionLogHandler _actionLog) : IRequestHandler<ChangeStatusRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(ChangeStatusRequest request, CancellationToken cancellationToken)
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.TicketId, cancellationToken);
                if (ticket is null)
                    return new ApiResult(404, false)
                    {
                        Message = "موردی یافت نشد"
                    };
                ticket.Status = request.TicketStatus;
                ticket.UpdatedAt = DateTime.Now;
                if (ticket.Status != ETicket_Status.Closed)
                    ticket.ClosedAt = null;

                _context.Tickets.Update(ticket);

                var res = await _context.SaveChangesAsync(cancellationToken);

                await _actionLog.Handle($"ticket Status Changed To: {request.TicketStatus}");

                return res > 0 ? new ApiResult(200, true) : new ApiResult(400, false);
            }
        }
    }
}
