using Application.Services.CityService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.CityTest;

public class SearchCityRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchCityRequest_Success()
    {
        moq.Context.Setup(x => x.Cities).ReturnsDbSet([new() { ProvinceId = 1 }]);
        moq.Context.Setup(x => x.Provinces).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchCityRequestHandler(moq.Context.Object);

        var request = new SearchCityRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
