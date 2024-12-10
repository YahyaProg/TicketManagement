using Application.Services.VehicleCollateralOwnerService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.VehicleCollateralOwner;

public class SearchVehicleCollateralOwnerRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchVehicleCollateralOwnerRequest_Success()
    {
        moq.Context.Setup(x => x.VehicleCollateralOwners).ReturnsDbSet([new() { CustomerId = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new SearchVehicleCollateralOwnerRequestHandler(moq.Context.Object);

        var request = new SearchVehicleCollateralOwnerRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
