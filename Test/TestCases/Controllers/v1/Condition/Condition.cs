
using Api.Controllers.v1;
using Application.Services.ConditionService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class ConditionControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ConditionVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ConditionVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddConditionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddConditionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ConditionController = new ConditionController(mediator.Object);
        var addCurrncyReq = new AddConditionRequest();

        var result = await ConditionController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteConditionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteConditionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ConditionController = new ConditionController(mediator.Object);
        var deleteCurrncyReq = new DeleteConditionRequest();

        var result = await ConditionController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateConditionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateConditionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ConditionController = new ConditionController(mediator.Object);
        var updateCurrncyReq = new UpdateConditionRequest();

        var result = await ConditionController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchConditionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchConditionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var ConditionController = new ConditionController(mediator.Object);
        var searchCurrncyReq = new SearchConditionRequest();

        var result = await ConditionController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }
}