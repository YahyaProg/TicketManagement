using Core.Entities;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Application.Services.ActionLogService
{
    public class GetAllActionLogsRequest : IRequest<ApiResult<PaginatedList<ActionLog>>>
    {
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 10;
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? UserId { get; set; }

        public class GetAllActionLogsRequestHandler(DBContext _context) : IRequestHandler<GetAllActionLogsRequest, ApiResult<PaginatedList<ActionLog>>>
        {
            public async Task<ApiResult<PaginatedList<ActionLog>>> Handle(GetAllActionLogsRequest request, CancellationToken cancellationToken)
            {
                var query = _context.ActionLog
                    .Include(x => x.User)
                    .AsNoTracking()
                    .AsQueryable();

                if (request.UserId.HasValue)
                    query = query.Where(x => x.UserId == request.UserId.Value);
                if (request.DateFrom.HasValue)
                    query = query.Where(x => x.RecordDate >= request.DateFrom.Value);
                if (request.DateTo.HasValue)
                    query = query.Where(x => x.RecordDate <= request.DateTo.Value);

                var result = await query.ToPaginatedListAsync((int)request.Page, (int)request.Size, cancellationToken);
                return new ApiResult<PaginatedList<ActionLog>>()
                {
                    Data = result
                };
            }
        }
    }
}
