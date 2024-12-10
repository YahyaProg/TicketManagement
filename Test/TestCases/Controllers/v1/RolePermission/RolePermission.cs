using Api.Controllers.auth;
using Application.Services.RolePermissionService;
using Core.GenericResultModel;
using Core.ViewModel.RolePermission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.RolePermission;

public class RolePermissionControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<RolePermissionVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<RolePermissionVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddRolePermissionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddRolePermissionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RolePermissionController = new RolePermissionController(mediator.Object);

        var addRolePermissionReq = new AddRolePermissionRequest();

        var result = await RolePermissionController.Add(addRolePermissionReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetRolePermissionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetRolePermissionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var RolePermissionController = new RolePermissionController(mediator.Object);

        var getRolePermissionReq = new GetRolePermissionRequest();

        var result = await RolePermissionController.Get(getRolePermissionReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchRolePermissionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchRolePermissionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var RolePermissionController = new RolePermissionController(mediator.Object);

        var searchRolePermissionReq = new SearchRolePermissionRequest();

        var result = await RolePermissionController.Search(searchRolePermissionReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateRolePermissionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateRolePermissionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RolePermissionController = new RolePermissionController(mediator.Object);

        var updateRolePermissionReq = new UpdateRolePermissionRequest();

        var result = await RolePermissionController.Update(updateRolePermissionReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteRolePermissionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteRolePermissionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RolePermissionController = new RolePermissionController(mediator.Object);

        var deleteRolePermissionReq = new DeleteRolePermissionRequest();

        var result = await RolePermissionController.Delete(deleteRolePermissionReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
