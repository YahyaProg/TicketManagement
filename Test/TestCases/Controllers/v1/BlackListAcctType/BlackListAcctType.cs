using Api.Controllers.v1;
using Application.Services.BlackListAcctTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.BlackListAcctType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BlackListAcctType;
public class BlackListAcctTypetestController
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<BlackListAcctTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<BlackListAcctTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddBlackListAcctTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddBlackListAcctTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var blackListAcctTypeController = new BlackListAcctTypeController(mediator.Object);
        var addCurrncyReq = new AddBlackListAcctTypeRequest();

        var result = await blackListAcctTypeController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteBlackListAcctTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteBlackListAcctTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var blackListAcctTypeController = new BlackListAcctTypeController(mediator.Object);
        var deleteCurrncyReq = new DeleteBlackListAcctTypeRequest();

        var result = await blackListAcctTypeController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateBlackListAcctTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateBlackListAcctTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var blackListAcctTypeController = new BlackListAcctTypeController(mediator.Object);
        var updateCurrncyReq = new UpdateBlackListAcctTypeRequest();

        var result = await blackListAcctTypeController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetBlackListAcctTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetBlackListAcctTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var blackListAcctTypeController = new BlackListAcctTypeController(mediator.Object);
        var getCurrncyReq = new GetBlackListAcctTypeRequest();

        var result = await blackListAcctTypeController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchBlackListAcctTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchBlackListAcctTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var blackListAcctTypeController = new BlackListAcctTypeController(mediator.Object);
        var searchCurrncyReq = new SearchBlackListAcctTypeRequest();

        var result = await blackListAcctTypeController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    
}
