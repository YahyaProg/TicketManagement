using Application.Services.Manager;
using Core.Entities;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Managers;

public class GetMajorStockHolderRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task GetMajorStockHolderRequest_Success()
    {
        moq.Context.Setup(x => x.MajorStocksHolders).ReturnsDbSet([new() { Id = 1, PersonId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet(
            [new() { Id = 1, IndividualCustomer = new IndividualCustomer { FirstName = "a" } }]);

        var handler = new GetMajorStockHolderRequestHandler(moq.Context.Object);

        var request = new GetMajorStockHolderRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }
}
