using Application.Services.ChequeCollateralSignerService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ChequeCollateralSigner;

public class SearchChequeCollateralSignerRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchChequeCollateralSignerRequest_Success()
    {
        moq.Context.Setup(x => x.ChequeCollateralSigners).ReturnsDbSet([new() { SignerId = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new SearchChequeCollateralSignerRequestHandler(moq.Context.Object);

        var request = new SearchChequeCollateralSignerRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
