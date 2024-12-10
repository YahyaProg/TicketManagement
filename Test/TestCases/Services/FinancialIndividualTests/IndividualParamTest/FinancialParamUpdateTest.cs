using Application.Services.IndividualParamService;
using Core.GenericResultModel;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.IndividualParamTest;

public class IndividualParamUpdateTest
{
    private readonly Mock<DBContext> context = new();
    private readonly IndividualParamUpdateRequest request = new() { Id = 1, Code = "1", Title = "" };

    [Fact]
    public async Task CodeExistInParams()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([new() { Id = 2, Code = request.Code }]);

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
    public async Task EntityNotFound()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([]);
        context.Setup(x => x.IndividualFormula).ReturnsDbSet([]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var res = await GetRes();

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task Success()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([new() { Id = 1, Code = request.Code }]);
        context.Setup(x => x.IndividualFormula).ReturnsDbSet([]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var res = await GetRes();

        Assert.True(res.IsSuccess);
    }


    private async Task<ApiResult> GetRes()
    {
        var handler = new IndividualParamUpdateRequestHandler(context.Object);
        var res = await handler.Handle(request, CancellationToken.None);

        return res;
    }
}
