
using Api.Controllers.v1;
using Application.Services.CreditItemRateService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class CreditItemRateControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<CreditItemRateVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<CreditItemRateVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddCreditItemRateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddCreditItemRateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CreditItemRateController = new CreditItemRateController(mediator.Object);
        var addCurrncyReq = new AddCreditItemRateRequest();

        var result = await CreditItemRateController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCreditItemRateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteCreditItemRateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CreditItemRateController = new CreditItemRateController(mediator.Object);
        var deleteCurrncyReq = new DeleteCreditItemRateRequest();

        var result = await CreditItemRateController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCreditItemRateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCreditItemRateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CreditItemRateController = new CreditItemRateController(mediator.Object);
        var updateCurrncyReq = new UpdateCreditItemRateRequest();

        var result = await CreditItemRateController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCreditItemRateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetCreditItemRateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var CreditItemRateController = new CreditItemRateController(mediator.Object);
        var getCurrncyReq = new GetCreditItemRateRequest();

        var result = await CreditItemRateController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchCreditItemRateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchCreditItemRateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var CreditItemRateController = new CreditItemRateController(mediator.Object);
        var searchCurrncyReq = new SearchCreditItemRateRequest();

        var result = await CreditItemRateController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

}