using Application.Services.IndividualValueServices;
using Core.Entities;
using Core.ViewModel;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

public class IndividualValueAddOrUpdateRequestHandlerTests
{
    private readonly Mock<DBContext> _mockContext;
    private readonly Mock<DbSet<IndividualValue>> _mockDbSet;
    private readonly IndividualValueAddOrUpdateRequestHandler _handler;

    public IndividualValueAddOrUpdateRequestHandlerTests()
    {
        _mockContext = new Mock<DBContext>();
        _mockDbSet = new Mock<DbSet<IndividualValue>>();

        // Setup DbSet mock
        _mockContext.Setup(m => m.IndividualValue).Returns(_mockDbSet.Object);
        _mockContext.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() {
            Id = 1,
            ProposalId = 1,
            CustomerId = 1
        }]);

        // Initialize the handler
        _handler = new IndividualValueAddOrUpdateRequestHandler(_mockContext.Object);


    }

    [Fact]
    public async Task Handle_ShouldAddNewValues_WhenItemsHaveNullId()
    {
        // Arrange
        var request = new IndividualValueAddOrUpdateRequest
        {
            ProposalSchemeId = 1,
            Items = new List<IndividualValueAddOrUpdate>
            {
                new IndividualValueAddOrUpdate { ParamCode = "P1", Value = 1 },
                new IndividualValueAddOrUpdate { ParamCode = "P2", Value = 2 }
            }
        };

       

        // Mock SaveChangesAsync to return 1 (success)
        _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _mockDbSet.Verify(m => m.AddRange(It.IsAny<IEnumerable<IndividualValue>>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldUpdateExistingValues_WhenItemsHaveNonNullId()
    {
        // Arrange
        var request = new IndividualValueAddOrUpdateRequest
        {
            ProposalSchemeId = 1,
            Items = new List<IndividualValueAddOrUpdate>
            {
                new IndividualValueAddOrUpdate { Id = 1, ParamCode = "P1", Value = 1 }
            }
        };

        var existingEntities = new List<IndividualValue>
        {
            new IndividualValue { Id = 1, ParamCode = "P1", Value =1 }
        }.AsQueryable();

        // Mock DbSet behavior for querying
        _mockDbSet.As<IQueryable<IndividualValue>>().Setup(m => m.Provider).Returns(existingEntities.Provider);
        _mockDbSet.As<IQueryable<IndividualValue>>().Setup(m => m.Expression).Returns(existingEntities.Expression);
        _mockDbSet.As<IQueryable<IndividualValue>>().Setup(m => m.ElementType).Returns(existingEntities.ElementType);
        _mockDbSet.As<IQueryable<IndividualValue>>().Setup(m => m.GetEnumerator()).Returns(existingEntities.GetEnumerator());


        _mockContext.Setup(x => x.IndividualValue).ReturnsDbSet([new() { Id = 1, CustomerId = 1, ParamCode = "P1", Value = 1 }]);
        // Mock SaveChangesAsync to return 1 (success)
        _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenSaveChangesFails()
    {
        // Arrange
        var request = new IndividualValueAddOrUpdateRequest
        {
            ProposalSchemeId = 1,
            Items = new List<IndividualValueAddOrUpdate>
            {
                new IndividualValueAddOrUpdate { ParamCode = "P1", Value = 1 }
            }
        };

        // Mock SaveChangesAsync to return 0 (failure)
        _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}