using Application.Services.FininfoChartconfigService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.FininfoChartconfig;

public class SearchFininfoChartconfigRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchFininfoChartconfigRequest_Success()
    {
        moq.Context.Setup(x => x.FininfoChartconfigs).ReturnsDbSet([new() { FininfoChartId = 1 }]);
        moq.Context.Setup(x => x.FininfoCharts).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchFininfoChartconfigRequestHandler(moq.Context.Object);

        var request = new SearchFininfoChartconfigRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
