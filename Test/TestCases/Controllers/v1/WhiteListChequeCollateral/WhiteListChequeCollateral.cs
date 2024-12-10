using Api.Controllers.v1;
using Application.Services.WhiteListChequeCollateralService;
using Core.GenericResultModel;
using Core.ViewModel.WhiteListChequeCollateral;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.WhiteListChequeCollateral;

public class WhiteListChequeCollateralControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<WhiteListChequeCollateralVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<WhiteListChequeCollateralVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddWhiteListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddWhiteListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WhiteListChequeCollateralController = new WhiteListChequeCollateralController(mediator.Object);

        var addWhiteListChequeCollateralReq = new AddWhiteListChequeCollateralRequest();

        var result = await WhiteListChequeCollateralController.Add(addWhiteListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetWhiteListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetWhiteListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var WhiteListChequeCollateralController = new WhiteListChequeCollateralController(mediator.Object);

        var getWhiteListChequeCollateralReq = new GetWhiteListChequeCollateralRequest();

        var result = await WhiteListChequeCollateralController.Get(getWhiteListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchWhiteListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchWhiteListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var WhiteListChequeCollateralController = new WhiteListChequeCollateralController(mediator.Object);

        var searchWhiteListChequeCollateralReq = new SearchWhiteListChequeCollateralRequest();

        var result = await WhiteListChequeCollateralController.Search(searchWhiteListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateWhiteListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateWhiteListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WhiteListChequeCollateralController = new WhiteListChequeCollateralController(mediator.Object);

        var updateWhiteListChequeCollateralReq = new UpdateWhiteListChequeCollateralRequest();

        var result = await WhiteListChequeCollateralController.Update(updateWhiteListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteWhiteListChequeCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteWhiteListChequeCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WhiteListChequeCollateralController = new WhiteListChequeCollateralController(mediator.Object);

        var deleteWhiteListChequeCollateralReq = new DeleteWhiteListChequeCollateralRequest();

        var result = await WhiteListChequeCollateralController.Delete(deleteWhiteListChequeCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
