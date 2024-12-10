
using Application.Services.StockService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.Stock;


public class StockRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void DeleteStockRequest_Success() =>
        Assert.NotNull(new DeleteStockRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdStockRequest_Success() =>
        Assert.NotNull(new GetStockRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateStockRequest_Success() =>
        Assert.NotNull(new UpdateStockRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchStockRequest_Success() =>
        Assert.NotNull(new SearchStockRequestHandler(_unitOfWork.Object));
}