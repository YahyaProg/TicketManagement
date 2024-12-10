using Core.GenericResultModel;
using Core.ViewModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Services.MessageService
{
    public class GetAllMessagesRequest : IRequest<ApiResult<List<MessageVM>>>
    {
        [Required(ErrorMessage = "آیدی تیکت اجباری است!")]
        public long? TicketId { get; set; }
        public class GetAllMessagesRequestHandler(DBContext _context) : IRequestHandler<GetAllMessagesRequest, ApiResult<List<MessageVM>>>
        {
            public async Task<ApiResult<List<MessageVM>>> Handle(GetAllMessagesRequest request, CancellationToken cancellationToken)
            {

                var result = await (from m in _context.Messages
                          join u in _context.Users on m.UserId equals u.Id
                          join t in _context.Tickets on m.TicketId equals t.Id
                          where t.Id == request.TicketId
                          orderby m.CreateAt descending
                          select new MessageVM
                          {
                              CreateAt = m.CreateAt,
                              Id = m.Id,
                              Text = m.Text,
                              SenderName = u.UserName,
                          })
                          .ToListAsync(cancellationToken);

                return new ApiResult<List<MessageVM>>
                {
                    Data = result
                };
            }
        }
    }
}
