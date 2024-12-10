using Application.Services.BaseService;
using Application.Services.VehicleCollateralOwnerService;
using Core.Enums;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.VehicleCollateralOwner;

public class AddVehicleCollateralOwnerRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddVehicleCollateralOwnerRequest_Fail1()
    {
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new AddVehicleCollateralOwnerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddVehicleCollateralOwnerRequest { OwnershipType = EOwnershipType.Owner };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddVehicleCollateralOwnerRequest_Fail2()
    {
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.VehicleCollateralOwners.Add(It.IsAny<Core.Entities.VehicleCollateralOwner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddVehicleCollateralOwnerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddVehicleCollateralOwnerRequest { CustomerId = 1, OwnershipType = EOwnershipType.Owner };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddVehicleCollateralOwnerRequest_Fail3()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new AddVehicleCollateralOwnerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddVehicleCollateralOwnerRequest { OwnershipType = EOwnershipType.ThirdPerson };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddVehicleCollateralOwnerRequest_Success()
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        moq.Context.Setup(x => x.VehicleCollateralOwners.Add(It.IsAny<Core.Entities.VehicleCollateralOwner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddVehicleCollateralOwnerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddVehicleCollateralOwnerRequest { OwnershipType = EOwnershipType.ThirdPerson };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
