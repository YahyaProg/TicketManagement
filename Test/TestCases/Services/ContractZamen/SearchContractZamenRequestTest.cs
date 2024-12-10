using Application.Services.ContractZamenService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ContractZamen;

public class SearchContractZamenRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchContractZamenRequest_Success()
    {
        moq.Context.Setup(x => x.ContractZamens).ReturnsDbSet([new() { CustomerId = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new SearchContractZamenRequestHandler(moq.Context.Object);

        var request = new SearchContractZamenRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
