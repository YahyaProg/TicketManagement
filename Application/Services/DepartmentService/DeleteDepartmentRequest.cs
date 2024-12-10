using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DepartmentService
{
    public class DeleteDepartmentRequest : IRequest<ApiResult>
    {
        public long DepartmentId { get; set; }

        public class DeleteDepartmentRequestHandler(DBContext _context) : IRequestHandler<DeleteDepartmentRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
            {
                var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

                department.Deleted = true;

                _context.Departments.Update(department);

                var res = await _context.SaveChangesAsync(cancellationToken);

                return res > 0 ? new ApiResult(200, true) : new ApiResult(400, false);
            }
        }
    }
}
