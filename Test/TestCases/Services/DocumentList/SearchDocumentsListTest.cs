using Application.Services.DocumentListService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.DocumentList;

public class SearchDocumentsListTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task SearchDocumentsListRequest_Success()
    {
        _unitOfWork.Setup(x => x.Context.Documents).ReturnsDbSet([new() { CustomerId = 1, DocumentDefId = 1 }]);
        _unitOfWork.Setup(x => x.Context.DocumentDefs).ReturnsDbSet([new() { Id = 1 }]);
        _unitOfWork.Setup(x => x.Context.DocumentGroups).ReturnsDbSet([new() { DocumentDefs = [new() { Id = 1 }] }]);

        var handler = new SearchDocumentsListRequestHandler(_unitOfWork.Object);

        var request = new SearchDocumentsListRequest { CustomerId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
