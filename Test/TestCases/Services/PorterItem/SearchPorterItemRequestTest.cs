using Application.Services.PorterItemService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.PorterItem;

public class SearchPorterItemRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchPorterItemRequest_Success()
    {
        moq.Context.Setup(x => x.PorterItems).ReturnsDbSet([new() { PorterGroupId = 1 }]);
        moq.Context.Setup(x => x.PorterGroups).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchPorterItemRequestHandler(moq.Context.Object);

        var request = new SearchPorterItemRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
