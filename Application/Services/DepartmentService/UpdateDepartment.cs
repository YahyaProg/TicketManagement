using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DepartmentService
{
    public class UpdateDepartmantRequest : IRequest<ApiResult>
    {
        [Required(ErrorMessage ="آیدی اجباری است!")]
        public long? Id { get; set; }
        [Required(ErrorMessage = "نام اجباری است!")]
        public string Name { get; set; }

        public class UpdateDepartmantRequestHandler(DBContext _context) : IRequestHandler<UpdateDepartmantRequest, ApiResult>
        {
            public async Task<ApiResult> Handle(UpdateDepartmantRequest request, CancellationToken cancellationToken)
            {
                var exist = await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (exist is null)
                    return new ApiResult(400, false)
                    {
                        Message = "دپارتمان یافت نشد!"
                    };

                exist.Name = request.Name;

                _context.Departments.Update(exist);

                var res = await _context.SaveChangesAsync(cancellationToken);

                return res > 0 ? new ApiResult(200, true) : new ApiResult(400, false);

            }
        }
    }
}
