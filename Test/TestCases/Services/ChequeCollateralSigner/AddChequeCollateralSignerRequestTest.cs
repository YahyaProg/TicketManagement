using Application.Services.BaseService;
using Application.Services.ChequeCollateralSignerService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ChequeCollateralSigner;

public class AddChequeCollateralSignerRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddChequeCollateralSignerRequest_Fail1()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new AddChequeCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddChequeCollateralSignerRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddChequeCollateralSignerRequest_Fail2()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.ChequeCollateralSigners.Add(It.IsAny<Core.Entities.ChequeCollateralSigner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddChequeCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddChequeCollateralSignerRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddChequeCollateralSignerRequest_Success()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.ChequeCollateralSigners.Add(It.IsAny<Core.Entities.ChequeCollateralSigner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddChequeCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddChequeCollateralSignerRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
