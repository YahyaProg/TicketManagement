using Application.Services.ClaimCollectActionUnitService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.City;

public class SearchClaimCollectActionUnitRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchClaimCollectActionUnitRequest_Success()
    {
        moq.Context.Setup(x => x.ClaimCollectActionUnits).ReturnsDbSet([new() { OrganizationId = 1 }]);
        moq.Context.Setup(x => x.Organizations).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchClaimCollectActionUnitRequestHandler(moq.Context.Object);

        var request = new SearchClaimCollectActionUnitRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
