using Api.Controllers.v1;
using Application.Services.PorterGroupService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.PorterGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.PorterGroup;

public class PorterGroupControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PorterGroupVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<PorterGroupVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddPorterGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddPorterGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PorterGroupController = new PorterGroupController(mediator.Object);

        var addPorterGroupReq = new AddPorterGroupRequest();

        var result = await PorterGroupController.Add(addPorterGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetPorterGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetPorterGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var PorterGroupController = new PorterGroupController(mediator.Object);

        var getPorterGroupReq = new GetPorterGroupRequest();

        var result = await PorterGroupController.Get(getPorterGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchPorterGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchPorterGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var PorterGroupController = new PorterGroupController(mediator.Object);

        var searchPorterGroupReq = new SearchPorterGroupRequest();

        var result = await PorterGroupController.Search(searchPorterGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownPorterGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownPorterGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var PorterGroupController = new PorterGroupController(mediator.Object);

        var dropDownPorterGroupReq = new DropDownPorterGroupRequest();

        var result = await PorterGroupController.DropDown(dropDownPorterGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePorterGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdatePorterGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PorterGroupController = new PorterGroupController(mediator.Object);

        var updatePorterGroupReq = new UpdatePorterGroupRequest();

        var result = await PorterGroupController.Update(updatePorterGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeletePorterGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeletePorterGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PorterGroupController = new PorterGroupController(mediator.Object);

        var deletePorterGroupReq = new DeletePorterGroupRequest();

        var result = await PorterGroupController.Delete(deletePorterGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
