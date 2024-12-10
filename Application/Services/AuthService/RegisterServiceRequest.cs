using Application.Utils;
using Core.Dto;
using Core.Entities;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class RegisterServiceRequest: IRequest<ApiResult<AuthDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public class RegisterServiceHandler(DBContext _context, IJwtTokenGenerator _generator) : IRequestHandler<RegisterServiceRequest, ApiResult<AuthDto>>
        {
            public async Task<ApiResult<AuthDto>> Handle(RegisterServiceRequest request, CancellationToken cancellationToken)
            {
                var exist = await _context.Users.FirstOrDefaultAsync(x => x.UserName ==  request.UserName && x.Email == request.Email, cancellationToken);

                if (exist is not null)
                    return new ApiResult<AuthDto>(400, false)
                    {
                        Message = "کاربری با این مشخصات از قبل موجود است!"
                    };

                var user = new User()
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    Password = request.Password,
                    Role = Core.enums.EUser_Role.Customer
                };

                _context.Users.Add(user);

                var res = await _context.SaveChangesAsync(cancellationToken);

                if (res > 0)
                {
                    var token = _generator.GenerateToken(user.Id, user.UserName, user.Role);
                    return new ApiResult<AuthDto>(200)
                    {
                        Data = new()
                        {
                            Token = token
                        }
                    };
                }
                else
                    return new ApiResult<AuthDto>(400, false);
            }
        }
    }
}
