using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities;
using Core.enums;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;

namespace Application.Services.TicketService
{
    public class TicketAdvanceSearchRequest : IRequest<ApiResult<PaginatedList<Ticket>>>
    {
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 10;
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? UserId { get; set; }
        public long? DepartmentId { get; set; }
        public long? SupportAgentId { get; set; }
        public ETicket_Priority? Priority { get; set; }
        public ETicket_Status? Status { get; set; }
        public class TicketAdvanceSearchRequestHandler : IRequestHandler<TicketAdvanceSearchRequest, ApiResult<PaginatedList<Ticket>>>
        {
            private readonly DBContext _context;

            public TicketAdvanceSearchRequestHandler(DBContext context)
            {
                _context = context;
            }

            public async Task<ApiResult<PaginatedList<Ticket>>> Handle(TicketAdvanceSearchRequest request, CancellationToken cancellationToken)
            {
                var query = _context.Tickets
                    .Include(t => t.User)
                    .Include(t => t.SupportAgent)
                    .Include(t => t.Department)
                    .AsNoTracking()
                    .AsQueryable();

                if (request.UserId.HasValue)
                {
                    query = query.Where(t => t.UserId == request.UserId.Value);
                }

                if (request.DateFrom.HasValue)
                {
                    query = query.Where(t => t.CreatedAt >= request.DateFrom.Value);
                }

                if (request.DateTo.HasValue)
                {
                    query = query.Where(t => t.CreatedAt <= request.DateTo.Value);
                }

                if (request.DepartmentId.HasValue)
                {
                    query = query.Where(t => t.DepartmentId == request.DepartmentId.Value);
                }

                if (request.SupportAgentId.HasValue)
                {
                    query = query.Where(t => t.SupportAgentId == request.SupportAgentId.Value);
                }

                if (request.Priority.HasValue)
                {
                    query = query.Where(t => t.Priority == request.Priority.Value);
                }

                if (request.Status.HasValue)
                {
                    query = query.Where(t => t.Status == request.Status.Value);
                }

                var result = await query.ToPaginatedListAsync((int)request.Page, (int)request.Size, cancellationToken);

                return new ApiResult<PaginatedList<Ticket>>()
                {
                    Data = result
                };
            }
        }
    }
}
