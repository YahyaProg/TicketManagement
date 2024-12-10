using Api.Controllers.v1;
using Application.Services.VehicleCollateralOwnerService;
using Core.GenericResultModel;
using Core.ViewModel.VehicleCollateralOwner;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.VehicleCollateralOwner;

public class VehicleCollateralOwnerControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<VehicleCollateralOwnerVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<VehicleCollateralOwnerVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddVehicleCollateralOwnerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddVehicleCollateralOwnerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var VehicleCollateralOwnerController = new VehicleCollateralOwnerController(mediator.Object);

        var addVehicleCollateralOwnerReq = new AddVehicleCollateralOwnerRequest();

        var result = await VehicleCollateralOwnerController.Add(addVehicleCollateralOwnerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetVehicleCollateralOwnerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetVehicleCollateralOwnerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var VehicleCollateralOwnerController = new VehicleCollateralOwnerController(mediator.Object);

        var getVehicleCollateralOwnerReq = new GetVehicleCollateralOwnerRequest();

        var result = await VehicleCollateralOwnerController.Get(getVehicleCollateralOwnerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchVehicleCollateralOwnerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchVehicleCollateralOwnerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var VehicleCollateralOwnerController = new VehicleCollateralOwnerController(mediator.Object);

        var searchVehicleCollateralOwnerReq = new SearchVehicleCollateralOwnerRequest();

        var result = await VehicleCollateralOwnerController.Search(searchVehicleCollateralOwnerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateVehicleCollateralOwnerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateVehicleCollateralOwnerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var VehicleCollateralOwnerController = new VehicleCollateralOwnerController(mediator.Object);

        var updateVehicleCollateralOwnerReq = new UpdateVehicleCollateralOwnerRequest();

        var result = await VehicleCollateralOwnerController.Update(updateVehicleCollateralOwnerReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteVehicleCollateralOwnerTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteVehicleCollateralOwnerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var VehicleCollateralOwnerController = new VehicleCollateralOwnerController(mediator.Object);

        var deleteVehicleCollateralOwnerReq = new DeleteVehicleCollateralOwnerRequest();

        var result = await VehicleCollateralOwnerController.Delete(deleteVehicleCollateralOwnerReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
