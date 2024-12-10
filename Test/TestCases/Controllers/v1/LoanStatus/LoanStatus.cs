using Api.Controllers.v1;
using Application.Services.LoanStatusService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.LoanStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.LoanStatus;

public class LoanStatusControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<LoanStatusVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<LoanStatusVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddLoanStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddLoanStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var LoanStatusController = new LoanStatusController(mediator.Object);

        var addLoanStatusReq = new AddLoanStatusRequest();

        var result = await LoanStatusController.Add(addLoanStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetLoanStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetLoanStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var LoanStatusController = new LoanStatusController(mediator.Object);

        var getLoanStatusReq = new GetLoanStatusRequest();

        var result = await LoanStatusController.Get(getLoanStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchLoanStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchLoanStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var LoanStatusController = new LoanStatusController(mediator.Object);

        var searchLoanStatusReq = new SearchLoanStatusRequest();

        var result = await LoanStatusController.Search(searchLoanStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownLoanStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownLoanStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var LoanStatusController = new LoanStatusController(mediator.Object);

        var dropDownLoanStatusReq = new DropDownLoanStatusRequest();

        var result = await LoanStatusController.DropDown(dropDownLoanStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLoanStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateLoanStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var LoanStatusController = new LoanStatusController(mediator.Object);

        var updateLoanStatusReq = new UpdateLoanStatusRequest();

        var result = await LoanStatusController.Update(updateLoanStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteLoanStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteLoanStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var LoanStatusController = new LoanStatusController(mediator.Object);

        var deleteLoanStatusReq = new DeleteLoanStatusRequest();

        var result = await LoanStatusController.Delete(deleteLoanStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
