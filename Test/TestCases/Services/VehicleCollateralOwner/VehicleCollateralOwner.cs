using Application.Services.VehicleCollateralOwnerService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.VehicleCollateralOwner;

public class VehicleCollateralOwnerRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void DeleteVehicleCollateralOwnerRequest_Success() =>
        Assert.NotNull(new DeleteVehicleCollateralOwnerRequestHandler(_unitOfWork.Object));
}
