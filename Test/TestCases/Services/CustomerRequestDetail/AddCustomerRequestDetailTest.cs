using Application.Services.CustomerRequestDetailService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;


namespace Test.TestCases.Services.CustomerRequestDetailTest;
public class AddCustomerRequestDetailRequestHandlerTests
{
    private readonly Mock<DBContext> _mockDbContext;
    private readonly AddCustomerRequestDetailRequestHandler _handler;

    public AddCustomerRequestDetailRequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
        _handler = new AddCustomerRequestDetailRequestHandler(_mockDbContext.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRequestIsValidAndSaved()
    {
        // Arrange
        var customerRequestDetailList = new List<CustomerRequestDetail>();
        _mockDbContext.Setup(x => x.CustomerRequestDetails).ReturnsDbSet(customerRequestDetailList);

        var individualCustomerList = new List<IndividualCustomer>
        {
            new IndividualCustomer { Id = 1 }
        };

        _mockDbContext.Setup(x => x.IndividualCustomers).ReturnsDbSet(individualCustomerList);

        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _mockDbContext.Setup(x => x.CreditTypes).ReturnsDbSet([
          new CreditType(){Id = 1, Code = Core.Enums.ECreditType_code.bg}
          ]);

        var request = new AddCustomerRequestDetailRequest
        {
            CustomerId = 1,
            InstallmentAmount = 1000,
            CreditTypeId = 1
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockDbContext.Verify(x => x.CustomerRequestDetails.Add(It.IsAny<CustomerRequestDetail>()), Times.Once);
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

        var request = new AddCustomerRequestDetailRequest
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
        _mockDbContext.Verify(x => x.CustomerRequestDetails.Add(It.IsAny<CustomerRequestDetail>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCustomerIsNotIndividual()
    {
        // Arrange
        var individualCustomers = new List<IndividualCustomer>();
        _mockDbContext.Setup(x => x.IndividualCustomers).ReturnsDbSet(individualCustomers);

        var customerRequestDetailList = new List<CustomerRequestDetail>();
        _mockDbContext.Setup(x => x.CustomerRequestDetails).ReturnsDbSet(customerRequestDetailList);
        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _mockDbContext.Setup(x => x.CreditTypes).ReturnsDbSet([
         new CreditType(){Id = 1, Code = Core.Enums.ECreditType_code.bg}
         ]);


        var request = new AddCustomerRequestDetailRequest
        {
            CustomerId = 2,
            InstallmentAmount = null, // Not required for non-individual customers
            CreditTypeId = 1
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockDbContext.Verify(x => x.CustomerRequestDetails.Add(It.IsAny<CustomerRequestDetail>()), Times.Once);
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
        _mockDbContext.Setup(x => x.CustomerRequestDetails.Add(It.IsAny<CustomerRequestDetail>()));
        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0); // Simulate save failure
        _mockDbContext.Setup(x => x.CreditTypes).ReturnsDbSet([
          new CreditType(){Id = 1, Code = Core.Enums.ECreditType_code.lic}
          ]);

        var request = new AddCustomerRequestDetailRequest
        {
            CustomerId = 1,
            InstallmentAmount = 1000,
            CreditTypeId = 1
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }


}
