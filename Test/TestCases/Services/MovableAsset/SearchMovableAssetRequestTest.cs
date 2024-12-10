using Application.Services.MovableAssetService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.MovableAsset;

public class SearchMovableAssetRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchMovableAssetRequest_Success()
    {
        moq.Context.Setup(x => x.MovableAssets).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.MovableAssetProperties).ReturnsDbSet([new() { MovableAssetId = 1 }]);

        var handler = new SearchMovableAssetRequestHandler(moq.Context.Object);

        var request = new SearchMovableAssetRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
