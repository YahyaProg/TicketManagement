using Application.Services.BaseService;
using Application.Services.WhiteListChequeCollateralService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.WhiteListChequeCollateral;

public class UpdateWhiteListChequeCollateralRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateWhiteListChequeCollateralRequest_Fail1()
    {
        moq.Context.Setup(x => x.WhiteListChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateWhiteListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateWhiteListChequeCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateWhiteListChequeCollateralRequest_Fail2()
    {
        moq.Context.Setup(x => x.WhiteListChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new UpdateWhiteListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateWhiteListChequeCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateWhiteListChequeCollateralRequest_Fail3()
    {
        moq.Context.Setup(x => x.WhiteListChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.WhiteListChequeCollaterals.Update(It.IsAny<Core.Entities.WhiteListChequeCollateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateWhiteListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateWhiteListChequeCollateralRequest { Id = 1, ChequeCollateralId = 1, MaxAmount = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateWhiteListChequeCollateralRequest_Success()
    {
        moq.Context.Setup(x => x.WhiteListChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.WhiteListChequeCollaterals.Update(It.IsAny<Core.Entities.WhiteListChequeCollateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateWhiteListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateWhiteListChequeCollateralRequest { Id = 1, ChequeCollateralId = 1, MaxAmount = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
