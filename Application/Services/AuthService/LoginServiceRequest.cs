using Application.Utils;
using Core.Dto;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class LoginServiceRequest: IRequest<ApiResult<AuthDto>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public class LoginServiceRequestHandler(DBContext _context, IJwtTokenGenerator _generator) : IRequestHandler<LoginServiceRequest, ApiResult<AuthDto>>
        {
            public async Task<ApiResult<AuthDto>> Handle(LoginServiceRequest request, CancellationToken cancellationToken)
            {
                var exist = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Email == request.Email, cancellationToken);

                if (exist is null)
                    return new ApiResult<AuthDto>(404, false);

                var token = _generator.GenerateToken(exist.Id, exist.UserName, exist.Role);
                return new ApiResult<AuthDto>(200)
                {
                    Data = new()
                    {
                        Token = token
                    }
                };
            }
        }
    }
}
