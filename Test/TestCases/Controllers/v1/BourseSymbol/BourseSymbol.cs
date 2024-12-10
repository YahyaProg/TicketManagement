using Api.Controllers.v1;
using Application.Services.BourseSymbolService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.BourseSymbol;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BourseSymbol;

public class BourseSymbolControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<BourseSymbolVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<BourseSymbolVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddBourseSymbolTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddBourseSymbolRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BourseSymbolController = new BourseSymbolController(mediator.Object);

        var addBourseSymbolReq = new AddBourseSymbolRequest();

        var result = await BourseSymbolController.Add(addBourseSymbolReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetBourseSymbolTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetBourseSymbolRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var BourseSymbolController = new BourseSymbolController(mediator.Object);

        var getBourseSymbolReq = new GetBourseSymbolRequest();

        var result = await BourseSymbolController.Get(getBourseSymbolReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchBourseSymbolTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchBourseSymbolRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var BourseSymbolController = new BourseSymbolController(mediator.Object);

        var searchBourseSymbolReq = new SearchBourseSymbolRequest();

        var result = await BourseSymbolController.Search(searchBourseSymbolReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownBourseSymbolTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownBourseSymbolRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var BourseSymbolController = new BourseSymbolController(mediator.Object);

        var dropDownBourseSymbolReq = new DropDownBourseSymbolRequest();

        var result = await BourseSymbolController.DropDown(dropDownBourseSymbolReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateBourseSymbolTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateBourseSymbolRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BourseSymbolController = new BourseSymbolController(mediator.Object);

        var updateBourseSymbolReq = new UpdateBourseSymbolRequest();

        var result = await BourseSymbolController.Update(updateBourseSymbolReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteBourseSymbolTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteBourseSymbolRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BourseSymbolController = new BourseSymbolController(mediator.Object);

        var deleteBourseSymbolReq = new DeleteBourseSymbolRequest();

        var result = await BourseSymbolController.Delete(deleteBourseSymbolReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
