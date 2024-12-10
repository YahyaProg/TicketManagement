using Application.Services.BaseService;
using Application.Services.WhiteListChequeCollateralService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.WhiteListChequeCollateral;

public class AddWhiteListChequeCollateralRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddWhiteListChequeCollateralRequest_Fail1()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new AddWhiteListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddWhiteListChequeCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddWhiteListChequeCollateralRequest_Fail2()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.WhiteListChequeCollaterals.Add(It.IsAny<Core.Entities.WhiteListChequeCollateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddWhiteListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddWhiteListChequeCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddWhiteListChequeCollateralRequest_Success()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.WhiteListChequeCollaterals.Add(It.IsAny<Core.Entities.WhiteListChequeCollateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddWhiteListChequeCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddWhiteListChequeCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
