
using Api.Controllers.v1;
using Application.Services.CustomerEducationService;
using Core.GenericResultModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.TestCases.Controllers.v1;


public class CustomerEducationControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddCustomerEducationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddCustomerEducationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CustomerEducationController = new CustomerEducationController(mediator.Object);
        var addCurrncyReq = new AddCustomerEducationRequest();

        var result = await CustomerEducationController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCustomerEducationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteCustomerEducationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CustomerEducationController = new CustomerEducationController(mediator.Object);
        var deleteCurrncyReq = new DeleteCustomerEducationRequest();

        var result = await CustomerEducationController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCustomerEducationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCustomerEducationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CustomerEducationController = new CustomerEducationController(mediator.Object);
        var updateCurrncyReq = new UpdateCustomerEducationRequest();

        var result = await CustomerEducationController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }


   
}