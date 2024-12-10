using Application.Services.MovableAssetPropertyService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.MovableAssetProperty;

public class MovableAssetPropertyRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void GetMovableAssetPropertyRequest_Success() =>
        Assert.NotNull(new GetMovableAssetPropertyRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchMovableAssetPropertyRequest_Success() =>
        Assert.NotNull(new SearchMovableAssetPropertyRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteMovableAssetPropertyRequest_Success() =>
        Assert.NotNull(new DeleteMovableAssetPropertyRequestHandler(_unitOfWork.Object));
}
