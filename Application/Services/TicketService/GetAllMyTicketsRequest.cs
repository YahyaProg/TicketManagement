using Core.Entities;
using Core.GenericResultModel;
using Core.Helpers;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Application.Services.TicketService
{
    public class GetAllMyTicketsRequest : IRequest<ApiResult<PaginatedList<Ticket>>>
    {
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 10;

        public class GetAllMyTicketsRequestHandler(DBContext _context, IUserHelper _helper) : IRequestHandler<GetAllMyTicketsRequest, ApiResult<PaginatedList<Ticket>>>
        {
            public async Task<ApiResult<PaginatedList<Ticket>>> Handle(GetAllMyTicketsRequest request, CancellationToken cancellationToken)
            {
                var user = _helper.GetUserFromToken();

                PaginatedList<Ticket> tickets = new([], 10, 1, 10);

                if (user.Role == Core.enums.EUser_Role.Customer)
                    // همه تیکت های مربوط به کاربر رو برمیگردونیم
                    tickets = await _context.Tickets
                        .Where(x => x.UserId == user.Id)
                        .OrderByDescending(x => x.Priority).ThenByDescending(x => x.CreatedAt)
                        .AsNoTracking()
                        .ToPaginatedListAsync((int)request.Page, (int)request.Size, cancellationToken);
                else if(user.Role == Core.enums.EUser_Role.Agent)
                    // همه تیکت هایی که به ایجنت اساین شده هستند و یا دپارتمان مشخص ندارند و اون هایی که در حوزه دپارتمان ایجنت هست رو برمیگردونیم
                    tickets = await _context.Tickets
                        .Include(x => x.SupportAgent)
                        .Where(x => x.SupportAgentId == user.Id || x.DepartmentId == null || x.DepartmentId == x.SupportAgent.DepartmentId)
                        .OrderByDescending(x => x.Priority).ThenByDescending(x => x.CreatedAt)
                        .AsNoTracking()
                        .ToPaginatedListAsync((int)request.Page, (int)request.Size, cancellationToken);

                else if(user.Role == Core.enums.EUser_Role.Admin)
                    // همرو برمیگردونیم
                    tickets = await _context.Tickets
                        .OrderByDescending(x => x.Priority).ThenByDescending(x => x.CreatedAt)
                        .AsNoTracking()
                        .ToPaginatedListAsync((int)request.Page, (int)request.Size, cancellationToken);
                

                return new ApiResult<PaginatedList<Ticket>>()
                {
                    Data = tickets
                };
            }
        }
    }
}
