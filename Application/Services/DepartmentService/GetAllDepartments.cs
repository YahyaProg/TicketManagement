using Core.GenericResultModel;
using Core.ViewModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DepartmentService
{
    public class GetAllDepartmentsRequest : IRequest<ApiResult<List<DepartmentVM>>>
    {
        public class GetAllDepartmentsRequestHandler(DBContext _context) : IRequestHandler<GetAllDepartmentsRequest, ApiResult<List<DepartmentVM>>>
        {
            public async Task<ApiResult<List<DepartmentVM>>> Handle(GetAllDepartmentsRequest request, CancellationToken cancellationToken)
            {
                var result = await _context.Departments
                    .AsNoTracking()
                    .Where(x => x.Deleted != true)
                    .Select(x => new DepartmentVM()
                    {
                        Id = x.Id,
                        Name = x.Name,  
                    })
                    .ToListAsync(cancellationToken);

                return new ApiResult<List<DepartmentVM>> { Data = result };
            }
        }
    }
}
