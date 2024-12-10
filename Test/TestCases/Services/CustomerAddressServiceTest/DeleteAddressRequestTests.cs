using Application.Services.CustomerAddressService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerAddressServiceTest;

public class DeleteAddressRequestHandlerTests
{
    [Fact]
    public async Task Handle_AddressAndCustomerAddressExist_DeletesBoth_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        var fakeCustomerAddress = new CustomerAddress { AddressId = 1 };
        var fakeAddress = new Address { Id = 1 };


        dbContextMock.Setup(c => c.CustomerAddresses).ReturnsDbSet([fakeCustomerAddress]);
        dbContextMock.Setup(c => c.Addresses).ReturnsDbSet([fakeAddress]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeleteAddressRequestHandler(dbContextMock.Object);
        var request = new DeleteAddressRequest { Id = 1 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

    }

    [Fact]
    public async Task Handle_AddressExistsOnly_DeletesAddress_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        var fakeAddress = new Address { Id = 1 };



        dbContextMock.Setup(c => c.CustomerAddresses).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.Addresses).ReturnsDbSet([fakeAddress]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeleteAddressRequestHandler(dbContextMock.Object);
        var request = new DeleteAddressRequest { Id = 1 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

    }

    [Fact]
    public async Task Handle_NeitherAddressNorCustomerAddressExist_ReturnsNotFound()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();


        dbContextMock.Setup(c => c.CustomerAddresses).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.Addresses).ReturnsDbSet([]);

        var handler = new DeleteAddressRequestHandler(dbContextMock.Object);
        var request = new DeleteAddressRequest { Id = 99 }; // Non-existent ID

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }
}