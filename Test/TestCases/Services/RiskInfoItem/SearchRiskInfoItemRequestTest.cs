using Application.Services.RiskInfoItemService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.RiskInfoItem;

public class SearchRiskInfoItemRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchRiskInfoItem_Success()
    {
        moq.Context.Setup(x => x.RiskInfoItems).ReturnsDbSet([new() { RiskInfoGroupId = 1 }]);
        moq.Context.Setup(x => x.RiskInfoGroups).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchRiskInfoItemRequestHandler(moq.Context.Object);

        var request = new SearchRiskInfoItemRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
