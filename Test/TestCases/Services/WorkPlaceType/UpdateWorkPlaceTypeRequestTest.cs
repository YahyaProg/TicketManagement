using Application.Services.WorkPlaceTypeService;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.WorkPlaceTypeTest;

public class UpdateWorkPlaceTypeRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateWorkPlaceTypeRequest_Fail1()
    {
        moq.Context.Setup(x => x.WorkPlaceTypes).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new UpdateWorkPlaceTypeRequestHandler(moq.Context.Object);

        var request = new UpdateWorkPlaceTypeRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateWorkPlaceTypeRequest_Fail2()
    {
        moq.Context.Setup(x => x.WorkPlaceTypes).ReturnsDbSet([new() { Id = 1 }, new() { Title = "1" }]);

        var handler = new UpdateWorkPlaceTypeRequestHandler(moq.Context.Object);

        var request = new UpdateWorkPlaceTypeRequest { Id = 1, Title = "1" };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateWorkPlaceTypeRequest_Fail3()
    {
        moq.Context.Setup(x => x.WorkPlaceTypes).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.WorkPlaceTypes.Update(It.IsAny<Core.Entities.WorkPlaceType>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateWorkPlaceTypeRequestHandler(moq.Context.Object);

        var request = new UpdateWorkPlaceTypeRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateWorkPlaceTypeRequest_Success()
    {
        moq.Context.Setup(x => x.WorkPlaceTypes).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.WorkPlaceTypes.Update(It.IsAny<Core.Entities.WorkPlaceType>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateWorkPlaceTypeRequestHandler(moq.Context.Object);

        var request = new UpdateWorkPlaceTypeRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
