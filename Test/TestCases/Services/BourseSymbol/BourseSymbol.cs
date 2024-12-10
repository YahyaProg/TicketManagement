using Application.Services.BourseSymbolService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BourseSymbolTest;

public class BourseSymbolRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddBourseSymbolRequest_Success() =>
        Assert.NotNull(new AddBourseSymbolRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetBourseSymbolRequest_Success() =>
        Assert.NotNull(new GetBourseSymbolRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchBourseSymbolRequest_Success() =>
        Assert.NotNull(new SearchBourseSymbolRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownBourseSymbolRequest_Success() =>
        Assert.NotNull(new DropDownBourseSymbolRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateBourseSymbolRequest_Success() =>
        Assert.NotNull(new UpdateBourseSymbolRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteBourseSymbolRequest_Success() =>
        Assert.NotNull(new DeleteBourseSymbolRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownBourseSymbolRequest()
    {
        var request = new DropDownBourseSymbolRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
