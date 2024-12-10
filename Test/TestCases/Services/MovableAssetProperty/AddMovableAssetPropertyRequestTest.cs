using Application.Services.MovableAssetPropertyService;
using Core.Enums;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.MovableAssetProperty;

public class AddMovableAssetPropertyRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddMovableAssetPropertyRequest_Fail()
    {
        moq.Context.Setup(x => x.MovableAssetProperties.Add(It.IsAny<Core.Entities.MovableAssetProperty>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddMovableAssetPropertyRequestHandler(moq.Context.Object);

        var request = new AddMovableAssetPropertyRequest { MovableAssetId = 1, Type = EMovableAssetProperty_type.Combo };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddMovableAssetPropertyRequest_Success()
    {
        moq.Context.Setup(x => x.MovableAssetProperties.Add(It.IsAny<Core.Entities.MovableAssetProperty>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddMovableAssetPropertyRequestHandler(moq.Context.Object);

        var request = new AddMovableAssetPropertyRequest { MovableAssetId = 1, Type = EMovableAssetProperty_type.String };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
