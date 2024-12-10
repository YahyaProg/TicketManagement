using Api.Controllers.auth;
using Application.Services.Auth;
using Application.Services.Auth.Keycloak.UserService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using Gateway.Model.KeyCloak.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.User
{
    public class UserControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult<UserKeyCloakVM> successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<UserKeyCloakVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<UserDtoWithBranchId> getUserDetailsSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<bool> StatusSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddUserTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddUserKeyCloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var UserController = new UserController(mediator.Object);
            var addCurrncyReq = new AddUserKeyCloakRequest();

            var result = await UserController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task GetUserTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetUserKeyCloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var UserController = new UserController(mediator.Object);
            var getCurrncyReq = new GetUserKeyCloakRequest();

            var result = await UserController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        
        }

        [Fact]
        public async Task GetUserDetailsTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetTokenDataRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getUserDetailsSuccessRes);

            var UserController = new UserController(mediator.Object);

            var result = await UserController.GetUserDetails();

            Assert.IsType<OkObjectResult>(result);

        }



        [Fact]
        public async Task ChangeStatusTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<ChangeStatusUserKeyCloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(StatusSuccessRes);

            var UserController = new UserController(mediator.Object);
            var getCurrncyReq = new ChangeStatusUserKeyCloakRequest();

            var result = await UserController.ChangeStatus(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task ChangePasswordTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<ChangePasswordKeyCloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(StatusSuccessRes);

            var UserController = new UserController(mediator.Object);
            var getCurrncyReq = new ChangePasswordKeyCloakRequest();

            var result = await UserController.ChangePassword(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

    }
}
