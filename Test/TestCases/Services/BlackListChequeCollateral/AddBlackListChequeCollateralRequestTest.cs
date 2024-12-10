using Application.Services.BaseService;
using Application.Services.BlackListChequeCollateralService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.BlackListChequeCollateral;

public class AddBlackListChequeCollateralRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddBlackListChequeCollateralRequest_Fail1()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new AddBlackListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddBlackListChequeCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddBlackListChequeCollateralRequest_Fail2()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.BlackListChequeCollaterals.Add(It.IsAny<Core.Entities.BlackListChequeCollateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddBlackListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddBlackListChequeCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddBlackListChequeCollateralRequest_Success()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.BlackListChequeCollaterals.Add(It.IsAny<Core.Entities.BlackListChequeCollateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddBlackListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddBlackListChequeCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
