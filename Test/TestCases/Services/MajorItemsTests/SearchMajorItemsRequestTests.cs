using Application.Services.MajorItemsService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.MajorItemsTests;

public class SearchMajorItemsRequestHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenItemsExist()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();

        var majorItems = new List<MajorItems>
        {
            new MajorItems { Id = 1, Bed = 100, Bes = 0, CompanyFinancialInfoId = 2, ProposalId = 3, ProposalSchemeId = 4, CustomerId = 5, Date = System.DateTime.Now },
            new MajorItems { Id = 2, Bed = 0, Bes = 100, CompanyFinancialInfoId = 2, ProposalId = 3, ProposalSchemeId = 4, CustomerId = 5, Date = System.DateTime.Now }
        }.AsQueryable();

        var companyInfos = new List<CompanyFinancialInfo>
        {
            new CompanyFinancialInfo { Id = 2, Title = "Company A" }
        }.AsQueryable();



        mockContext.Setup(c => c.MajorItems).ReturnsDbSet(majorItems);
        mockContext.Setup(c => c.CompanyFinancialInfos).ReturnsDbSet(companyInfos);

        var handler = new SearchMajorItemsRequestHandler(mockContext.Object);

        var request = new SearchMajorItemsRequest
        {
            Page = 1,
            Size = 10,
            ProposalId = 3
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.TotalItems); // Ensure both items are returned
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoItemsMatchQuery()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();

        mockContext.Setup(c => c.MajorItems).ReturnsDbSet([]);
        mockContext.Setup(c => c.CompanyFinancialInfos).ReturnsDbSet([]);

        var handler = new SearchMajorItemsRequestHandler(mockContext.Object);

        var request = new SearchMajorItemsRequest
        {
            Page = 1,
            Size = 10,
            ProposalId = 999 // Non-matching ProposalId
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(0, result.Data.TotalItems);
    }
}