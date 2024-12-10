
using Application.Services.IvbbFixedAssetService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.IvbbFixedAsset;


public class IvbbFixedAssetRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddIvbbFixedAssetRequest_Success() =>
        Assert.NotNull(new AddIvbbFixedAssetRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteIvbbFixedAssetRequest_Success() =>
        Assert.NotNull(new DeleteIvbbFixedAssetRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdIvbbFixedAssetRequest_Success() =>
        Assert.NotNull(new GetIvbbFixedAssetRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateIvbbFixedAssetRequest_Success() =>
        Assert.NotNull(new UpdateIvbbFixedAssetRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchIvbbFixedAssetRequest_Success() =>
        Assert.NotNull(new SearchIvbbFixedAssetRequestHandler(_unitOfWork.Object));
}