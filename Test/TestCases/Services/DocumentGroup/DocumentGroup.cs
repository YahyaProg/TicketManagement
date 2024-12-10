using Application.Services.DocumentGroupService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.DocumentGroup;

public class DocumentGroupRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddDocumentGroupRequest_Success() =>
        Assert.NotNull(new AddDocumentGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetDocumentGroupRequest_Success() =>
        Assert.NotNull(new GetDocumentGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchDocumentGroupRequest_Success() =>
        Assert.NotNull(new SearchDocumentGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownDocumentGroupRequest_Success() =>
        Assert.NotNull(new DropDownDocumentGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateDocumentGroupRequest_Success() =>
        Assert.NotNull(new UpdateDocumentGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteDocumentGroupRequest_Success() =>
        Assert.NotNull(new DeleteDocumentGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownDocumentGroupRequest()
    {
        var request = new DropDownDocumentGroupRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
