using Api.Controllers.v1;
using Application.Services.BlackListChequeCollateralService;
using Core.GenericResultModel;
using Core.ViewModel.BlackListChequeCollateral;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BlackListChequeCollateral;

public class BlackListChequeCollateralControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<BlackListChequeCollateralVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<BlackListChequeCollateralVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddBlackListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddBlackListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BlackListChequeCollateralController = new BlackListChequeCollateralController(mediator.Object);

        var addBlackListChequeCollateralReq = new AddBlackListChequeCollateralRequest();

        var result = await BlackListChequeCollateralController.Add(addBlackListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetBlackListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetBlackListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var BlackListChequeCollateralController = new BlackListChequeCollateralController(mediator.Object);

        var getBlackListChequeCollateralReq = new GetBlackListChequeCollateralRequest();

        var result = await BlackListChequeCollateralController.Get(getBlackListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchBlackListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchBlackListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var BlackListChequeCollateralController = new BlackListChequeCollateralController(mediator.Object);

        var searchBlackListChequeCollateralReq = new SearchBlackListChequeCollateralRequest();

        var result = await BlackListChequeCollateralController.Search(searchBlackListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateBlackListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateBlackListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BlackListChequeCollateralController = new BlackListChequeCollateralController(mediator.Object);

        var updateBlackListChequeCollateralReq = new UpdateBlackListChequeCollateralRequest();

        var result = await BlackListChequeCollateralController.Update(updateBlackListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteBlackListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteBlackListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BlackListChequeCollateralController = new BlackListChequeCollateralController(mediator.Object);

        var deleteBlackListChequeCollateralReq = new DeleteBlackListChequeCollateralRequest();

        var result = await BlackListChequeCollateralController.Delete(deleteBlackListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
