using Core.Entities;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
using Core.Helpers;
using Application.Utils;

namespace Application.Services.TicketService
{
    public class CreateTicketRequest : IRequest<ApiResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long? DepartmentId { get; set; }
        public class CreateTicketRequestHandler(DBContext _context, IMediator _mediator, IUserHelper _helper, IActionLogHandler _actionLog) : IRequestHandler<CreateTicketRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
            {
                var user = _helper.GetUserFromToken();

                var ongoingTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Status != Core.enums.ETicket_Status.Closed, cancellationToken);

                if (ongoingTicket is not null)
                    return new ApiResult(400, false)
                    {
                        Message = "شما یک تیکت باز دارید!"
                    };

                var ticket = new Ticket()
                {
                    Title = request.Title,
                    Description = request.Description,
                    CreatedAt = DateTime.Now,
                    DepartmentId = request.DepartmentId,
                    Status = Core.enums.ETicket_Status.New,
                    UserId = user.Id
                };

                var message = new Message()
                {
                    CreateAt = DateTime.Now,
                    Text = request.Description,
                    Ticket = ticket,
                    UserId = user.Id
                };

                _context.Messages.Add(message);

                var ticketSaved = await _context.SaveChangesAsync(cancellationToken);
                if (ticketSaved <= 0)
                    return new ApiResult(400, false);

                await _actionLog.Handle($"Ticket created by title: {request.Title}");


                if (request.DepartmentId is not null)
                {
                    // find the support with less load
                    var agent = await (from s in _context.SupportAgents
                                       from t in _context.Tickets.Where(x => x.SupportAgentId == s.Id).DefaultIfEmpty()
                                       where s.DepartmentId == request.DepartmentId
                                       group s by s.Id into g
                                       select new
                                       {
                                           Id = g.Key,
                                           count = g.Count()
                                       })
                                        .OrderBy(x => x.count)
                                        .FirstOrDefaultAsync(cancellationToken);

                    if (agent is not null)
                        await _mediator.Send(new AssignTicketRequest()
                        {
                            AgentId = agent.Id,
                            Priority = Core.enums.ETicket_Priority.Medium,
                            TicketId = ticket.Id
                        }, cancellationToken);
                }

                return new ApiResult(200, true);
            }
        }
    }
}
