using Application.Services.ChequeCollateralSignerService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ChequeCollateralSigner;

public class GetChequeCollateralSignerRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task GetChequeCollateralSignerRequest_Success()
    {
        moq.Context.Setup(x => x.ChequeCollateralSigners).ReturnsDbSet([new() { Id = 1, SignerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, CorporateCustomer = new() { Name = "a" } }]);

        var handler = new GetChequeCollateralSignerRequestHandler(moq.Context.Object);

        var request = new GetChequeCollateralSignerRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }
}
