using Application.Utils;
using Core.Entities;
using Core.GenericResultModel;
using Core.Helpers;
using DocumentFormat.OpenXml.Math;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.MessageService
{
    public class AddMessageToTicketRequest : IRequest<ApiResult>
    {
        [Required(ErrorMessage = "آیدی تیکت اجباری است!")]
        public long? TicketId { get; set; }
        [Required(ErrorMessage = "متن پیام اجباری است!")]
        public string Message { get; set; }
        public class AddMessageToTicketRequestHandler(DBContext _context, IUserHelper _helper, IActionLogHandler _actionLog) : IRequestHandler<AddMessageToTicketRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(AddMessageToTicketRequest request, CancellationToken cancellationToken)
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.TicketId, cancellationToken);
                if (ticket is null)
                    return new ApiResult(400, false)
                    {
                        Message = "تیکت یافت نشد!"
                    };

                var user = _helper.GetUserFromToken();

                if (user.Role == Core.enums.EUser_Role.Agent)
                {
                    var agent = await _context.SupportAgents.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);

                    if (ticket.SupportAgentId != agent.Id)
                        return new ApiResult(400, false)
                        {
                            Message = "شما به این گفت و گو دسترسی ندارید!"
                        };
                }else if(user.Role == Core.enums.EUser_Role.Customer)
                {
                    if(ticket.Status == Core.enums.ETicket_Status.Closed)
                        return new ApiResult(400, false)
                        {
                            Message = "امکان ارسال پیام به تیکت بسته شده وجود ندارد!"
                        };

                    if (ticket.UserId != user.Id)
                        return new ApiResult(400, false)
                        {
                            Message = "شما به این گفت و گو دسترسی ندارید!"
                        };
                }

                var message = new Message()
                {
                    CreateAt = DateTime.Now,
                    Text = request.Message,
                    UserId = user.Id,
                    TicketId = ticket.Id,
                };

                _context.Messages.Add(message);

                if(user.Role == Core.enums.EUser_Role.Customer)
                    ticket.Status = Core.enums.ETicket_Status.Awaiting;
                else
                    ticket.Status = Core.enums.ETicket_Status.Resolved;

                _context.Tickets.Update(ticket);

                var res = await _context.SaveChangesAsync(cancellationToken);

                await _actionLog.Handle($"new Message Added Id: {message.Id}");

                return res > 0 ? new ApiResult(200, true) : new ApiResult(400, false);
            }
        }
    }
}
