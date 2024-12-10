using Application.Services.IndividualParamService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.IndividualParamTest;

public class IndividualParamDeleteTests
{
    private readonly Mock<DBContext> context = new();
    private readonly IndividualParamDeleteRequest request = new IndividualParamDeleteRequest()
    {
        Id = 1,
    };

    [Fact]
    public async Task NotFound()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([]);

        var handler = new IndividualParamDeleteRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task Success()
    {
        context.Setup(x => x.IndividualParam).ReturnsDbSet([new() { Id = 1 }]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var handler = new IndividualParamDeleteRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.True(res.IsSuccess);
    }
}
