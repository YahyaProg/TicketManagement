using Api.Controllers.v1;
using Application.Services.ChequeCollateralSignerService;
using Core.GenericResultModel;
using Core.ViewModel.ChequeCollateralSigner;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ChequeCollateralSigner;

public class ChequeCollateralSignerControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ChequeCollateralSignerVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ChequeCollateralSignerVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddChequeCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddChequeCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ChequeCollateralSignerController = new ChequeCollateralSignerController(mediator.Object);

        var addChequeCollateralSignerReq = new AddChequeCollateralSignerRequest();

        var result = await ChequeCollateralSignerController.Add(addChequeCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetChequeCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetChequeCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var ChequeCollateralSignerController = new ChequeCollateralSignerController(mediator.Object);

        var getChequeCollateralSignerReq = new GetChequeCollateralSignerRequest();

        var result = await ChequeCollateralSignerController.Get(getChequeCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchChequeCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchChequeCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var ChequeCollateralSignerController = new ChequeCollateralSignerController(mediator.Object);

        var searchChequeCollateralSignerReq = new SearchChequeCollateralSignerRequest();

        var result = await ChequeCollateralSignerController.Search(searchChequeCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateChequeCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateChequeCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ChequeCollateralSignerController = new ChequeCollateralSignerController(mediator.Object);

        var updateChequeCollateralSignerReq = new UpdateChequeCollateralSignerRequest();

        var result = await ChequeCollateralSignerController.Update(updateChequeCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteChequeCollateralSignerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteChequeCollateralSignerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ChequeCollateralSignerController = new ChequeCollateralSignerController(mediator.Object);

        var deleteChequeCollateralSignerReq = new DeleteChequeCollateralSignerRequest();

        var result = await ChequeCollateralSignerController.Delete(deleteChequeCollateralSignerReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
