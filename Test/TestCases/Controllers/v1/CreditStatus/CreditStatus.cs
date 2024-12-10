using Api.Controllers.v1;
using Application.Services.CreditStatusService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CreditStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CreditStatus;

public class CreditStatusControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<CreditStatusVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<CreditStatusVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddCreditStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddCreditStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CreditStatusController = new CreditStatusController(mediator.Object);

        var addCreditStatusReq = new AddCreditStatusRequest();

        var result = await CreditStatusController.Add(addCreditStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCreditStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetCreditStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var CreditStatusController = new CreditStatusController(mediator.Object);

        var getCreditStatusReq = new GetCreditStatusRequest();

        var result = await CreditStatusController.Get(getCreditStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchCreditStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchCreditStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var CreditStatusController = new CreditStatusController(mediator.Object);

        var searchCreditStatusReq = new SearchCreditStatusRequest();

        var result = await CreditStatusController.Search(searchCreditStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownCreditStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownCreditStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var CreditStatusController = new CreditStatusController(mediator.Object);

        var dropDownCreditStatusReq = new DropDownCreditStatusRequest();

        var result = await CreditStatusController.DropDown(dropDownCreditStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCreditStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCreditStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CreditStatusController = new CreditStatusController(mediator.Object);

        var updateCreditStatusReq = new UpdateCreditStatusRequest();

        var result = await CreditStatusController.Update(updateCreditStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCreditStatusTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteCreditStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CreditStatusController = new CreditStatusController(mediator.Object);

        var deleteCreditStatusReq = new DeleteCreditStatusRequest();

        var result = await CreditStatusController.Delete(deleteCreditStatusReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
