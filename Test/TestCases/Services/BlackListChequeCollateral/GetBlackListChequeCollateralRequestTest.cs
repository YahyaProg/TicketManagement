using Application.Services.BlackListChequeCollateralService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.BlackListChequeCollateral;

public class GetBlackListChequeCollateralRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task GetBlackListChequeCollateralRequest_Success()
    {
        moq.Context.Setup(x => x.BlackListChequeCollaterals).ReturnsDbSet([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, CorporateCustomer = new() { Name = "a" } }]);

        var handler = new GetBlackListChequeCollateralRequestHandler(moq.Context.Object);

        var request = new GetBlackListChequeCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }
}
