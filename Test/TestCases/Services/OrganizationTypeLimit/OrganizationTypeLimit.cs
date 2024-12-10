using Application.Services.OrganizationTypeLimitService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.OrganizationTypeLimit;
public class OrganizationTypeLimit
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();


    [Fact]
    public void AddOrganizationTypeLimitRequest_Success() =>
        Assert.NotNull(new AddOrganizationTypeLimitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteOrganizationTypeLimitRequest_Success() =>
        Assert.NotNull(new DeleteOrganizationTypeLimitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdOrganizationTypeLimitRequest_Success() =>
        Assert.NotNull(new GetOrganizationTypeLimitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateOrganizationTypeLimitRequest_Success() =>
        Assert.NotNull(new UpdateOrganizationTypeLimitRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchOrganizationTypeLimitRequest_Success() =>
        Assert.NotNull(new AdvanceSearchOrganizationTypeLimitRequestHandler(_unitOfWork.Object));
}
