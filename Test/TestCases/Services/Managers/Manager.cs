using Application.Services.Manager;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.Managers;

public class ManagerRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void DeleteMajorStockHolderRequest_Success() =>
        Assert.NotNull(new DeleteMajorStockHolderRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteBoardMemberAndManagerHandler_Success() =>
        Assert.NotNull(new DeleteBoardMemberAndManagerHandler(_unitOfWork.Object));
}
