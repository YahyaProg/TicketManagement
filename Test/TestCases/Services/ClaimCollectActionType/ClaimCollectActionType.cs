using Application.Services.ClaimCollectActionTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ClaimCollectActionType;

public class ClaimCollectActionTypeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddClaimCollectActionTypeRequest_Success() =>
        Assert.NotNull(new AddClaimCollectActionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetClaimCollectActionTypeRequest_Success() =>
        Assert.NotNull(new GetClaimCollectActionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchClaimCollectActionTypeRequest_Success() =>
        Assert.NotNull(new SearchClaimCollectActionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownClaimCollectActionTypeRequest_Success() =>
        Assert.NotNull(new DropDownClaimCollectActionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateClaimCollectActionTypeRequest_Success() =>
        Assert.NotNull(new UpdateClaimCollectActionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteClaimCollectActionTypeRequest_Success() =>
        Assert.NotNull(new DeleteClaimCollectActionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownClaimCollectActionTypeRequest()
    {
        var request = new DropDownClaimCollectActionTypeRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
