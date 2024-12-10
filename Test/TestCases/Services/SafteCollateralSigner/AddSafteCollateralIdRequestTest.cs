using Application.Services.BaseService;
using Application.Services.SafteCollateralSignerService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.SafteCollateralSigner;

public class AddSafteCollateralSignerRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddSafteCollateralSignerRequest_Fail1()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new AddSafteCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddSafteCollateralSignerRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddSafteCollateralSignerRequest_Fail2()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.SafteCollateralSigners.Add(It.IsAny<Core.Entities.SafteCollateralSigner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddSafteCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddSafteCollateralSignerRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddSafteCollateralSignerRequest_Success()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.SafteCollateralSigners.Add(It.IsAny<Core.Entities.SafteCollateralSigner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddSafteCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddSafteCollateralSignerRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
