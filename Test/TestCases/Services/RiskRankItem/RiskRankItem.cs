using Application.Services.AccountModuleDescService;
using Application.Services.RiskRankItemService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.RiskRankItemTest;

public class RiskRankItemRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddRiskRankItemRequest_Success() =>
        Assert.NotNull(new AddRiskRankItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetRiskRankItemRequest_Success() =>
        Assert.NotNull(new GetRiskRankItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchRiskRankItemRequest_Success() =>
        Assert.NotNull(new SearchRiskRankItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownRiskRankItemRequest_Success() =>
        Assert.NotNull(new DropDownRiskRankItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateRiskRankItemRequest_Success() =>
        Assert.NotNull(new UpdateRiskRankItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteRiskRankItemRequest_Success() =>
        Assert.NotNull(new DeleteRiskRankItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownRiskRankItemRequest()
    {
        var request = new DropDownRiskRankItemRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
