using Api.Controllers.auth;
using Application.Services.Auth.Keycloak.GroupService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Gateway.Model.KeyCloak.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.Auth.Group
{
    public class GroupControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<RoleKeycloakDto> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<List<RoleKeycloakDto>> getAllSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };
        [Fact]
        public async Task GetGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetGroupKeycloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var GroupController = new GroupController(mediator.Object);

            var getCurrncyReq = new GetGroupKeycloakRequest();

            var result = await GroupController.Get(getCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetAllGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetAllGroupsKeycloakRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getAllSuccessRes);

            var GroupController = new GroupController(mediator.Object);

            var getCurrncyReq = new GetAllGroupsKeycloakRequest();

            var result = await GroupController.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
