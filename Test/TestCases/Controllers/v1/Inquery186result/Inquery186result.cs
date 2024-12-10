using Api.Controllers.v1;
using Application.Services.Inquery186resultService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.Inquery186result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Inquery186result;
public class Inquery186resultTestController
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<Inquery186resultVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<Inquery186resultVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddInquery186resultTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddInquery186ResultRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var inquery186resultController = new Inquery186ResultController(mediator.Object);
        var addCurrncyReq = new AddInquery186ResultRequest();

        var result = await inquery186resultController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteInquery186resultTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteInquery186ResultRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var inquery186resultController = new Inquery186ResultController(mediator.Object);
        var deleteCurrncyReq = new DeleteInquery186ResultRequest();

        var result = await inquery186resultController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateInquery186resultTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateInquery186ResultRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var inquery186resultController = new Inquery186ResultController(mediator.Object);
        var updateCurrncyReq = new UpdateInquery186ResultRequest();

        var result = await inquery186resultController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetInquery186resultTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetInquery186ResultRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var inquery186resultController = new Inquery186ResultController(mediator.Object);
        var getCurrncyReq = new GetInquery186ResultRequest();

        var result = await inquery186resultController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchInquery186resultTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchInquery186ResultRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var inquery186resultController = new Inquery186ResultController(mediator.Object);

        var searchCurrncyReq = new SearchInquery186ResultRequest();

        var result = await inquery186resultController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownInquery186resultTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownInquery186ResultRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var Inquery186resultController = new Inquery186ResultController(mediator.Object);

        var dropDownInquery186resultReq = new DropDownInquery186ResultRequest();

        var result = await Inquery186resultController.DropDown(dropDownInquery186resultReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
