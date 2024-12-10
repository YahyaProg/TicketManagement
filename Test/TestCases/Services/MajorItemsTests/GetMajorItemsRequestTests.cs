using Application.Services.MajorItemsService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.MajorItemsTests;


public class GetMajorItemsRequestHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMajorItemsVM_WhenItemExists()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var itemId = 1L;

        var majorItems = new List<MajorItems>
        {
            new MajorItems { Id = itemId, Bed = 100, Bes = 0, CompanyFinancialInfoId = 2, ProposalId = 3, ProposalSchemeId = 4, CustomerId = 5, Date = System.DateTime.Now }
        }.AsQueryable();

        var companyInfos = new List<CompanyFinancialInfo>
        {
            new CompanyFinancialInfo { Id = 2, Title = "Company A" }
        }.AsQueryable();



        mockContext.Setup(c => c.MajorItems).ReturnsDbSet(majorItems);
        mockContext.Setup(c => c.CompanyFinancialInfos).ReturnsDbSet(companyInfos);

        var handler = new GetMajorItemsRequestHandler(mockContext.Object);

        var request = new GetMajorItemsRequest { Id = itemId };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(itemId, result.Data.Id);
        Assert.Equal("Company A", result.Data.Title);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenItemDoesNotExist()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();

        var majorItems = new List<MajorItems>().AsQueryable();
        var companyInfos = new List<CompanyFinancialInfo>().AsQueryable();


        mockContext.Setup(c => c.MajorItems).ReturnsDbSet(majorItems);
        mockContext.Setup(c => c.CompanyFinancialInfos).ReturnsDbSet(companyInfos);

        var handler = new GetMajorItemsRequestHandler(mockContext.Object);

        var request = new GetMajorItemsRequest { Id = 999 }; // Non-existent ID

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Null(result.Data);
    }


}