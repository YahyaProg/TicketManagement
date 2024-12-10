using Application.Services.CustomerPhonesService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerPhonesTests;

public class DeletePhoneRequestHandlerTests
{
    [Fact]
    public async Task Handle_PhoneAndCustomerPhoneExist_DeletesBoth_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        var fakeCustomerPhone = new CustomerPhone { PhoneId = 1 };
        var fakePhone = new Phone { Id = 1 };


        dbContextMock.Setup(c => c.CustomerPhones).ReturnsDbSet([fakeCustomerPhone]);
        dbContextMock.Setup(c => c.Phones).ReturnsDbSet([fakePhone]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeletePhoneRequestHandler(dbContextMock.Object);
        var request = new DeletePhoneRequest { Id = 1 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_PhoneExistsOnly_DeletesPhone_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        var fakePhone = new Phone { Id = 1 };


        dbContextMock.Setup(c => c.CustomerPhones).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.Phones).ReturnsDbSet([fakePhone]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeletePhoneRequestHandler(dbContextMock.Object);
        var request = new DeletePhoneRequest { Id = 1 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_NeitherPhoneNorCustomerPhoneExist_ReturnsNotFound()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();


        dbContextMock.Setup(c => c.CustomerPhones).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.Phones).ReturnsDbSet([]);

        var handler = new DeletePhoneRequestHandler(dbContextMock.Object);
        var request = new DeletePhoneRequest { Id = 99 }; // Non-existent ID

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }
}
