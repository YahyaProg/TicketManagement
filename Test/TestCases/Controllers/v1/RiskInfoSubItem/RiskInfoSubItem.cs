using Api.Controllers.v1;
using Application.Services.RiskInfoSubItemService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.RiskInfoSubItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.RiskInfoSubItem;

public class RiskInfoSubItemControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<RiskInfoSubItemVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<RiskInfoSubItemVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddRiskInfoSubItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddRiskInfoSubItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoSubItemController = new RiskInfoSubItemController(mediator.Object);

        var addRiskInfoSubItemReq = new AddRiskInfoSubItemRequest();

        var result = await RiskInfoSubItemController.Add(addRiskInfoSubItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetRiskInfoSubItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetRiskInfoSubItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var RiskInfoSubItemController = new RiskInfoSubItemController(mediator.Object);

        var getRiskInfoSubItemReq = new GetRiskInfoSubItemRequest();

        var result = await RiskInfoSubItemController.Get(getRiskInfoSubItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchRiskInfoSubItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchRiskInfoSubItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var RiskInfoSubItemController = new RiskInfoSubItemController(mediator.Object);

        var searchRiskInfoSubItemReq = new SearchRiskInfoSubItemRequest();

        var result = await RiskInfoSubItemController.Search(searchRiskInfoSubItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownRiskInfoSubItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownRiskInfoSubItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var RiskInfoSubItemController = new RiskInfoSubItemController(mediator.Object);

        var dropDownRiskInfoSubItemReq = new DropDownRiskInfoSubItemRequest();

        var result = await RiskInfoSubItemController.DropDown(dropDownRiskInfoSubItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateRiskInfoSubItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateRiskInfoSubItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoSubItemController = new RiskInfoSubItemController(mediator.Object);

        var updateRiskInfoSubItemReq = new UpdateRiskInfoSubItemRequest();

        var result = await RiskInfoSubItemController.Update(updateRiskInfoSubItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteRiskInfoSubItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteRiskInfoSubItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoSubItemController = new RiskInfoSubItemController(mediator.Object);

        var deleteRiskInfoSubItemReq = new DeleteRiskInfoSubItemRequest();

        var result = await RiskInfoSubItemController.Delete(deleteRiskInfoSubItemReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
