using Application.Services.SymbolCloseReasonService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.SymbolCloseReason;

public class SymbolCloseReasonRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddSymbolCloseReasonRequest_Success() =>
        Assert.NotNull(new AddSymbolCloseReasonRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetSymbolCloseReasonRequest_Success() =>
        Assert.NotNull(new GetSymbolCloseReasonRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchSymbolCloseReasonRequest_Success() =>
        Assert.NotNull(new SearchSymbolCloseReasonRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownSymbolCloseReasonRequest_Success() =>
        Assert.NotNull(new DropDownSymbolCloseReasonRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateSymbolCloseReasonRequest_Success() =>
        Assert.NotNull(new UpdateSymbolCloseReasonRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteSymbolCloseReasonRequest_Success() =>
        Assert.NotNull(new DeleteSymbolCloseReasonRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownSymbolCloseReasonRequest()
    {
        var request = new DropDownSymbolCloseReasonRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
