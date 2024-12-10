using Api.Controllers.v1;
using Application.Services.ClaimCollectActionTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.ClaimCollectActionType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ClaimCollectActionType;

public class ClaimCollectActionTypeControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ClaimCollectActionTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ClaimCollectActionTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddClaimCollectActionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddClaimCollectActionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ClaimCollectActionTypeController = new ClaimCollectActionTypeController(mediator.Object);

        var addClaimCollectActionTypeReq = new AddClaimCollectActionTypeRequest();

        var result = await ClaimCollectActionTypeController.Add(addClaimCollectActionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetClaimCollectActionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetClaimCollectActionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var ClaimCollectActionTypeController = new ClaimCollectActionTypeController(mediator.Object);

        var getClaimCollectActionTypeReq = new GetClaimCollectActionTypeRequest();

        var result = await ClaimCollectActionTypeController.Get(getClaimCollectActionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchClaimCollectActionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchClaimCollectActionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var ClaimCollectActionTypeController = new ClaimCollectActionTypeController(mediator.Object);

        var searchClaimCollectActionTypeReq = new SearchClaimCollectActionTypeRequest();

        var result = await ClaimCollectActionTypeController.Search(searchClaimCollectActionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownClaimCollectActionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownClaimCollectActionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var ClaimCollectActionTypeController = new ClaimCollectActionTypeController(mediator.Object);

        var dropDownClaimCollectActionTypeReq = new DropDownClaimCollectActionTypeRequest();

        var result = await ClaimCollectActionTypeController.DropDown(dropDownClaimCollectActionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateClaimCollectActionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateClaimCollectActionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ClaimCollectActionTypeController = new ClaimCollectActionTypeController(mediator.Object);

        var updateClaimCollectActionTypeReq = new UpdateClaimCollectActionTypeRequest();

        var result = await ClaimCollectActionTypeController.Update(updateClaimCollectActionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteClaimCollectActionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteClaimCollectActionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ClaimCollectActionTypeController = new ClaimCollectActionTypeController(mediator.Object);

        var deleteClaimCollectActionTypeReq = new DeleteClaimCollectActionTypeRequest();

        var result = await ClaimCollectActionTypeController.Delete(deleteClaimCollectActionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
