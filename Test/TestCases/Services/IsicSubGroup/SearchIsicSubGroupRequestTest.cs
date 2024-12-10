using Application.Services.IsicSubGroupService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.IsicSubGroupTests;

public class SearchIsicSubGroupRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchIsicSubGroupRequest_Success()
    {
        moq.Context.Setup(x => x.IsicSubGroups).ReturnsDbSet([new() { IsicGroupId = 1 }]);
        moq.Context.Setup(x => x.IsicGroups).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchIsicSubGroupRequestHandler(moq.Context.Object);

        var request = new SearchIsicSubGroupRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
