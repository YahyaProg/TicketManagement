using Api.Controllers.v1;
using Application.Services.DocumentGroupService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.DocumentGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.DocumentGroup;

public class DocumentGroupControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<DocumentGroupVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DocumentGroupVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddDocumentGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddDocumentGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var DocumentGroupController = new DocumentGroupController(mediator.Object);

        var addDocumentGroupReq = new AddDocumentGroupRequest();

        var result = await DocumentGroupController.Add(addDocumentGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetDocumentGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetDocumentGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var DocumentGroupController = new DocumentGroupController(mediator.Object);

        var getDocumentGroupReq = new GetDocumentGroupRequest();

        var result = await DocumentGroupController.Get(getDocumentGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchDocumentGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchDocumentGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var DocumentGroupController = new DocumentGroupController(mediator.Object);

        var searchDocumentGroupReq = new SearchDocumentGroupRequest();

        var result = await DocumentGroupController.Search(searchDocumentGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownDocumentGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownDocumentGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var DocumentGroupController = new DocumentGroupController(mediator.Object);

        var dropDownDocumentGroupReq = new DropDownDocumentGroupRequest();

        var result = await DocumentGroupController.DropDown(dropDownDocumentGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateDocumentGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateDocumentGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var DocumentGroupController = new DocumentGroupController(mediator.Object);

        var updateDocumentGroupReq = new UpdateDocumentGroupRequest();

        var result = await DocumentGroupController.Update(updateDocumentGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteDocumentGroupTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteDocumentGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var DocumentGroupController = new DocumentGroupController(mediator.Object);

        var deleteDocumentGroupReq = new DeleteDocumentGroupRequest();

        var result = await DocumentGroupController.Delete(deleteDocumentGroupReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
