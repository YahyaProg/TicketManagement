using Application.Services.BlackListChequeCollateralService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.BlackListChequeCollateral;

public class SearchBlackListChequeCollateralRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchBlackListChequeCollateralRequest_Success()
    {
        moq.Context.Setup(x => x.BlackListChequeCollaterals).ReturnsDbSet([new() { CustomerId = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new SearchBlackListChequeCollateralRequestHandler(moq.Context.Object);

        var request = new SearchBlackListChequeCollateralRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
