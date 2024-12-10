using Api.Controllers.v1;
using Application.Services.SchemeTitleService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.SchemeTitle;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.SchemeTitle;

public class SchemeTitleControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<SchemeTitleVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<SchemeTitleVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddSchemeTitleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddSchemeTitleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SchemeTitleController = new SchemeTitleController(mediator.Object);

        var addSchemeTitleReq = new AddSchemeTitleRequest();

        var result = await SchemeTitleController.Add(addSchemeTitleReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetSchemeTitleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetSchemeTitleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var SchemeTitleController = new SchemeTitleController(mediator.Object);

        var getSchemeTitleReq = new GetSchemeTitleRequest();

        var result = await SchemeTitleController.Get(getSchemeTitleReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchSchemeTitleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchSchemeTitleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var SchemeTitleController = new SchemeTitleController(mediator.Object);

        var searchSchemeTitleReq = new SearchSchemeTitleRequest();

        var result = await SchemeTitleController.Search(searchSchemeTitleReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownSchemeTitleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownSchemeTitleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var SchemeTitleController = new SchemeTitleController(mediator.Object);

        var dropDownSchemeTitleReq = new DropDownSchemeTitleRequest();

        var result = await SchemeTitleController.DropDown(dropDownSchemeTitleReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateSchemeTitleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateSchemeTitleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SchemeTitleController = new SchemeTitleController(mediator.Object);

        var updateSchemeTitleReq = new UpdateSchemeTitleRequest();

        var result = await SchemeTitleController.Update(updateSchemeTitleReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteSchemeTitleTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteSchemeTitleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var SchemeTitleController = new SchemeTitleController(mediator.Object);

        var deleteSchemeTitleReq = new DeleteSchemeTitleRequest();

        var result = await SchemeTitleController.Delete(deleteSchemeTitleReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
