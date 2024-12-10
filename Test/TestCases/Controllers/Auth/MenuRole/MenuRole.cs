
using Api.Controllers.v1;
using Application.Services.Auth.MenuRoleService;
using Core.GenericResultModel;
using Core.ViewModel.Auth.MenuRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class MenuRoleControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<MenuRoleVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<MenuRoleVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddMenuRoleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddMenuRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var MenuRoleController = new MenuRoleController(mediator.Object);
        var addCurrncyReq = new AddMenuRoleRequest();

        var result = await MenuRoleController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMenuRoleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteMenuRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var MenuRoleController = new MenuRoleController(mediator.Object);
        var deleteCurrncyReq = new DeleteMenuRoleRequest();

        var result = await MenuRoleController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMenuRoleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateMenuRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var MenuRoleController = new MenuRoleController(mediator.Object);
        var updateCurrncyReq = new UpdateMenuRoleRequest();

        var result = await MenuRoleController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetMenuRoleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetMenuRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var MenuRoleController = new MenuRoleController(mediator.Object);
        var getCurrncyReq = new GetMenuRoleRequest();

        var result = await MenuRoleController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

}