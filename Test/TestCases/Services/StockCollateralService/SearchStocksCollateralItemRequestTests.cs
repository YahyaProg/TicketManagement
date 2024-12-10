using Application.Services.StocksCollateralService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.StockCollateralService;

public class SearchStocksCollateralItemRequestHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenStocksCollateralIdExists()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var cancellationToken = CancellationToken.None;

        var testStocksCollateralId = 1L;

 
        var mockCustomers = new List<Customer> { new Customer { Id = 1 } }.AsQueryable();
        var mockCorporateCustomers = new List<CorporateCustomer> { new CorporateCustomer { Id = 1, Name = "Name1", CorpId = "123" } }.AsQueryable();
        var mockBourseSymbols = new List<BourseSymbol> { new BourseSymbol { Id = 1, Title = "Test Symbol" } }.AsQueryable();
        var mockData = new List<StocksCollateralItem>
        {
            new StocksCollateralItem { Id = 1, StocksCollateralId = testStocksCollateralId, RelationType = Core.Enums.EStocksCollateralItem_relationType.Direct},
            new StocksCollateralItem { Id = 2, StocksCollateralId = testStocksCollateralId, RelationType = Core.Enums.EStocksCollateralItem_relationType.Indirect }
        }.AsQueryable();

        mockContext.Setup(c => c.Customers).ReturnsDbSet(mockCustomers);
        mockContext.Setup(c => c.CorporateCustomers).ReturnsDbSet(mockCorporateCustomers);
        mockContext.Setup(c => c.IndividualCustomers).ReturnsDbSet([]);
        mockContext.Setup(c => c.BourseSymbols).ReturnsDbSet(mockBourseSymbols);
        mockContext.Setup(c => c.StocksCollateralItems).ReturnsDbSet(mockData);




        var handler = new SearchStocksCollateralItemRequestHandler(mockContext.Object);

        // Act
        var result = await handler.Handle(new SearchStocksCollateralItemRequest { StocksCollateralId = testStocksCollateralId, Page = 1, Size = 10 }, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Items.Any());
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyPaginatedList_WhenStocksCollateralIdDoesNotExist()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var cancellationToken = CancellationToken.None;

        var mockData = new List<StocksCollateralItem>().AsQueryable();

        mockContext.Setup(c => c.StocksCollateralItems).ReturnsDbSet(mockData);

        var handler = new SearchStocksCollateralItemRequestHandler(mockContext.Object);

        // Act
        var result = await handler.Handle(new SearchStocksCollateralItemRequest { StocksCollateralId = 999, Page = 1, Size = 10 }, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data.Items);
    }
}