using Application.Services.VehicleCollateralOwnerService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.VehicleCollateralOwner;

public class GetVehicleCollateralOwnerRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task GetVehicleCollateralOwnerRequest_Success()
    {
        moq.Context.Setup(x => x.VehicleCollateralOwners).ReturnsDbSet([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, CorporateCustomer = new() { Name = "a" } }]);

        var handler = new GetVehicleCollateralOwnerRequestHandler(moq.Context.Object);

        var request = new GetVehicleCollateralOwnerRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }
}
