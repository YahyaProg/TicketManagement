using Api.Controllers.v1;
using Application.Services.CityService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.City;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.City;

public class CityControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<CityVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<CityVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddCityTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddCityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CityController = new CityController(mediator.Object);

        var addCityReq = new AddCityRequest();

        var result = await CityController.Add(addCityReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCityTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetCityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var CityController = new CityController(mediator.Object);

        var getCityReq = new GetCityRequest();

        var result = await CityController.Get(getCityReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchCityTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchCityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var CityController = new CityController(mediator.Object);

        var searchCityReq = new SearchCityRequest();

        var result = await CityController.Search(searchCityReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownCityTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownCityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var CityController = new CityController(mediator.Object);

        var dropDownCityReq = new DropDownCityRequest();

        var result = await CityController.DropDown(dropDownCityReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCityTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CityController = new CityController(mediator.Object);

        var updateCityReq = new UpdateCityRequest();

        var result = await CityController.Update(updateCityReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCityTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteCityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CityController = new CityController(mediator.Object);

        var deleteCityReq = new DeleteCityRequest();

        var result = await CityController.Delete(deleteCityReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
