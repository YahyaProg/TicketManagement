using Api.Controllers.v1;
using Application.Services.CustomerRequestDetailService;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class CustomerRequestDetailControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<CustomerRequestDetailVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<CustomerRequestDetailSearchVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddCustomerRequestDetailTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddCustomerRequestDetailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var customerRequestDetailController = new CustomerRequestDetailController(mediator.Object);
        var addCurrncyReq = new AddCustomerRequestDetailRequest();

        var result = await customerRequestDetailController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCustomerRequestDetailTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteCustomerRequestDetailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var customerRequestDetailController = new CustomerRequestDetailController(mediator.Object);
        var deleteCurrncyReq = new DeleteCustomerRequestDetailRequest();

        var result = await customerRequestDetailController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCustomerRequestDetailTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCustomerRequestDetailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var customerRequestDetailController = new CustomerRequestDetailController(mediator.Object);
        var updateCurrncyReq = new UpdateCustomerRequestDetailRequest();

        var result = await customerRequestDetailController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCustomerRequestDetailTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetCustomerRequestDetailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var customerRequestDetailController = new CustomerRequestDetailController(mediator.Object);
        var getCurrncyReq = new GetCustomerRequestDetailRequest();

        var result = await customerRequestDetailController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchCustomerRequestDetailTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchCustomerRequestDetailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var customerRequestDetailController = new CustomerRequestDetailController(mediator.Object);
        var searchCurrncyReq = new SearchCustomerRequestDetailRequest();

        var result = await customerRequestDetailController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }
}