using Application.Services.WhiteListChequeCollateralService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.WhiteListChequeCollateral;

public class SearchWhiteListChequeCollateralRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchWhiteListChequeCollateralRequest_Success()
    {
        moq.Context.Setup(x => x.WhiteListChequeCollaterals).ReturnsDbSet([new() { CustomerId = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new SearchWhiteListChequeCollateralRequestHandler(moq.Context.Object);

        var request = new SearchWhiteListChequeCollateralRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
