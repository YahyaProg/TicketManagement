
using Api.Controllers.v1;
using Application.Services.CurrencyService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class CurrencyControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<CurrencyVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<CurrencyVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddCurrencyTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddCurrencyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var currencyController = new CurrencyController(mediator.Object);
        var addCurrncyReq = new AddCurrencyRequest();

        var result = await currencyController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCurrencyTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteCurrencyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var currencyController = new CurrencyController(mediator.Object);
        var deleteCurrncyReq = new DeleteCurrencyRequest();

        var result = await currencyController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCurrencyTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCurrencyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var currencyController = new CurrencyController(mediator.Object);
        var updateCurrncyReq = new UpdateCurrencyRequest();

        var result = await currencyController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCurrencyTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetCurrencyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var currencyController = new CurrencyController(mediator.Object);
        var getCurrncyReq = new GetCurrencyRequest();

        var result = await currencyController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchCurrencyTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchCurrencyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var currencyController = new CurrencyController(mediator.Object);
        var searchCurrncyReq = new SearchCurrencyRequest();

        var result = await currencyController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownCurrencyTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownCurrencyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var CurrencyController = new CurrencyController(mediator.Object);

        var dropDownCurrencyReq = new DropDownCurrencyRequest();

        var result = await CurrencyController.DropDown(dropDownCurrencyReq);

        Assert.IsType<OkObjectResult>(result);
    }
}