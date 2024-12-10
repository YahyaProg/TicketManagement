using Application.Services.CutoffConfigService;
using Core.Enums;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CutoffConfig;

public class UpdateCutoffConfigByTypeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task UpdateCutoffConfigByTypeRequest_Fail1()
    {
        var handler = new UpdateCutoffConfigByTypeRequestHandler(_unitOfWork.Object);

        var request = new UpdateCutoffConfigByTypeRequest
        { Cutoff = "[{\"value\":1,\"\":\"-12\"},{\"start\":\"-12\",\"end\":\"0\",\"value\":\"2\"},{\"start\":\"0\",\"value\":\"3\"}]" };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateCutoffConfigByTypeRequest_Fail2()
    {
        _unitOfWork.Setup(x => x.Context.CutoffConfigs).ReturnsDbSet([new() { Type = "" }]);

        var handler = new UpdateCutoffConfigByTypeRequestHandler(_unitOfWork.Object);

        var request = new UpdateCutoffConfigByTypeRequest
        { Cutoff = "[{\"value\":1}]", Type = ECutoffConfig_Type.late_payment };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateCutoffConfigByTypeRequest_Fail3()
    {
        _unitOfWork.Setup(x => x.Context.CutoffConfigs).ReturnsDbSet([new() { Type = "late-payment" }]);
        _unitOfWork.Setup(x => x.Repository.Update(It.IsAny<Core.Entities.CutoffConfig>()));
        _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCutoffConfigByTypeRequestHandler(_unitOfWork.Object);

        var request = new UpdateCutoffConfigByTypeRequest
        { Cutoff = "[{\"value\":1}]", Type = ECutoffConfig_Type.late_payment };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateCutoffConfigByTypeRequest_Success()
    {
        _unitOfWork.Setup(x => x.Context.CutoffConfigs).ReturnsDbSet([new() { Type = "late-payment" }]);
        _unitOfWork.Setup(x => x.Repository.Update(It.IsAny<Core.Entities.CutoffConfig>()));
        _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCutoffConfigByTypeRequestHandler(_unitOfWork.Object);

        var request = new UpdateCutoffConfigByTypeRequest
        { Cutoff = "[{\"value\":1}]", Type = ECutoffConfig_Type.late_payment };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
