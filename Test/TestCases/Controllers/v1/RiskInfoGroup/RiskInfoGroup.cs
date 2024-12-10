using Api.Controllers.v1;
using Application.Services.RiskInfoGroupService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.RiskInfoGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.RiskInfoGroup;

public class RiskInfoGroupControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<RiskInfoGroupVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<RiskInfoGroupVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddRiskInfoGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddRiskInfoGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoGroupController = new RiskInfoGroupController(mediator.Object);

        var addRiskInfoGroupReq = new AddRiskInfoGroupRequest();

        var result = await RiskInfoGroupController.Add(addRiskInfoGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetRiskInfoGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetRiskInfoGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var RiskInfoGroupController = new RiskInfoGroupController(mediator.Object);

        var getRiskInfoGroupReq = new GetRiskInfoGroupRequest();

        var result = await RiskInfoGroupController.Get(getRiskInfoGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchRiskInfoGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchRiskInfoGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var RiskInfoGroupController = new RiskInfoGroupController(mediator.Object);

        var searchRiskInfoGroupReq = new SearchRiskInfoGroupRequest();

        var result = await RiskInfoGroupController.Search(searchRiskInfoGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownRiskInfoGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownRiskInfoGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var RiskInfoGroupController = new RiskInfoGroupController(mediator.Object);

        var dropDownRiskInfoGroupReq = new DropDownRiskInfoGroupRequest();

        var result = await RiskInfoGroupController.DropDown(dropDownRiskInfoGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateRiskInfoGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateRiskInfoGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoGroupController = new RiskInfoGroupController(mediator.Object);

        var updateRiskInfoGroupReq = new UpdateRiskInfoGroupRequest();

        var result = await RiskInfoGroupController.Update(updateRiskInfoGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteRiskInfoGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteRiskInfoGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var RiskInfoGroupController = new RiskInfoGroupController(mediator.Object);

        var deleteRiskInfoGroupReq = new DeleteRiskInfoGroupRequest();

        var result = await RiskInfoGroupController.Delete(deleteRiskInfoGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
