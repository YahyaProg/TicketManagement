using Application.Services.IsicSubGroupService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.IsicSubGroupTests;


public class UpdateIsicSubGroupRequestHandlerTests
{
    private readonly Mock<DBContext> _mockDbContext;
    private readonly UpdateIsicSubGroupRequestHandler _handler;

    public UpdateIsicSubGroupRequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
        _handler = new UpdateIsicSubGroupRequestHandler(_mockDbContext.Object);
    }

    [Fact]
    public async Task Handle_IsicSubGroupNotFound_Returns404()
    {
        // Arrange
        var request = new UpdateIsicSubGroupRequest { Id = 1 };
        _mockDbContext.Setup(db => db.IsicSubGroups).ReturnsDbSet([]);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }

    [Fact]
    public async Task Handle_TitleDuplicate_Returns400()
    {
        // Arrange
        var request = new UpdateIsicSubGroupRequest { Id = 1, Title = "DuplicateTitle" };
        var isicSubGroups = new List<IsicSubGroup>() {
            new IsicSubGroup { Id = 1, Title = "ExistingTitle" },
            new IsicSubGroup { Id = 2, Title = "DuplicateTitle" }
        };
        _mockDbContext.Setup(db => db.IsicSubGroups).ReturnsDbSet(isicSubGroups);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
        Assert.Equal("عنوان وارد شده تکراری می باشد", result.Message);
    }

    [Fact]
    public async Task Handle_CodeDuplicate_Returns400()
    {
        // Arrange
        var request = new UpdateIsicSubGroupRequest { Id = 1, Title = "NewTitle1", Code = "DuplicateCode" };
        var isicSubGroups = new List<IsicSubGroup>() {
            new IsicSubGroup { Id = 1, Title = "ExistingTitle1",Code="DuplicateCode" },
            new IsicSubGroup { Id = 3, Title = "ExistingTitle3",Code="DuplicateCode" },
            new IsicSubGroup { Id = 2, Title = "ExistingTitle2",Code="Code1" }
        };
        _mockDbContext.Setup(db => db.IsicSubGroups).ReturnsDbSet(isicSubGroups);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
        Assert.Equal("کد وارد شده تکراری می باشد", result.Message);
    }

    [Fact]
    public async Task Handle_Success_ReturnsSuccess()
    {
        // Arrange
        var request = new UpdateIsicSubGroupRequest
        {
            Id = 1,
            Title = "NewTitle",
            Code = "NewCode",
            IsicGroupId = 10
        };

        var isicSubGroups = new List<IsicSubGroup>() {
           new IsicSubGroup { Id = 1, Title = "OldTitle", Code = "OldCode", IsicGroupId = 5 }
        };
        _mockDbContext.Setup(db => db.IsicSubGroups).ReturnsDbSet(isicSubGroups);

        _mockDbContext.Setup(x => x.CustomerAddresses.Update(It.IsAny<CustomerAddress>()));
        _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }
}