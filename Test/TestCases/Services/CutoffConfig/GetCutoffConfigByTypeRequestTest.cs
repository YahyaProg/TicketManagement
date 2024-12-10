using Application.Services.CutoffConfigService;
using Core.Enums;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CutoffConfig;

public class GetCutoffConfigByTypeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task GetCutoffConfigByTypeRequest_Success()
    {
        _unitOfWork.Setup(x => x.Context.CutoffConfigs).ReturnsDbSet([new() { Type = "late-payment" }]);

        var handler = new GetCutoffConfigByTypeRequestHandler(_unitOfWork.Object);

        var request = new GetCutoffConfigByTypeRequest { Type = ECutoffConfig_Type.late_payment };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
