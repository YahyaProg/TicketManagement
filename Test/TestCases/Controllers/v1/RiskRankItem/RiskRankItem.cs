using Api.Controllers.v1;
using Application.Services.RiskRankItemService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.RiskRankItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.RiskRankItem;

public class RiskRankItemControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<RiskRankItemVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<RiskRankItemVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddRiskRankItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddRiskRankItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskRankItemController = new RiskRankItemController(mediator.Object);

        var addRiskRankItemReq = new AddRiskRankItemRequest();

        var result = await RiskRankItemController.Add(addRiskRankItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetRiskRankItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetRiskRankItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var RiskRankItemController = new RiskRankItemController(mediator.Object);

        var getRiskRankItemReq = new GetRiskRankItemRequest();

        var result = await RiskRankItemController.Get(getRiskRankItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchRiskRankItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchRiskRankItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var RiskRankItemController = new RiskRankItemController(mediator.Object);

        var searchRiskRankItemReq = new SearchRiskRankItemRequest();

        var result = await RiskRankItemController.Search(searchRiskRankItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownRiskRankItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownRiskRankItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var RiskRankItemController = new RiskRankItemController(mediator.Object);

        var dropDownRiskRankItemReq = new DropDownRiskRankItemRequest();

        var result = await RiskRankItemController.DropDown(dropDownRiskRankItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateRiskRankItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateRiskRankItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskRankItemController = new RiskRankItemController(mediator.Object);

        var updateRiskRankItemReq = new UpdateRiskRankItemRequest();

        var result = await RiskRankItemController.Update(updateRiskRankItemReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteRiskRankItemTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteRiskRankItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskRankItemController = new RiskRankItemController(mediator.Object);

        var deleteRiskRankItemReq = new DeleteRiskRankItemRequest();

        var result = await RiskRankItemController.Delete(deleteRiskRankItemReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
