using Application.Services.RiskInfoSubItemService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.RiskInfoSubItem;

public class RiskInfoSubItemRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddRiskInfoSubItemRequest_Success() =>
        Assert.NotNull(new AddRiskInfoSubItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetRiskInfoSubItemRequest_Success() =>
        Assert.NotNull(new GetRiskInfoSubItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownRiskInfoSubItemRequest_Success() =>
        Assert.NotNull(new DropDownRiskInfoSubItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateRiskInfoSubItemRequest_Success() =>
        Assert.NotNull(new UpdateRiskInfoSubItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteRiskInfoSubItemRequest_Success() =>
        Assert.NotNull(new DeleteRiskInfoSubItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownRiskInfoSubItemRequest()
    {
        var request = new DropDownRiskInfoSubItemRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
