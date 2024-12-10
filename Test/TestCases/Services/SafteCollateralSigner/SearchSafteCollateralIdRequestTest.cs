using Application.Services.SafteCollateralSignerService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.SafteCollateralSigner;

public class SearchSafteCollateralSignerRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchSafteCollateralSignerRequest_Success()
    {
        moq.Context.Setup(x => x.SafteCollateralSigners).ReturnsDbSet([new() { SignerId = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new SearchSafteCollateralSignerRequestHandler(moq.Context.Object);

        var request = new SearchSafteCollateralSignerRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
