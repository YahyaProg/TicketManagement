using Application.Services.MovableAssetPropertyService;
using Core.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.MovableAssetProperty;

public class UpdateMovableAssetPropertyRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateMovableAssetPropertyRequest_Fail1()
    {
        moq.Context.Setup(x => x.MovableAssetProperties).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateMovableAssetPropertyRequestHandler(moq.Context.Object);

        var request = new UpdateMovableAssetPropertyRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateMovableAssetPropertyRequest_Fail2()
    {
        moq.Context.Setup(x => x.MovableAssetProperties).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.MovableAssetProperties.Update(It.IsAny<Core.Entities.MovableAssetProperty>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateMovableAssetPropertyRequestHandler(moq.Context.Object);

        var request = new UpdateMovableAssetPropertyRequest { Id = 1, MovableAssetId = 1, Type = EMovableAssetProperty_type.Combo };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateMovableAssetPropertyRequest_Success()
    {
        moq.Context.Setup(x => x.MovableAssetProperties).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.MovableAssetProperties.Update(It.IsAny<Core.Entities.MovableAssetProperty>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateMovableAssetPropertyRequestHandler(moq.Context.Object);

        var request = new UpdateMovableAssetPropertyRequest { Id = 1, MovableAssetId = 1, Type = EMovableAssetProperty_type.String };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
