using Core.GenericResultModel;
using Core.Helpers;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.TicketService
{
    public class AcceptTicketRequest: IRequest<ApiResult>
    {
        [Required(ErrorMessage ="آیدی تیکت اجباری است!")]
        public long? TicketId { get; set; }


        public class AcceptTicketRequestHandler(DBContext _context, IUserHelper _helper) : IRequestHandler<AcceptTicketRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(AcceptTicketRequest request, CancellationToken cancellationToken)
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.TicketId, cancellationToken);

                if (ticket is null)
                    return new ApiResult(400, false)
                    {
                        Message = "تیکت یافت نشد"
                    };

                if (ticket.SupportAgentId is not null)
                    return new ApiResult(400, false)
                    {
                        Message = "پشتیبان دیگری درحال اراعه خدمات به این تیکت است!"
                    };

                var user = _helper.GetUserFromToken();
                var agent = await _context.SupportAgents.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);

                ticket.SupportAgentId = agent.Id;

                _context.Tickets.Update(ticket);


                var res = await _context.SaveChangesAsync(cancellationToken);

                return res > 0 ? new ApiResult(200, true) : new ApiResult(400, false);
            }
        }
    }
}
