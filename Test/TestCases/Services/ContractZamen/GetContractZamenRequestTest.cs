using Application.Services.ContractZamenService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ContractZamen;

public class GetContractZamenRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task GetContractZamenRequest_Success()
    {
        moq.Context.Setup(x => x.ContractZamens).ReturnsDbSet([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, CorporateCustomer = new() { Name = "a" } }]);

        var handler = new GetContractZamenRequestHandler(moq.Context.Object);

        var request = new GetContractZamenRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }
}
