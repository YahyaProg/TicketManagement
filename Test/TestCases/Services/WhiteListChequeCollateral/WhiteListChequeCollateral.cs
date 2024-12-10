using Application.Services.WhiteListChequeCollateralService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.WhiteListChequeCollateral;

public class WhiteListChequeCollateralRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void DeleteWhiteListChequeCollateralRequest_Success() =>
        Assert.NotNull(new DeleteWhiteListChequeCollateralRequestHandler(_unitOfWork.Object));
}
