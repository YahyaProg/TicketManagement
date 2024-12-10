
using Api.Controllers.v1;
using Application.Services.FinancialYearInfoService;
using Core.Enums;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class FinancialYearInfoControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<FinancialYearInfoVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<FinancialYearInfoVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddFinancialYearInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddFinancialYearInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var FinancialYearInfoController = new FinancialYearInfoController(mediator.Object);
        var addCurrncyReq = new AddFinancialYearInfoRequest();

        var result = await FinancialYearInfoController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteFinancialYearInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteFinancialYearInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var FinancialYearInfoController = new FinancialYearInfoController(mediator.Object);
        var deleteCurrncyReq = new DeleteFinancialYearInfoRequest();

        var result = await FinancialYearInfoController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateFinancialYearInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateFinancialYearInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var FinancialYearInfoController = new FinancialYearInfoController(mediator.Object);
        var updateCurrncyReq = new UpdateFinancialYearInfoRequest();

        var result = await FinancialYearInfoController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetFinancialYearInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetFinancialYearInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var FinancialYearInfoController = new FinancialYearInfoController(mediator.Object);
        var getCurrncyReq = new GetFinancialYearInfoRequest();

        var result = await FinancialYearInfoController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchFinancialYearInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchFinancialYearInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var FinancialYearInfoController = new FinancialYearInfoController(mediator.Object);
        var searchCurrncyReq = new SearchFinancialYearInfoRequest();

        var result = await FinancialYearInfoController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }
}