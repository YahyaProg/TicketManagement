using Application.Services.CustomerEmailsService;
using Core.Const;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerEmailsTests;



public class AddCustomerEmailsRequestHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_AddsEmail_ReturnsSuccess()
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
        dbContextMock.Setup(c => c.CustomerEmails).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new AddCustomerEmailsRequestHandler(dbContextMock.Object);
        var request = new AddCustomerEmailsRequest
        {
            CustomerId = 1,
            CustomerSchemeId = 1,
            Email = "test@example.com"
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

        var handler = new AddCustomerEmailsRequestHandler(dbContextMock.Object);
        var request = new AddCustomerEmailsRequest
        {
            CustomerId = 1,
            CustomerSchemeId = 999, // Non-existent ID
            Email = "test@example.com"
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
        Assert.Equal(ResultMessage.Error.notFound("مشتری حقیقی"), result.Message);
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
        dbContextMock.Setup(c => c.CustomerEmails).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0); // Simulate save failure

        var handler = new AddCustomerEmailsRequestHandler(dbContextMock.Object);
        var request = new AddCustomerEmailsRequest
        {
            CustomerId = 1,
            CustomerSchemeId = 1,
            Email = "test@example.com"
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}