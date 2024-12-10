using Api.Controllers.v1;
using Application.Services.LoanMebService;
using Core.GenericResultModel;
using Core.ViewModel.LoanMeb;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.LoanMeb;

public class LoanMebControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<GetAllLoanMebVM>> getAllLoanMebSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task GetAllLoanMebTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAllLoanMebRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getAllLoanMebSuccessRes);

        var loanMebController = new LoanMebController(mediator.Object);

        var getAllLoanMebRequest = new GetAllLoanMebRequest();

        var result = await loanMebController.GetAllLoanMeb(getAllLoanMebRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLoanMebTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateLoanMebRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var loanMebController = new LoanMebController(mediator.Object);

        var updateLoanMebReq = new UpdateLoanMebRequest();

        var result = await loanMebController.UpdateLoanMeb(updateLoanMebReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteLoanMebTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteLoanMebRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var loanMebController = new LoanMebController(mediator.Object);

        var deleteLoanMebReq = new DeleteLoanMebRequest();

        var result = await loanMebController.DeleteLoanMeb(deleteLoanMebReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
