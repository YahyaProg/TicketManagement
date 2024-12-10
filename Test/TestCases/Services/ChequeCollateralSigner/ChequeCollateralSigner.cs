using Application.Services.ChequeCollateralSignerService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ChequeCollateralSigner;

public class ChequeCollateralSignerRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void DeleteChequeCollateralSignerRequest_Success() =>
        Assert.NotNull(new DeleteChequeCollateralSignerRequestHandler(_unitOfWork.Object));
}
