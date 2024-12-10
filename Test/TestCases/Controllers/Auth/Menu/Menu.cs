
using Api.Controllers.v1;
using Application.Services.Auth.MenuService;
using Application.Services.Auth.RoleService;
using Core.GenericResultModel;
using Core.ViewModel.Auth.Menu;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class MenuControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<MenuVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<List<MenuTreeVM>> getAllSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<MenuVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };


    [Fact]
    public async Task AddMenuTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddMenuRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var MenuController = new MenuController(mediator.Object);
        var addCurrncyReq = new AddMenuRequest();

        var result = await MenuController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMenuTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteMenuRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var MenuController = new MenuController(mediator.Object);
        var deleteCurrncyReq = new DeleteMenuRequest();

        var result = await MenuController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMenuTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateMenuRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var MenuController = new MenuController(mediator.Object);
        var updateCurrncyReq = new UpdateMenuRequest();

        var result = await MenuController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetMenuTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetMenuRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var MenuController = new MenuController(mediator.Object);
        var getCurrncyReq = new GetMenuRequest();

        var result = await MenuController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public async Task GetAllMenuTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAllTreeMenuRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getAllSuccessRes);

        var MenuController = new MenuController(mediator.Object);
        var getCurrncyReq = new GetAllTreeMenuRequest();

        var result = await MenuController.GetAll();


        Assert.IsType<OkObjectResult>(result);
    }

    //[Fact]
    //public async Task SearchMenuTest()
    //{
    //    mediator.Setup(x => x.Send(It.IsAny<SearchMenuRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

    //    var MenuController = new MenuController(mediator.Object);
    //    var searchCurrncyReq = new SearchMenuRequest();

    //    var result = await MenuController.Search(searchCurrncyReq);


    //    Assert.IsType<OkObjectResult>(result);
    //}

    [Fact]
    public async Task DropDownMenuTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownMenuRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var RoleController = new MenuController(mediator.Object);

        var dropDownMenuReq = new DropDownMenuRequest();

        var result = await RoleController.DropDown(dropDownMenuReq);

        Assert.IsType<OkObjectResult>(result);
    }
}