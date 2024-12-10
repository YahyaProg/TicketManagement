using Application.Services.SafteCollateralSignerService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.SafteCollateralSigner;

public class GetSafteCollateralSignerRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task GetSafteCollateralSignerRequest_Success()
    {
        moq.Context.Setup(x => x.SafteCollateralSigners).ReturnsDbSet([new() { Id = 1, SignerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, CorporateCustomer = new() { Name = "a" } }]);

        var handler = new GetSafteCollateralSignerRequestHandler(moq.Context.Object);

        var request = new GetSafteCollateralSignerRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }
}
