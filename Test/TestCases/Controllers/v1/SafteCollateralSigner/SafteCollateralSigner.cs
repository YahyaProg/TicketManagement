using Api.Controllers.v1;
using Application.Services.SafteCollateralSignerService;
using Core.GenericResultModel;
using Core.ViewModel.SafteCollateralSigner;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.SafteCollateralSigner;

public class SafteCollateralSignerControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<SafteCollateralSignerVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<SafteCollateralSignerVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddSafteCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddSafteCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SafteCollateralSignerController = new SafteCollateralSignerController(mediator.Object);

        var addSafteCollateralSignerReq = new AddSafteCollateralSignerRequest();

        var result = await SafteCollateralSignerController.Add(addSafteCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetSafteCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetSafteCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var SafteCollateralSignerController = new SafteCollateralSignerController(mediator.Object);

        var getSafteCollateralSignerReq = new GetSafteCollateralSignerRequest();

        var result = await SafteCollateralSignerController.Get(getSafteCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchSafteCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchSafteCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var SafteCollateralSignerController = new SafteCollateralSignerController(mediator.Object);

        var searchSafteCollateralSignerReq = new SearchSafteCollateralSignerRequest();

        var result = await SafteCollateralSignerController.Search(searchSafteCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateSafteCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateSafteCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SafteCollateralSignerController = new SafteCollateralSignerController(mediator.Object);

        var updateSafteCollateralSignerReq = new UpdateSafteCollateralSignerRequest();

        var result = await SafteCollateralSignerController.Update(updateSafteCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteSafteCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteSafteCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SafteCollateralSignerController = new SafteCollateralSignerController(mediator.Object);

        var deleteSafteCollateralSignerReq = new DeleteSafteCollateralSignerRequest();

        var result = await SafteCollateralSignerController.Delete(deleteSafteCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
