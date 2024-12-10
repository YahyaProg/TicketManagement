using Application.Services.ProposalService;
using Core.Entities;
using Core.Helpers;
using Core.ViewModel;
using Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.EntityFrameworkCore;


namespace Test.TestCases.Services.ProposalTest;

public class ChangeProposalUserRequestHandlerTests
{
    private readonly Mock<DBContext> _mockDbContext;
    private readonly Mock<IUserHelper> _mockUserHelper;
    private readonly IMemoryCache _memoryCache;
    private readonly ChangeProposalUserRequestHandler _handler;

    public ChangeProposalUserRequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
        _mockUserHelper = new Mock<IUserHelper>();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _handler = new ChangeProposalUserRequestHandler(_mockDbContext.Object, _mockUserHelper.Object, _memoryCache);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenProposalDoesNotExist()
    {
        // Arrange
        _mockDbContext.Setup(x => x.Proposals).ReturnsDbSet(new List<Proposal>());


        var request = new ChangeProposalUserRequest { ProposalId = 1, BankStaffId = 2 };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenBankStaffDoesNotExist()
    {
        // Arrange
        var proposals = new List<Proposal> {
          new(){
            Id = 1
            }
        };
        _mockDbContext.Setup(x => x.Proposals).ReturnsDbSet(proposals);

        _mockDbContext.Setup(x => x.BankStaffs).ReturnsDbSet(new List<BankStaff>());

        var request = new ChangeProposalUserRequest { ProposalId = 1, BankStaffId = 2 };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnForbidden_WhenUserHasNoAccessToBranch()
    {
        // Arrange

            var proposals = new List<Proposal>() { new() { Id = 1 } };
            var bankStaffs = new List<BankStaff>() { new() { Id = 2, BranchCode = "ABC123" } };

            _mockDbContext.Setup(x => x.Proposals).ReturnsDbSet(proposals);

            _mockDbContext.Setup(x => x.BankStaffs).ReturnsDbSet(bankStaffs);

            _mockUserHelper.Setup(x => x.GetUserFromToken())
                           .Returns(new UserDto { Id = "1" });

            _memoryCache.Set("1-branches", new List<string> { "ABC124" });

            var request = new ChangeProposalUserRequest { ProposalId = 1, BankStaffId = 2 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(403, result.Code);
            Assert.Equal("شما دسترسی انتقال پرونده به این کاربر را ندارید", result.Message);
            _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
 
    }



    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenProposalIsUpdatedSuccessfully()
    {
        // Arrange
        var proposals = new List<Proposal>() { new() { Id = 1,BankStaffId=3 } };
        var bankStaffs = new List<BankStaff>() { new() { Id = 2, BranchCode = "ABC123" } };

        _mockDbContext.Setup(x => x.Proposals).ReturnsDbSet(proposals);

        _mockDbContext.Setup(x => x.BankStaffs).ReturnsDbSet(bankStaffs);

        _mockUserHelper.Setup(x => x.GetUserFromToken())
                       .Returns(new UserDto { Id = "1" });

        _memoryCache.Set("1-branches", new List<string> { "ABC123" });
        
        _mockDbContext.Setup(x => x.Proposals.Update(It.IsAny<Proposal>()));
        
        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1); // Simulate successful save

        var request = new ChangeProposalUserRequest { ProposalId = 1, BankStaffId = 2 };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(200, result.Code);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockDbContext.Verify(x => x.Proposals.Update(It.IsAny<Proposal>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenSaveChangesFails()
    {
        // Arrange
        var proposals = new List<Proposal>() { new() { Id = 1, BankStaffId = 3 } };
        var bankStaffs = new List<BankStaff>() { new() { Id = 2, BranchCode = "ABC123" } };

        _mockDbContext.Setup(x => x.Proposals).ReturnsDbSet(proposals);

        _mockDbContext.Setup(x => x.BankStaffs).ReturnsDbSet(bankStaffs);

        _mockUserHelper.Setup(x => x.GetUserFromToken())
                       .Returns(new UserDto { Id = "1" });

        _memoryCache.Set("1-branches", new List<string> { "ABC123" });

        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0); // Simulate failed save

        var request = new ChangeProposalUserRequest { ProposalId = 1, BankStaffId = 2 };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}