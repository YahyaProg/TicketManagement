
using Api.Controllers.v1;
using Application.Services.PorterItemService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class PorterItemControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PorterItemVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<PorterItemVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddPorterItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddPorterItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PorterItemController = new PorterItemController(mediator.Object);
        var addCurrncyReq = new AddPorterItemRequest();

        var result = await PorterItemController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeletePorterItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeletePorterItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PorterItemController = new PorterItemController(mediator.Object);
        var deleteCurrncyReq = new DeletePorterItemRequest();

        var result = await PorterItemController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePorterItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdatePorterItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PorterItemController = new PorterItemController(mediator.Object);
        var updateCurrncyReq = new UpdatePorterItemRequest();

        var result = await PorterItemController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetPorterItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetPorterItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var PorterItemController = new PorterItemController(mediator.Object);
        var getCurrncyReq = new GetPorterItemRequest();

        var result = await PorterItemController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchPorterItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchPorterItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var PorterItemController = new PorterItemController(mediator.Object);
        var searchCurrncyReq = new SearchPorterItemRequest();

        var result = await PorterItemController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownPorterItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownPorterItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var PorterItemController = new PorterItemController(mediator.Object);

        var dropDownPorterItemReq = new DropDownPorterItemRequest();

        var result = await PorterItemController.DropDown(dropDownPorterItemReq);

        Assert.IsType<OkObjectResult>(result);
    }
}