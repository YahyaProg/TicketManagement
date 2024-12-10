using Api.Controllers.v1;
using Application.Services.DocumentDefService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.DocumentDef;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.DocumentDef;

public class DocumentDefControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<DocumentDefVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DocumentDefVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddDocumentDefTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddDocumentDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var DocumentDefController = new DocumentDefController(mediator.Object);

        var addDocumentDefReq = new AddDocumentDefRequest();

        var result = await DocumentDefController.Add(addDocumentDefReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetDocumentDefTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetDocumentDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var DocumentDefController = new DocumentDefController(mediator.Object);

        var getDocumentDefReq = new GetDocumentDefRequest();

        var result = await DocumentDefController.Get(getDocumentDefReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchDocumentDefTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchDocumentDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var DocumentDefController = new DocumentDefController(mediator.Object);

        var searchDocumentDefReq = new SearchDocumentDefRequest();

        var result = await DocumentDefController.Search(searchDocumentDefReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownDocumentDefTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownDocumentDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var DocumentDefController = new DocumentDefController(mediator.Object);

        var dropDownDocumentDefReq = new DropDownDocumentDefRequest();

        var result = await DocumentDefController.DropDown(dropDownDocumentDefReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateDocumentDefTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateDocumentDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var DocumentDefController = new DocumentDefController(mediator.Object);

        var updateDocumentDefReq = new UpdateDocumentDefRequest();

        var result = await DocumentDefController.Update(updateDocumentDefReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteDocumentDefTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteDocumentDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var DocumentDefController = new DocumentDefController(mediator.Object);

        var deleteDocumentDefReq = new DeleteDocumentDefRequest();

        var result = await DocumentDefController.Delete(deleteDocumentDefReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
