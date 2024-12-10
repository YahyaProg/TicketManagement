using Application.Services.OrgTypeLimitCollateralService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.OrgTypeLimitCollateral;

public class OrgTypeLimitCollateralRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddOrgTypeLimitCollateralRequest_Success() =>
        Assert.NotNull(new AddOrgTypeLimitCollateralRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetOrgTypeLimitCollateralRequest_Success() =>
        Assert.NotNull(new GetOrgTypeLimitCollateralRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchOrgTypeLimitCollateralRequest_Success() =>
        Assert.NotNull(new SearchOrgTypeLimitCollateralRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateOrgTypeLimitCollateralRequest_Success() =>
        Assert.NotNull(new UpdateOrgTypeLimitCollateralRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteOrgTypeLimitCollateralRequest_Success() =>
        Assert.NotNull(new DeleteOrgTypeLimitCollateralRequestHandler(_unitOfWork.Object));
}
