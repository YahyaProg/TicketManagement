using Application.Services.CustomerPhonesService;
using Core.Entities;
using Core.Enums;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerPhonesTests;
public class AddCustomerPhonesRequestHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_AddsPhone_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();
        var fakeCustomerScheme = new CustomerScheme
        {
            Id = 1,
            Customer = new Customer
            {
                IndividualCustomer = new IndividualCustomer()
            }
        };


        dbContextMock.Setup(c => c.CustomerSchemes).ReturnsDbSet([fakeCustomerScheme]);

        dbContextMock.Setup(x => x.CustomerPhones.Add(It.IsAny<CustomerPhone>()));

        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new AddCustomerPhonesRequestHandler(dbContextMock.Object);
        var request = new AddCustomerPhonesRequest
        {
            CustomerSchemeId = 1,
            Pnumber = "123456789",
            Code = "045",
            ContactType = EPhone_contactType.Phone
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_CustomerSchemeNotFound_ReturnsNotFound()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();


        dbContextMock.Setup(c => c.CustomerSchemes).ReturnsDbSet([]);

        var handler = new AddCustomerPhonesRequestHandler(dbContextMock.Object);
        var request = new AddCustomerPhonesRequest
        {
            CustomerSchemeId = 99, // Non-existent CustomerSchemeId
            Pnumber = "987654321",
            Code = "045",
            ContactType = EPhone_contactType.Fax
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
        Assert.Contains("مشتری حقیقی", result.Message);
    }

    [Fact]
    public async Task Handle_SaveChangesFails_ReturnsBadRequest()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();
        var fakeCustomerScheme = new CustomerScheme
        {
            Id = 1,
            Customer = new Customer
            {
                IndividualCustomer = new IndividualCustomer()
            }
        };


        dbContextMock.Setup(c => c.CustomerSchemes).ReturnsDbSet([fakeCustomerScheme]);
        dbContextMock.Setup(x => x.CustomerPhones.Add(It.IsAny<CustomerPhone>()));
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0); // Simulate failure

        var handler = new AddCustomerPhonesRequestHandler(dbContextMock.Object);
        var request = new AddCustomerPhonesRequest
        {
            CustomerSchemeId = 1,
            Pnumber = "123456789",
            Code = "045",
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