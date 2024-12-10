using Application.Services.VehicleCollateralEvaluationService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.VehicleCollateralEvaluation;

public class SearchVehicleCollateralEvaluationRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchVehicleCollateralEvaluationRequest_Success()
    {
        moq.Context.Setup(x => x.VehicleCollateralEvaluations).ReturnsDbSet([new() { CurrencyId = 1 }]);
        moq.Context.Setup(x => x.Currencies).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchVehicleCollateralEvaluationRequestHandler(moq.Context.Object);

        var request = new SearchVehicleCollateralEvaluationRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
