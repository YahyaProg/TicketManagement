using Api.Controllers.v1;
using Application.Services.LoanOtherBanksService;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class LoanOtherbanksControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<LoanOtherBanksVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddLoanOtherbanksTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddLoanOtherBanksRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var LoanOtherbanksController = new LoanOtherBanksController(mediator.Object);
        var addLoanOtherbanksReq = new AddLoanOtherBanksRequest();

        var result = await LoanOtherbanksController.Add(addLoanOtherbanksReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteLoanOtherbanksTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteLoanOtherBanksRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var LoanOtherbanksController = new LoanOtherBanksController(mediator.Object);
        var deleteLoanOtherbanksReq = new DeleteLoanOtherBanksRequest();

        var result = await LoanOtherbanksController.Delete(deleteLoanOtherbanksReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLoanOtherbanksTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateLoanOtherBanksRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var LoanOtherbanksController = new LoanOtherBanksController(mediator.Object);
        var updateLoanOtherbanksReq = new UpdateLoanOtherBanksRequest();

        var result = await LoanOtherbanksController.Update(updateLoanOtherbanksReq);


        Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task SearchLoanOtherbanksTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchLoanOtherBanksRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var LoanOtherbanksController = new LoanOtherBanksController(mediator.Object);
        var searchLoanOtherbanksReq = new SearchLoanOtherBanksRequest();

        var result = await LoanOtherbanksController.Search(searchLoanOtherbanksReq);


        Assert.IsType<OkObjectResult>(result);
    }
}