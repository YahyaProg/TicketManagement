using Application.Services.BaseService;
using Application.Services.BlackListChequeCollateralService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.BlackListChequeCollateral;

public class UpdateBlackListChequeCollateralRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateBlackListChequeCollateralRequest_Fail1()
    {
        moq.Context.Setup(x => x.BlackListChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateBlackListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateBlackListChequeCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateBlackListChequeCollateralRequest_Fail2()
    {
        moq.Context.Setup(x => x.BlackListChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new UpdateBlackListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateBlackListChequeCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateBlackListChequeCollateralRequest_Fail3()
    {
        moq.Context.Setup(x => x.BlackListChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.BlackListChequeCollaterals.Update(It.IsAny<Core.Entities.BlackListChequeCollateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateBlackListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateBlackListChequeCollateralRequest { Id = 1, ChequeCollateralId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateBlackListChequeCollateralRequest_Success()
    {
        moq.Context.Setup(x => x.BlackListChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.BlackListChequeCollaterals.Update(It.IsAny<Core.Entities.BlackListChequeCollateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateBlackListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateBlackListChequeCollateralRequest { Id = 1, ChequeCollateralId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
