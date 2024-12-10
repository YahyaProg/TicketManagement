using Application.Services.CurrentChequeConfigService;
using Core.Entities;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.RiskInfoQuestions;

public class DeleteCurrentChequeConfigRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task DeleteCurrentChequeConfigRequest_Fail1()
    {
        moq.UnitOfWork.Setup(x => x.BlackListChequeConfigRepo.getByChequeCollateralID(It.IsAny<long>())).ReturnsAsync(1);
        moq.UnitOfWork.Setup(x => x.WhiteListChequeConfigRepo.getByChequeCollateralID(It.IsAny<long>())).ReturnsAsync(1);

        var handler = new DeleteCurrentChequeConfigRequestHandler(moq.UnitOfWork.Object);

        var request = new DeleteCurrentChequeConfigRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeleteCurrentChequeConfigRequest_Fail2()
    {
        moq.UnitOfWork.Setup(x => x.BlackListChequeConfigRepo.getByChequeCollateralID(It.IsAny<long>())).ReturnsAsync(0);
        moq.UnitOfWork.Setup(x => x.WhiteListChequeConfigRepo.getByChequeCollateralID(It.IsAny<long>())).ReturnsAsync(1);

        var handler = new DeleteCurrentChequeConfigRequestHandler(moq.UnitOfWork.Object);

        var request = new DeleteCurrentChequeConfigRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeleteCurrentChequeConfigRequest_Fail3()
    {        
        moq.UnitOfWork.Setup(x => x.BlackListChequeConfigRepo.getByChequeCollateralID(It.IsAny<long>())).ReturnsAsync(0);
        moq.UnitOfWork.Setup(x => x.WhiteListChequeConfigRepo.getByChequeCollateralID(It.IsAny<long>())).ReturnsAsync(0);
        moq.Context.Setup(x => x.CurrentChequeConfigs.FindAsync(It.IsAny<object>()));
        moq.UnitOfWork.Setup(x => x.Context.Remove(It.IsAny<CurrentChequeConfig>()));
        moq.UnitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new DeleteCurrentChequeConfigRequestHandler(moq.UnitOfWork.Object);

        var request = new DeleteCurrentChequeConfigRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeleteCurrentChequeConfigRequest_Success()
    {
        moq.UnitOfWork.Setup(x => x.BlackListChequeConfigRepo.getByChequeCollateralID(It.IsAny<long>())).ReturnsAsync(0);
        moq.UnitOfWork.Setup(x => x.WhiteListChequeConfigRepo.getByChequeCollateralID(It.IsAny<long>())).ReturnsAsync(0);
        moq.Context.Setup(x => x.CurrentChequeConfigs.FindAsync(It.IsAny<object>()));
        moq.UnitOfWork.Setup(x => x.Context.Remove(It.IsAny<CurrentChequeConfig>()));
        moq.UnitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCurrentChequeConfigRequestHandler(moq.UnitOfWork.Object);

        var request = new DeleteCurrentChequeConfigRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
