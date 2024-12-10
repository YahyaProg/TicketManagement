using Application.Services.IndividualCustomerService;
using Core.Entities;
using Core.Enums;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.IndividualCustomerServicesTest;


public class ChangeIndividualTaxTypRequestHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_UpdatesTaxType_ReturnsSuccess()
    {
        // Arrange
        var customerId = 1001L;
        var taxType = EIndividualCustomer_taxType.B;

        var dbContextMock = new Mock<DBContext>();

        var existingCustomer = new IndividualCustomer { Id = customerId, TaxType = EIndividualCustomer_taxType.A };



        dbContextMock.Setup(c => c.IndividualCustomers).ReturnsDbSet([existingCustomer]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new ChangeIndividualTaxTypRequestHandler(dbContextMock.Object);
        var request = new ChangeIndividualTaxTypRequest
        {
            CustomerId = customerId,
            TaxType = taxType
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(taxType, existingCustomer.TaxType);
    }

    [Fact]
    public async Task Handle_CustomerNotFound_ReturnsNotFound()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        var existingCustomer = new IndividualCustomer { Id = 2, TaxType = EIndividualCustomer_taxType.A };
        dbContextMock.Setup(c => c.IndividualCustomers).ReturnsDbSet([]);

        var handler = new ChangeIndividualTaxTypRequestHandler(dbContextMock.Object);
        var request = new ChangeIndividualTaxTypRequest
        {
            CustomerId = 9999L, // Non-existing customer ID
            TaxType = EIndividualCustomer_taxType.B
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
        var customerId = 1001L;
        var taxType = EIndividualCustomer_taxType.A;

        var dbContextMock = new Mock<DBContext>();

        var existingCustomer = new IndividualCustomer { Id = customerId, TaxType = EIndividualCustomer_taxType.A };

        dbContextMock.Setup(c => c.IndividualCustomers).ReturnsDbSet([existingCustomer]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0); // Simulate save failure

        var handler = new ChangeIndividualTaxTypRequestHandler(dbContextMock.Object);
        var request = new ChangeIndividualTaxTypRequest
        {
            CustomerId = customerId,
            TaxType = taxType
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}