using Api.Controllers.v1;
using Application.Services.Auth.Keycloak.UserService;
using Application.Services.AuthService;
using Application.Services.AuthService.CaptchaService;
using Core.GenericResultModel;
using Core.ViewModel.Auth;
using Core.ViewModel.Captcha;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.TestCases.Controllers.Auth.Auth
{
    public class AuthControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult<LoginViewModel> successlogin = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<GenerateCpatchaVM> CpatchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult ChangePasswordResponse = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task LoginTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<LoginWithKeyCloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successlogin);

            var authController = new AuthController(mediator.Object);
            var addCurrncyReq = new LoginWithKeyCloakRequest();

            var result = await authController.Login(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task RefreshTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<RefreshWithKeyCloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successlogin);

            var authController = new AuthController(mediator.Object);
            var addCurrncyReq = new RefreshWithKeyCloakRequest();

            var result = await authController.Refresh(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task CaptchaTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GenerateCaptchaRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(CpatchSuccessRes);

            var authController = new AuthController(mediator.Object);
            var addCurrncyReq = new GenerateCaptchaRequest();

            var result = await authController.Captcha();


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task ChangePasswordTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<ChangePasswordWithTokenKeyCloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(ChangePasswordResponse);

            var authController = new AuthController(mediator.Object);
            var request = new ChangePasswordWithTokenKeyCloakRequest();

            var result = await authController.ChangePassword(request);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
