using Core.Entities;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Application.Services.AuthService
{
    public class GetAllUsersRequest : IRequest<ApiResult<PaginatedList<User>>>
    {
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 10;

        public class GetAllUsersRequestHandler(DBContext _context) : IRequestHandler<GetAllUsersRequest, ApiResult<PaginatedList<User>>>
        {
            public async Task<ApiResult<PaginatedList<User>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
            {
                var result = await _context.Users
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .ToPaginatedListAsync((int)request.Page, (int)request.Size, cancellationToken);

                return new ApiResult<PaginatedList<User>>
                {
                    Data = result
                };
            }
        }
    }
}
