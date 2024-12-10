
using Api.Controllers.v1;
using Application.Services.SecurityCodeMappingService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class SecurityCodeMappingControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<SecurityCodeMappingVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<SecurityCodeMappingVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddSecurityCodeMappingTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddSecurityCodeMappingRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SecurityCodeMappingController = new SecurityCodeMappingController(mediator.Object);
        var addCurrncyReq = new AddSecurityCodeMappingRequest();

        var result = await SecurityCodeMappingController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteSecurityCodeMappingTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteSecurityCodeMappingRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SecurityCodeMappingController = new SecurityCodeMappingController(mediator.Object);
        var deleteCurrncyReq = new DeleteSecurityCodeMappingRequest();

        var result = await SecurityCodeMappingController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateSecurityCodeMappingTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateSecurityCodeMappingRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SecurityCodeMappingController = new SecurityCodeMappingController(mediator.Object);
        var updateCurrncyReq = new UpdateSecurityCodeMappingRequest();

        var result = await SecurityCodeMappingController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetSecurityCodeMappingTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetSecurityCodeMappingRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var SecurityCodeMappingController = new SecurityCodeMappingController(mediator.Object);
        var getCurrncyReq = new GetSecurityCodeMappingRequest();

        var result = await SecurityCodeMappingController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchSecurityCodeMappingTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchSecurityCodeMappingRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var SecurityCodeMappingController = new SecurityCodeMappingController(mediator.Object);
        var searchCurrncyReq = new SearchSecurityCodeMappingRequest();

        var result = await SecurityCodeMappingController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownSecurityCodeMappingTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownSecurityCodeMappingRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var SecurityCodeMappingController = new SecurityCodeMappingController(mediator.Object);

        var dropDownSecurityCodeMappingReq = new DropDownSecurityCodeMappingRequest();

        var result = await SecurityCodeMappingController.DropDown(dropDownSecurityCodeMappingReq);

        Assert.IsType<OkObjectResult>(result);
    }
}