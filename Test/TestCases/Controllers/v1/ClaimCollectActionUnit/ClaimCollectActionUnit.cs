using Api.Controllers.v1;
using Application.Services.ClaimCollectActionUnitService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.ClaimCollectActionUnit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ClaimCollectActionUnit;

public class ClaimCollectActionUnitControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ClaimCollectActionUnitVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ClaimCollectActionUnitVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddClaimCollectActionUnitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddClaimCollectActionUnitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ClaimCollectActionUnitController = new ClaimCollectActionUnitController(mediator.Object);

        var addClaimCollectActionUnitReq = new AddClaimCollectActionUnitRequest();

        var result = await ClaimCollectActionUnitController.Add(addClaimCollectActionUnitReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetClaimCollectActionUnitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetClaimCollectActionUnitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var ClaimCollectActionUnitController = new ClaimCollectActionUnitController(mediator.Object);

        var getClaimCollectActionUnitReq = new GetClaimCollectActionUnitRequest();

        var result = await ClaimCollectActionUnitController.Get(getClaimCollectActionUnitReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchClaimCollectActionUnitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchClaimCollectActionUnitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var ClaimCollectActionUnitController = new ClaimCollectActionUnitController(mediator.Object);

        var searchClaimCollectActionUnitReq = new SearchClaimCollectActionUnitRequest();

        var result = await ClaimCollectActionUnitController.Search(searchClaimCollectActionUnitReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownClaimCollectActionUnitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownClaimCollectActionUnitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var ClaimCollectActionUnitController = new ClaimCollectActionUnitController(mediator.Object);

        var dropDownClaimCollectActionUnitReq = new DropDownClaimCollectActionUnitRequest();

        var result = await ClaimCollectActionUnitController.DropDown(dropDownClaimCollectActionUnitReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateClaimCollectActionUnitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateClaimCollectActionUnitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ClaimCollectActionUnitController = new ClaimCollectActionUnitController(mediator.Object);

        var updateClaimCollectActionUnitReq = new UpdateClaimCollectActionUnitRequest();

        var result = await ClaimCollectActionUnitController.Update(updateClaimCollectActionUnitReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteClaimCollectActionUnitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteClaimCollectActionUnitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ClaimCollectActionUnitController = new ClaimCollectActionUnitController(mediator.Object);

        var deleteClaimCollectActionUnitReq = new DeleteClaimCollectActionUnitRequest();

        var result = await ClaimCollectActionUnitController.Delete(deleteClaimCollectActionUnitReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
