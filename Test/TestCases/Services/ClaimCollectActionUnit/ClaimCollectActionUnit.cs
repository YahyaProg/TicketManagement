using Application.Services.ClaimCollectActionUnitService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ClaimCollectActionUnit;

public class ClaimCollectActionUnitRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddClaimCollectActionUnitRequest_Success() =>
        Assert.NotNull(new AddClaimCollectActionUnitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetClaimCollectActionUnitRequest_Success() =>
        Assert.NotNull(new GetClaimCollectActionUnitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownClaimCollectActionUnitRequest_Success() =>
        Assert.NotNull(new DropDownClaimCollectActionUnitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateClaimCollectActionUnitRequest_Success() =>
        Assert.NotNull(new UpdateClaimCollectActionUnitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteClaimCollectActionUnitRequest_Success() =>
        Assert.NotNull(new DeleteClaimCollectActionUnitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownClaimCollectActionUnitRequest()
    {
        var request = new DropDownClaimCollectActionUnitRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
