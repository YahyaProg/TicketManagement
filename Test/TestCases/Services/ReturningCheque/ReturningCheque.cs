using Application.Services.ReturningChequeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ReturningCheque;


public class ReturningChequeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();


    [Fact]
    public void UpdateReturningChequeRequest_Success() =>
        Assert.NotNull(new PartialUpdateReturningChequeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchReturningChequeRequest_Success() =>
        Assert.NotNull(new SearchReturningChequeRequestHandler(_unitOfWork.Object));


}