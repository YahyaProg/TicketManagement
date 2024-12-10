using Application.Services.RiskInfoSubItemService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.RiskInfoSubItem;

public class SearchRiskInfoSubItemRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchRiskInfoSubItem_Success()
    {
        moq.Context.Setup(x => x.RiskInfoSubItems).ReturnsDbSet([new() { RiskInfoItemId = 1 }]);
        moq.Context.Setup(x => x.RiskInfoItems).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchRiskInfoSubItemRequestHandler(moq.Context.Object);

        var request = new SearchRiskInfoSubItemRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
