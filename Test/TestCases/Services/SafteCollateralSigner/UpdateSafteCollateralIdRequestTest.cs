using Application.Services.BaseService;
using Application.Services.SafteCollateralSignerService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.SafteCollateralSigner;

public class UpdateSafteCollateralSignerRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateSafteCollateralSignerRequest_Fail1()
    {
        moq.Context.Setup(x => x.SafteCollateralSigners).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateSafteCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateSafteCollateralSignerRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateSafteCollateralSignerRequest_Fail2()
    {
        moq.Context.Setup(x => x.SafteCollateralSigners).ReturnsDbSet([new() { Id = 1 }]);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new UpdateSafteCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateSafteCollateralSignerRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateSafteCollateralSignerRequest_Fail3()
    {
        moq.Context.Setup(x => x.SafteCollateralSigners).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.SafteCollateralSigners.Update(It.IsAny<Core.Entities.SafteCollateralSigner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateSafteCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateSafteCollateralSignerRequest { Id = 1, SafteCollateralId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateSafteCollateralSignerRequest_Success()
    {
        moq.Context.Setup(x => x.SafteCollateralSigners).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.SafteCollateralSigners.Update(It.IsAny<Core.Entities.SafteCollateralSigner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateSafteCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateSafteCollateralSignerRequest { Id = 1, SafteCollateralId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
