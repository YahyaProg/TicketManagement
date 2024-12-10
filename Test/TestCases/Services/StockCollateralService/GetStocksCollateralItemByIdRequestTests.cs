using Application.Services.StocksCollateralService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;



namespace Test.TestCases.Services.StockCollateralService;


public class GetStocksCollateralItemByIdRequestHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnItem_WhenIdExists()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var cancellationToken = CancellationToken.None;

        var testId = 1;

        var mockCustomers = new List<Customer> { new Customer { Id = 1} }.AsQueryable();
        var mockCorporateCustomers = new List<CorporateCustomer> { new CorporateCustomer { Id = 1,Name="Name1" ,CorpId="123" } }.AsQueryable();        
        var mockBourseSymbols = new List<BourseSymbol> { new BourseSymbol { Id = 1, Title = "Test Symbol" } }.AsQueryable();
        var mockData = new List<StocksCollateralItem>
        {
            new StocksCollateralItem { Id = 1, BourseSymbolId = 1,CustomerId=1 ,RelationType = Core.Enums.EStocksCollateralItem_relationType.Direct }
        }.AsQueryable();

        mockContext.Setup(c => c.Customers).ReturnsDbSet(mockCustomers);
        mockContext.Setup(c => c.CorporateCustomers).ReturnsDbSet(mockCorporateCustomers);
        mockContext.Setup(c => c.IndividualCustomers).ReturnsDbSet([]);
        mockContext.Setup(c => c.BourseSymbols).ReturnsDbSet(mockBourseSymbols);
        mockContext.Setup(c => c.StocksCollateralItems).ReturnsDbSet(mockData);
        

        var handler = new GetStocksCollateralItemByIdRequestHandler(mockContext.Object);

        // Act
        var result = await handler.Handle(new GetStocksCollateralItemByIdRequest { Id = testId }, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(testId, result.Data.Id);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var cancellationToken = CancellationToken.None;

        var mockCustomers = new List<Customer> { new Customer { Id = 1 } }.AsQueryable();
        var mockCorporateCustomers = new List<CorporateCustomer> { new CorporateCustomer { Id = 1, Name = "Name1", CorpId = "123" } }.AsQueryable();
        var mockBourseSymbols = new List<BourseSymbol> { new BourseSymbol { Id = 1, Title = "Test Symbol" } }.AsQueryable();
        var mockData = new List<StocksCollateralItem>
        {
            new StocksCollateralItem { Id = 1, BourseSymbolId = 1,CustomerId=1 ,RelationType = Core.Enums.EStocksCollateralItem_relationType.Direct }
        }.AsQueryable();

        mockContext.Setup(c => c.Customers).ReturnsDbSet(mockCustomers);
        mockContext.Setup(c => c.CorporateCustomers).ReturnsDbSet(mockCorporateCustomers);
        mockContext.Setup(c => c.IndividualCustomers).ReturnsDbSet([]);
        mockContext.Setup(c => c.BourseSymbols).ReturnsDbSet(mockBourseSymbols);
        mockContext.Setup(c => c.StocksCollateralItems).ReturnsDbSet(mockData);


        var handler = new GetStocksCollateralItemByIdRequestHandler(mockContext.Object);

        // Act
        var result = await handler.Handle(new GetStocksCollateralItemByIdRequest { Id = 999 }, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Data);
    }
}