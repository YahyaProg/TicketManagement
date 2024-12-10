using Application.Services.VehicleCollateralEvaluationService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.VehicleCollateralEvaluation;

public class VehicleCollateralEvaluationRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void GetVehicleCollateralEvaluationRequest_Success() =>
        Assert.NotNull(new GetVehicleCollateralEvaluationRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteVehicleCollateralEvaluationRequest_Success() =>
        Assert.NotNull(new DeleteVehicleCollateralEvaluationRequestHandler(_unitOfWork.Object));
}
