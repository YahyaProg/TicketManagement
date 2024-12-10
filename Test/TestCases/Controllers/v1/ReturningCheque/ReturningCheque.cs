
using Api.Controllers.v1;
using Application.Services.ReturningChequeService;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class ReturningChequeControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ReturningChequeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task UpdateReturningChequeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<PartialUpdateReturningChequeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ReturningChequeController = new ReturningChequeController(mediator.Object);
        var updateCurrncyReq = new PartialUpdateReturningChequeRequest();

        var result = await ReturningChequeController.PartialUpdateForComment(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchReturningChequeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchReturningChequeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var ReturningChequeController = new ReturningChequeController(mediator.Object);
        var searchCurrncyReq = new SearchReturningChequeRequest();

        var result = await ReturningChequeController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }
}