
using Api.Controllers.v1;
using Application.Services.StockService;
using Core.Enums;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class StockControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<StockVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<StockSearchVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddStockTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddOrUpdateStockRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var StockController = new StockController(mediator.Object);
        var addStockReq = new AddOrUpdateStockRequest();

        var result = await StockController.AddOrUpdate(addStockReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteStockTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteStockRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var StockController = new StockController(mediator.Object);
        var deleteStockReq = new DeleteStockRequest();

        var result = await StockController.Delete(deleteStockReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateStockTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateStockRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var StockController = new StockController(mediator.Object);
        var updateStockReq = new UpdateStockRequest();

        var result = await StockController.Update(updateStockReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetStockTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetStockRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var StockController = new StockController(mediator.Object);
        var getStockReq = new GetStockRequest();

        var result = await StockController.Get(getStockReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchStockTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchStockRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var StockController = new StockController(mediator.Object);
        var searchStockReq = new SearchStockRequest();

        var result = await StockController.Search(searchStockReq);


        Assert.IsType<OkObjectResult>(result);
    }
}