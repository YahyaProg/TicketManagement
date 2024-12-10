using Application.Services.IndividualParamService;
using Core.GenericResultModel;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.IndividualParamTest;

public class IndividualParamAddTest
{
    private readonly Mock<DBContext> context = new();
    private readonly IndividualParamAddRequest request = new() { Code = "1", Title = "" };

    [Fact]
    public async Task CodeExistInParams()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([new() { Code = request.Code }]);

        var res = await GetRes();

        Assert.False(res.IsSuccess);
    }
    
    [Fact]
    public async Task CodeExistInFormules()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([]);
        context.Setup(x => x.IndividualFormula).ReturnsDbSet([new() { Code = request.Code}]);
        var res = await GetRes();

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task Success()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([]);
        context.Setup(x => x.IndividualFormula).ReturnsDbSet([]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var res = await GetRes();

        Assert.True (res.IsSuccess);
    }

    private async Task<ApiResult> GetRes()
    {
        var handler = new IndividualParamAddRequestHandler(context.Object);
        var res = await handler.Handle(request, CancellationToken.None);

        return res;
    }
}
