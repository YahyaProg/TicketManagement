using Application.Services.DocumentDefService;
using Application.Services.LoanMebService;
using Core.ViewModel.DocumentDef;
using Core.ViewModel.LoanMeb;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.DocumentDef;

public class DocumentDefRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddDocumentDefRequest_Success() =>
        Assert.NotNull(new AddDocumentDefRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownDocumentDefRequest_Success() =>
        Assert.NotNull(new DropDownDocumentDefRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateDocumentDefRequest_Success() =>
        Assert.NotNull(new UpdateDocumentDefRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteDocumentDefRequest_Success() =>
        Assert.NotNull(new DeleteDocumentDefRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownDocumentDefRequest()
    {
        var request = new DropDownDocumentDefRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0,
        };

        Assert.NotNull(request);
    }

    [Fact]
    public async Task GetDocumentDefRequest_Fail()
    {
        var command = new GetDocumentDefRequest { DocumentDefId = 0 };

        _unitOfWork.Setup(x => x.DocumentDefRepo.GetDocumentDef(It.IsAny<long>()));

        var handler = new GetDocumentDefRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetDocumentDefRequest_Success()
    {
        var command = new GetDocumentDefRequest { DocumentDefId = 0 };

        _unitOfWork.Setup(x => x.DocumentDefRepo.GetDocumentDef(It.IsAny<long>())).ReturnsAsync(
            new DocumentDefVM
            {
                Id = 0
            });

        var handler = new GetDocumentDefRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task SearchDocumentDefRequest_Success()
    {
        var command = new SearchDocumentDefRequest();

        _unitOfWork.Setup(x => x.DocumentDefRepo.SearchDocumentDef(It.IsAny<DocumentDefIM>()));

        var handler = new SearchDocumentDefRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
