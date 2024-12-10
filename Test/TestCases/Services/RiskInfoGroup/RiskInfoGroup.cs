using Application.Services.RiskInfoGroupService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.RiskInfoGroup;

public class RiskInfoGroupRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void GetRiskInfoGroupRequest_Success() =>
        Assert.NotNull(new GetRiskInfoGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchRiskInfoGroupRequest_Success() =>
        Assert.NotNull(new SearchRiskInfoGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateRiskInfoGroupRequest_Success() =>
        Assert.NotNull(new UpdateRiskInfoGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteRiskInfoGroupRequest_Success() =>
        Assert.NotNull(new DeleteRiskInfoGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownRiskInfoGroupRequest()
    {
        var request = new DropDownRiskInfoGroupRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
