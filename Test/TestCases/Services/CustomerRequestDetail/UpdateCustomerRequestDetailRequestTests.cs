using Application.Services.CustomerRequestDetailService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerRequestDetailTest;

public class UpdateCustomerRequestDetailRequestHandlerTests
{
    private readonly Mock<DBContext> _mockDbContext;
    private readonly UpdateCustomerRequestDetailRequestHandler _handler;

    public UpdateCustomerRequestDetailRequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
        _handler = new UpdateCustomerRequestDetailRequestHandler(_mockDbContext.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRequestIsValidAndSaved()
    {
        // Arrange
        _mockDbContext.Setup(x => x.CustomerRequestDetails).ReturnsDbSet(new List<CustomerRequestDetail>());

        var individualCustomerList = new List<IndividualCustomer>
        {
            new IndividualCustomer { Id = 1 }
        };
        _mockDbContext.Setup(x => x.IndividualCustomers).ReturnsDbSet(individualCustomerList);


        _mockDbContext.Setup(x => x.CustomerRequestDetails.Update(It.IsAny<CustomerRequestDetail>()));
        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var request = new UpdateCustomerRequestDetailRequest
        {
            CustomerId = 1,
            InstallmentAmount = 1000
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockDbContext.Verify(x => x.CustomerRequestDetails.Update(It.IsAny<CustomerRequestDetail>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenInstallmentAmountIsNullForIndividualCustomer()
    {
        // Arrange
        var individualCustomerList = new List<IndividualCustomer>
        {
            new IndividualCustomer { Id = 1 }
        };

        _mockDbContext.Setup(x => x.IndividualCustomers).ReturnsDbSet(individualCustomerList);

        var request = new UpdateCustomerRequestDetailRequest
        {
            CustomerId = 1,
            InstallmentAmount = null
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(406, result.Code);
        Assert.Equal("مبلغ اقساط را وارد کنید", result.Message);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _mockDbContext.Verify(x => x.CustomerRequestDetails.Update(It.IsAny<CustomerRequestDetail>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenSaveChangesFails()
    {
        // Arrange
        var individualCustomerList = new List<IndividualCustomer>
        {
            new IndividualCustomer { Id = 1 }
        };

        _mockDbContext.Setup(x => x.IndividualCustomers).ReturnsDbSet(individualCustomerList);

        _mockDbContext.Setup(x => x.CustomerRequestDetails.Update(It.IsAny<CustomerRequestDetail>()));
        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0); // Simulate save failure

        var request = new UpdateCustomerRequestDetailRequest
        {
            CustomerId = 1,
            InstallmentAmount = 1000
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


}
