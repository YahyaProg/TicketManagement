using Application.Services.CustomerPhonesService;
using Core.Entities;
using Core.Enums;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
namespace Test.TestCases.Services.CustomerPhonesTests;



public class UpdateCustomerPhoneRequestHandlerTests
{
    [Fact]
    public async Task Handle_CustomerPhoneExists_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();
        var fakePhone = new Phone
        {
            Id = 1,
            Code = "123",
            Pnumber = "987654321",
            ContactType = EPhone_contactType.Mobile
        };

        dbContextMock.Setup(c => c.Phones).ReturnsDbSet([fakePhone]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new UpdateCustomerPhoneRequestHandler(dbContextMock.Object);
        var request = new UpdateCustomerPhoneRequest
        {
            ID = 1,
            Code = "456",
            Pnumber = "123456789",
            ContactType = EPhone_contactType.Phone
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_CustomerPhoneNotFound_ReturnsNotFound()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        dbContextMock.Setup(c => c.Phones).ReturnsDbSet([]);

        var handler = new UpdateCustomerPhoneRequestHandler(dbContextMock.Object);
        var request = new UpdateCustomerPhoneRequest
        {
            ID = 99, // Non-existent ID
            Code = "123",
            Pnumber = "987654321",
            ContactType = EPhone_contactType.Mobile
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }

    [Fact]
    public async Task Handle_SaveChangesFails_ReturnsBadRequest()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();
        var fakePhone = new Phone
        {
            Id = 1,
            Code = "123",
            Pnumber = "987654321",
            ContactType = EPhone_contactType.Mobile
        };


        dbContextMock.Setup(c => c.Phones).ReturnsDbSet([fakePhone]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        var handler = new UpdateCustomerPhoneRequestHandler(dbContextMock.Object);
        var request = new UpdateCustomerPhoneRequest
        {
            ID = 1,
            Code = "456",
            Pnumber = "123456789",
            ContactType = EPhone_contactType.Phone
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}