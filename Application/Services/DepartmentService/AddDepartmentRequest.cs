using Core.Entities;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DepartmentService
{
    public class AddDepartmentRequest : IRequest<ApiResult>
    {
        public string Name { get; set; }
        public class AddDepartmentRequestHandler(DBContext _context) : IRequestHandler<AddDepartmentRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(AddDepartmentRequest request, CancellationToken cancellationToken)
            {
                var department = new Department()
                {
                    Deleted = false,
                    Name = request.Name,
                };

                _context.Departments.Add(department);

                var res = await _context.SaveChangesAsync(cancellationToken);

                return res > 0 ? new ApiResult(200, true) : new ApiResult(400, false);

            }
        }
    }
}
