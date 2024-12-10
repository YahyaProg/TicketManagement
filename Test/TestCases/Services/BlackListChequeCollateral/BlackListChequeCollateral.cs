using Application.Services.BlackListChequeCollateralService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BlackListChequeCollateral;

public class BlackListChequeCollateralRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void DeleteBlackListChequeCollateralRequest_Success() =>
        Assert.NotNull(new DeleteBlackListChequeCollateralRequestHandler(_unitOfWork.Object));
}
