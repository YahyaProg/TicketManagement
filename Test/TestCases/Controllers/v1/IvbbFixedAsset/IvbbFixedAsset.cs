
using Api.Controllers.v1;
using Application.Services.IvbbFixedAssetService;
using Core.Enums;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class IvbbFixedAssetControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<IvbbFixedAssetVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<IvbbFixedAssetVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddIvbbFixedAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddIvbbFixedAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var IvbbFixedAssetController = new IvbbFixedAssetController(mediator.Object);
        var addCurrncyReq = new AddIvbbFixedAssetRequest();

        var result = await IvbbFixedAssetController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteIvbbFixedAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteIvbbFixedAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var IvbbFixedAssetController = new IvbbFixedAssetController(mediator.Object);
        var deleteCurrncyReq = new DeleteIvbbFixedAssetRequest();

        var result = await IvbbFixedAssetController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateIvbbFixedAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateIvbbFixedAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var IvbbFixedAssetController = new IvbbFixedAssetController(mediator.Object);
        var updateCurrncyReq = new UpdateIvbbFixedAssetRequest();

        var result = await IvbbFixedAssetController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetIvbbFixedAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetIvbbFixedAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var IvbbFixedAssetController = new IvbbFixedAssetController(mediator.Object);
        var getCurrncyReq = new GetIvbbFixedAssetRequest();

        var result = await IvbbFixedAssetController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchIvbbFixedAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchIvbbFixedAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var IvbbFixedAssetController = new IvbbFixedAssetController(mediator.Object);
        var searchCurrncyReq = new SearchIvbbFixedAssetRequest();

        var result = await IvbbFixedAssetController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }
}