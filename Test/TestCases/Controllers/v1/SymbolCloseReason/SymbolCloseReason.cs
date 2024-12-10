using Api.Controllers.v1;
using Application.Services.SymbolCloseReasonService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.SymbolCloseReason;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.SymbolCloseReason;

public class SymbolCloseReasonControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<SymbolCloseReasonVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<SymbolCloseReasonVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddSymbolCloseReasonTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddSymbolCloseReasonRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SymbolCloseReasonController = new SymbolCloseReasonController(mediator.Object);

        var addSymbolCloseReasonReq = new AddSymbolCloseReasonRequest();

        var result = await SymbolCloseReasonController.Add(addSymbolCloseReasonReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetSymbolCloseReasonTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetSymbolCloseReasonRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var SymbolCloseReasonController = new SymbolCloseReasonController(mediator.Object);

        var getSymbolCloseReasonReq = new GetSymbolCloseReasonRequest();

        var result = await SymbolCloseReasonController.Get(getSymbolCloseReasonReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchSymbolCloseReasonTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchSymbolCloseReasonRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var SymbolCloseReasonController = new SymbolCloseReasonController(mediator.Object);

        var searchSymbolCloseReasonReq = new SearchSymbolCloseReasonRequest();

        var result = await SymbolCloseReasonController.Search(searchSymbolCloseReasonReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownSymbolCloseReasonTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownSymbolCloseReasonRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var SymbolCloseReasonController = new SymbolCloseReasonController(mediator.Object);

        var dropDownSymbolCloseReasonReq = new DropDownSymbolCloseReasonRequest();

        var result = await SymbolCloseReasonController.DropDown(dropDownSymbolCloseReasonReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateSymbolCloseReasonTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateSymbolCloseReasonRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SymbolCloseReasonController = new SymbolCloseReasonController(mediator.Object);

        var updateSymbolCloseReasonReq = new UpdateSymbolCloseReasonRequest();

        var result = await SymbolCloseReasonController.Update(updateSymbolCloseReasonReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteSymbolCloseReasonTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteSymbolCloseReasonRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SymbolCloseReasonController = new SymbolCloseReasonController(mediator.Object);

        var deleteSymbolCloseReasonReq = new DeleteSymbolCloseReasonRequest();

        var result = await SymbolCloseReasonController.Delete(deleteSymbolCloseReasonReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
