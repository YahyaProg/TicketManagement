using Application.Services.WhiteListChequeCollateralService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.WhiteListChequeCollateral;

public class GetWhiteListChequeCollateralRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task GetWhiteListChequeCollateralRequest_Success()
    {
        moq.Context.Setup(x => x.WhiteListChequeCollaterals).ReturnsDbSet([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, CorporateCustomer = new() { Name = "a" } }]);

        var handler = new GetWhiteListChequeCollateralRequestHandler(moq.Context.Object);

        var request = new GetWhiteListChequeCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }
}
