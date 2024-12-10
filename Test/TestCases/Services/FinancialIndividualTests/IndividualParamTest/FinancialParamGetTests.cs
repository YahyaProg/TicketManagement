using Application.Services.IndividualParamService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
namespace Test.TestCases.Services.IndividualParamTest;

public class IndividualParamGetTests
{
    private readonly GetIndividualParamByIdRequest getByIdRequest = new() { Id = 1 };
    private readonly IndividualParamSearchRequest searchRequest = new();
    private readonly Mock<DBContext> context = new();

    [Fact]
    public async Task GetById_NotFound()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([]);
        var handler = new GetIndividualParamByIdRequestHandler(context.Object);
        var res = await handler.Handle(getByIdRequest, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task GetById_Success()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([new() { Id = 1}]);
        var handler = new GetIndividualParamByIdRequestHandler(context.Object);
        var res = await handler.Handle(getByIdRequest, CancellationToken.None);

        Assert.True(res.IsSuccess);
    }

    [Fact]
    public async Task Search_Success()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([new() { Id = 1 }]);
        var handler = new IndividualParamSearchRequestHandler(context.Object);
        var res = await handler.Handle(searchRequest, CancellationToken.None);

        Assert.True(res.IsSuccess);
        Assert.NotNull(res.Data);
    }
}
