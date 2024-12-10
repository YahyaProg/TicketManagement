
using Api.Controllers.v1;
using Application.Services.WebServiceService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class WebServiceControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<WebServiceVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<WebServiceVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddWebServiceTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddWebServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WebServiceController = new WebServiceController(mediator.Object);
        var addCurrncyReq = new AddWebServiceRequest();

        var result = await WebServiceController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteWebServiceTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteWebServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WebServiceController = new WebServiceController(mediator.Object);
        var deleteCurrncyReq = new DeleteWebServiceRequest();

        var result = await WebServiceController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateWebServiceTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateWebServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WebServiceController = new WebServiceController(mediator.Object);
        var updateCurrncyReq = new UpdateWebServiceRequest();

        var result = await WebServiceController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetWebServiceTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetWebServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var WebServiceController = new WebServiceController(mediator.Object);
        var getCurrncyReq = new GetWebServiceRequest();

        var result = await WebServiceController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchWebServiceTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchWebServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var WebServiceController = new WebServiceController(mediator.Object);
        var searchCurrncyReq = new SearchWebServiceRequest();

        var result = await WebServiceController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownWebServiceTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownWebServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var WebServiceController = new WebServiceController(mediator.Object);

        var dropDownWebServiceReq = new DropDownWebServiceRequest();

        var result = await WebServiceController.DropDown(dropDownWebServiceReq);

        Assert.IsType<OkObjectResult>(result);
    }
}