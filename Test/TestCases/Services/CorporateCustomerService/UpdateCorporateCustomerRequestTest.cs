using Application.Services.CorporateCustomerService;
using Core.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.CorporateCustomerService;

public class UpdateCorporateCustomerRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateCorporateCustomerRequest_Fail1()
    {
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new UpdateCorporateCustomerRequestHandler(moq.Context.Object);

        var request = new UpdateCorporateCustomerRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateCorporateCustomerRequest_Fail2()
    {
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1 }, new() { ClientNo = "1" }]);

        var handler = new UpdateCorporateCustomerRequestHandler(moq.Context.Object);

        var request = new UpdateCorporateCustomerRequest { Id = 1, ClientNo = "1" };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateCorporateCustomerRequest_Fail3()
    {
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, CorporateCustomer = new CorporateCustomer() }]);
        moq.Context.Setup(x => x.Customers.Update(It.IsAny<Core.Entities.Customer>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCorporateCustomerRequestHandler(moq.Context.Object);

        var request = new UpdateCorporateCustomerRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateCorporateCustomerRequest_Success()
    {
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, CorporateCustomer = new CorporateCustomer() }]);
        moq.Context.Setup(x => x.Customers.Update(It.IsAny<Core.Entities.Customer>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCorporateCustomerRequestHandler(moq.Context.Object);

        var request = new UpdateCorporateCustomerRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }
}
