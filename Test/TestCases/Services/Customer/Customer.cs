using Application.Services.CustomerService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerTest;


public class CustomerRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    private readonly Mock<DBContext> context = new();

    [Fact]
    public void AddCustomerRequest_Success() =>
        Assert.NotNull(new AddCustomerRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteCustomerRequest_Success() =>
        Assert.NotNull(new DeleteCustomerRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdCustomerRequest_Success() =>
        Assert.NotNull(new GetCustomerRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateCustomerRequest_Success() =>
        Assert.NotNull(new UpdateCustomerRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchCustomerRequest_Success() =>
        Assert.NotNull(new SearchCustomerRequestHandler(_unitOfWork.Object));

    [Fact]
    public async Task UpdateCustomerGeneralInformationRequest_Fail1()
    {
        var command = new UpdateCustomerGeneralInformationRequest { CurrencyId = 0 };

        context.Setup(x => x.Customers).ReturnsDbSet([]);
        
        context.Setup(x => x.CorporateCustomers).ReturnsDbSet([]);

        var handler = new UpdateCustomerGeneralInformationRequestHandler(context.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    [Obsolete]
    public async Task UpdateCustomerGeneralInformationRequest_Fail2()
    {
        var command = new UpdateCustomerGeneralInformationRequest { CustomerId = 1 , CurrencyId = 0 };

        var customers = new List<Core.Entities.Customer> { new()
        {
            Id = command.CustomerId.Value
        } };

        context.Setup(x => x.Customers).ReturnsDbSet(customers);

        var corporateCustomers = new List<CorporateCustomer>
        {
            new()
            {
                Id = command.CustomerId.Value,
            }
        };

        context.Setup(x=> x.CorporateCustomers).ReturnsDbSet(corporateCustomers);

        context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCustomerGeneralInformationRequestHandler(context.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    [Obsolete]
    public async Task UpdateCustomerGeneralInformationRequest_Success()
    {
        var command = new UpdateCustomerGeneralInformationRequest { CustomerId = 1, CurrencyId = 0 };

        var customers = new List<Core.Entities.Customer> { new()
        {
            Id = command.CustomerId.Value
        } };

        context.Setup(x => x.Customers).ReturnsDbSet(customers);

        var corporateCustomers = new List<CorporateCustomer>
        {
            new()
            {
                Id = command.CustomerId.Value,
            }
        };

        context.Setup(x => x.CorporateCustomers).ReturnsDbSet(corporateCustomers);

        context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCustomerGeneralInformationRequestHandler(context.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
