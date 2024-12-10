using Application.Services.SafteCollateralSignerService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.SafteCollateralSigner;

public class SafteCollateralSignerRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void DeleteSafteCollateralSignerRequest_Success() =>
        Assert.NotNull(new DeleteSafteCollateralSignerRequestHandler(_unitOfWork.Object));
}
