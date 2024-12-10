
using Api.Controllers.v1;
using Application.Services.RiskInfoItemService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class RiskInfoItemControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<RiskInfoItemVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<RiskInfoItemVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddRiskInfoItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddRiskInfoItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoItemController = new RiskInfoItemController(mediator.Object);
        var addCurrncyReq = new AddRiskInfoItemRequest();

        var result = await RiskInfoItemController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteRiskInfoItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteRiskInfoItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoItemController = new RiskInfoItemController(mediator.Object);
        var deleteCurrncyReq = new DeleteRiskInfoItemRequest();

        var result = await RiskInfoItemController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateRiskInfoItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateRiskInfoItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoItemController = new RiskInfoItemController(mediator.Object);
        var updateCurrncyReq = new UpdateRiskInfoItemRequest();

        var result = await RiskInfoItemController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetRiskInfoItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetRiskInfoItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var RiskInfoItemController = new RiskInfoItemController(mediator.Object);
        var getCurrncyReq = new GetRiskInfoItemRequest();

        var result = await RiskInfoItemController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchRiskInfoItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchRiskInfoItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var RiskInfoItemController = new RiskInfoItemController(mediator.Object);
        var searchCurrncyReq = new SearchRiskInfoItemRequest();

        var result = await RiskInfoItemController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownRiskInfoItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownRiskInfoItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var RiskInfoItemController = new RiskInfoItemController(mediator.Object);

        var dropDownRiskInfoItemReq = new DropDownRiskInfoItemRequest();

        var result = await RiskInfoItemController.DropDown(dropDownRiskInfoItemReq);

        Assert.IsType<OkObjectResult>(result);
    }
}