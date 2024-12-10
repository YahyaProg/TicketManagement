using Application.Services.WorkPlaceTypeService;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.WorkPlaceTypeTest;

public class AddWorkPlaceTypeRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddWorkPlaceType_Fail1()
    {
        moq.Context.Setup(x => x.WorkPlaceTypes).ReturnsDbSet([new() { Title = "x" }]);

        var handler = new AddWorkPlaceTypeRequestHandler(moq.Context.Object);

        var request = new AddWorkPlaceTypeRequest { Title = "x" };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddWorkPlaceType_Fail2()
    {
        moq.Context.Setup(x => x.WorkPlaceTypes).ReturnsDbSet([new() { Title = "x" }]);
        moq.Context.Setup(x => x.WorkPlaceTypes.Add(It.IsAny<Core.Entities.WorkPlaceType>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddWorkPlaceTypeRequestHandler(moq.Context.Object);

        var request = new AddWorkPlaceTypeRequest { Title = "y" };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddWorkPlaceType_Success()
    {
        moq.Context.Setup(x => x.WorkPlaceTypes).ReturnsDbSet([new() { Title = "x" }]);
        moq.Context.Setup(x => x.WorkPlaceTypes.Add(It.IsAny<Core.Entities.WorkPlaceType>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddWorkPlaceTypeRequestHandler(moq.Context.Object);

        var request = new AddWorkPlaceTypeRequest { Title = "y" };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
