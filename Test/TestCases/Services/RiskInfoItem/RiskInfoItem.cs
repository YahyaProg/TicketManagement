
using Application.Services.RiskInfoItemService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.RiskInfoItem;


public class RiskInfoItemRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddRiskInfoItemRequest_Success() =>
        Assert.NotNull(new AddRiskInfoItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteRiskInfoItemRequest_Success() =>
        Assert.NotNull(new DeleteRiskInfoItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdRiskInfoItemRequest_Success() =>
        Assert.NotNull(new GetRiskInfoItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateRiskInfoItemRequest_Success() =>
        Assert.NotNull(new UpdateRiskInfoItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownRiskInfoItemRequest_Success() =>
        Assert.NotNull(new DropDownRiskInfoItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownRiskInfoItemRequest()
    {
        var request = new DropDownRiskInfoItemRequest()
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
