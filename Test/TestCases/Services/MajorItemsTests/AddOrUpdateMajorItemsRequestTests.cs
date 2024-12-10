using Application.Services.MajorItemsService;
using Core.Entities;
using Core.ViewModel;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.MajorItemsTests;



public class AddOrUpdateMajorItemsRequestHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenItemsListIsEmpty()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var handler = new AddOrUpdateMajorItemsRequestHandler(mockContext.Object);

        var request = new AddOrUpdateMajorItemsRequest
        {
            Items = new List<MajorItemsAddOrUpdateItemsVM>(),
            ProposalId = 1,
            CustomerId = 2
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotAcceptable_WhenInvalidItemCombination()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var handler = new AddOrUpdateMajorItemsRequestHandler(mockContext.Object);

        var request = new AddOrUpdateMajorItemsRequest
        {
            Items = new List<MajorItemsAddOrUpdateItemsVM>
            {
                new MajorItemsAddOrUpdateItemsVM { Bed = 100, Bes = 200 }
            },
            ProposalId = 1,
            CustomerId = 2
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(406, result.Code);
    }

    [Fact]
    public async Task Handle_ShouldRemoveExistingItems_AndAddNewItems()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var mockDbSet = new Mock<DbSet<MajorItems>>();

        // Mocking `MajorItems` DbSet
        var data = new List<MajorItems>
        {
            new MajorItems { ProposalId = 1, Bed = 50 },
            new MajorItems { ProposalId = 1, Bed = 100 }
        }.AsQueryable();


        mockContext.Setup(c => c.MajorItems).ReturnsDbSet(data);

        mockContext.Setup(x => x.MajorItems.AddRange(It.IsAny<List<MajorItems>>()));
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new AddOrUpdateMajorItemsRequestHandler(mockContext.Object);

        var request = new AddOrUpdateMajorItemsRequest
        {
            Items = new List<MajorItemsAddOrUpdateItemsVM>
            {
                new MajorItemsAddOrUpdateItemsVM { Bed = 150, Bes = 0, CompanyFinancialInfoId = 1 },
                new MajorItemsAddOrUpdateItemsVM { Bed = 0, Bes = 100, CompanyFinancialInfoId = 2 }
            },
            ProposalId = 1,
            CustomerId = 2,
            Date = System.DateTime.Now
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        mockContext.Verify(c => c.MajorItems.RemoveRange(It.IsAny<IEnumerable<MajorItems>>()), Times.Once);
        mockContext.Verify(c => c.MajorItems.AddRange(It.IsAny<IEnumerable<MajorItems>>()), Times.Once);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenSaveChangesFails()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();


        // Mocking DbSet
        mockContext.Setup(c => c.MajorItems).ReturnsDbSet([]);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0); // Simulate failure

        var handler = new AddOrUpdateMajorItemsRequestHandler(mockContext.Object);

        var request = new AddOrUpdateMajorItemsRequest
        {
            Items = new List<MajorItemsAddOrUpdateItemsVM>
            {
                new MajorItemsAddOrUpdateItemsVM { Bed = 150, Bes = 0, CompanyFinancialInfoId = 1 }
            },
            ProposalId = 1,
            CustomerId = 2,
            Date = System.DateTime.Now
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}