using Api.Controllers.v1;
using Application.Services.VehicleCollateralEvaluationService;
using Core.GenericResultModel;
using Core.ViewModel.VehicleCollateralEvaluation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.VehicleCollateralEvaluation;

public class VehicleCollateralEvaluationControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<VehicleCollateralEvaluationVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<VehicleCollateralEvaluationVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddVehicleCollateralEvaluationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddVehicleCollateralEvaluationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var VehicleCollateralEvaluationController = new VehicleCollateralEvaluationController(mediator.Object);

        var addVehicleCollateralEvaluationReq = new AddVehicleCollateralEvaluationRequest();

        var result = await VehicleCollateralEvaluationController.Add(addVehicleCollateralEvaluationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetVehicleCollateralEvaluationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetVehicleCollateralEvaluationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var VehicleCollateralEvaluationController = new VehicleCollateralEvaluationController(mediator.Object);

        var getVehicleCollateralEvaluationReq = new GetVehicleCollateralEvaluationRequest();

        var result = await VehicleCollateralEvaluationController.Get(getVehicleCollateralEvaluationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchVehicleCollateralEvaluationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchVehicleCollateralEvaluationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var VehicleCollateralEvaluationController = new VehicleCollateralEvaluationController(mediator.Object);

        var searchVehicleCollateralEvaluationReq = new SearchVehicleCollateralEvaluationRequest();

        var result = await VehicleCollateralEvaluationController.Search(searchVehicleCollateralEvaluationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateVehicleCollateralEvaluationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateVehicleCollateralEvaluationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var VehicleCollateralEvaluationController = new VehicleCollateralEvaluationController(mediator.Object);

        var updateVehicleCollateralEvaluationReq = new UpdateVehicleCollateralEvaluationRequest();

        var result = await VehicleCollateralEvaluationController.Update(updateVehicleCollateralEvaluationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteVehicleCollateralEvaluationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteVehicleCollateralEvaluationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var VehicleCollateralEvaluationController = new VehicleCollateralEvaluationController(mediator.Object);

        var deleteVehicleCollateralEvaluationReq = new DeleteVehicleCollateralEvaluationRequest();

        var result = await VehicleCollateralEvaluationController.Delete(deleteVehicleCollateralEvaluationReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
