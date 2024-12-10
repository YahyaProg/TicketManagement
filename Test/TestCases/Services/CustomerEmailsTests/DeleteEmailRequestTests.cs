using Application.Services.CustomerEmailsService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerEmailsTests;

public class DeleteEmailRequestHandlerTests
{
    [Fact]
    public async Task Handle_EmailAndCustomerEmailExist_DeletesBoth_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        var fakeCustomerEmail = new CustomerEmail { EmailId = 1 };
        var fakeEmail = new Email { Id = 1 };


        dbContextMock.Setup(c => c.CustomerEmails).ReturnsDbSet([fakeCustomerEmail]);
        dbContextMock.Setup(c => c.Emails).ReturnsDbSet([fakeEmail]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeleteEmailRequestHandler(dbContextMock.Object);
        var request = new DeleteEmailRequest { Id = 1 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_EmailExistsOnly_DeletesEmail_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        var fakeEmail = new Email { Id = 1 };



        dbContextMock.Setup(c => c.CustomerEmails).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.Emails).ReturnsDbSet([fakeEmail]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeleteEmailRequestHandler(dbContextMock.Object);
        var request = new DeleteEmailRequest { Id = 1 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_NeitherEmailNorCustomerEmailExist_ReturnsNotFound()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();


        dbContextMock.Setup(c => c.CustomerEmails).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.Emails).ReturnsDbSet([]);

        var handler = new DeleteEmailRequestHandler(dbContextMock.Object);
        var request = new DeleteEmailRequest { Id = 99 }; // Non-existent ID

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }
}