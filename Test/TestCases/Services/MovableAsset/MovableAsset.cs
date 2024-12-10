using Application.Services.MovableAssetService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.MovableAsset;
public class MovableAssetRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddMovableAssetRequest_Success() =>
        Assert.NotNull(new AddMovableAssetRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SoftDeleteMovableAssetRequest_Success() =>
        Assert.NotNull(new DeleteMovableAssetRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetMovableAssetRequest_Success() =>
        Assert.NotNull(new GetMovableAssetRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateMovableAssetRequest_Success() =>
        Assert.NotNull(new UpdateMovableAssetRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownMovableAssetRequest_Success() =>
        Assert.NotNull(new DropDownMovableAssetRequestHandler(_unitOfWork.Object));
}
