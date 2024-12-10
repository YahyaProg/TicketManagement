using Application.Services.StocksCollateralService;
using Core.Entities;
using Core.Enums;
using Infrastructure;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;


namespace Test.TestCases.Services.StockCollateralService;
public class UpdateStocksCollateralItemRequestHandlerTests
{

    private readonly Mock<DBContext> _mockDbContext;

    public UpdateStocksCollateralItemRequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenItemDoesNotExist()
    {
        // Arrange

        var mediatorMock = new Mock<IMediator>();
        var handler = new UpdateStocksCollateralItemRequestHandler(_mockDbContext.Object, mediatorMock.Object);
        var request = new UpdateStocksCollateralItemRequest { Id = 999 };

        var corporateCustomers = new List<StocksCollateralItem>
            {
                new () { Id = 1,CustomerId=1 }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.StocksCollateralItems).ReturnsDbSet(corporateCustomers);
      

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        var expectedCode = 404;
        var expectedStatus = false;
        Assert.Equal(result.Code, expectedCode);
        Assert.Equal(result.IsSuccess, expectedStatus);
    }

    [Fact]
    public async Task Handle_Should_UpdateDirectRelationType()
    {
        // Arrange
        var existingItem = new StocksCollateralItem
        {
            Id = 1,
            RelationType = EStocksCollateralItem_relationType.Indirect,
            CustomerId = 123
        };

        var corporateCustomers = new List<StocksCollateralItem>
            {
                new () { Id = 1,CustomerId=123,RelationType = EStocksCollateralItem_relationType.Indirect }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.StocksCollateralItems).ReturnsDbSet(corporateCustomers);
        _ =  _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);


        var mediatorMock = new Mock<IMediator>();
        var handler = new UpdateStocksCollateralItemRequestHandler(_mockDbContext.Object, mediatorMock.Object);
        var request = new UpdateStocksCollateralItemRequest
        {
            Id = 1,
            RelationType = EStocksCollateralItem_relationType.Direct
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);


        //var expectedRelationType = EStocksCollateralItem_relationType.Direct;
        var expectedStatus = true;
        Assert.Equal(result.IsSuccess, expectedStatus);

    }

}