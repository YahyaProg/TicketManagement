using Api.Controllers.v1;
using Application.Services.PropertyTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.PropertyType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.PropertyType;

public class PropertyTypeControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PropertyTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<PropertyTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddPropertyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddPropertyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PropertyTypeController = new PropertyTypeController(mediator.Object);

        var addPropertyTypeReq = new AddPropertyTypeRequest();

        var result = await PropertyTypeController.Add(addPropertyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetPropertyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetPropertyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var PropertyTypeController = new PropertyTypeController(mediator.Object);

        var getPropertyTypeReq = new GetPropertyTypeRequest();

        var result = await PropertyTypeController.Get(getPropertyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchPropertyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchPropertyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var PropertyTypeController = new PropertyTypeController(mediator.Object);

        var searchPropertyTypeReq = new SearchPropertyTypeRequest();

        var result = await PropertyTypeController.Search(searchPropertyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownPropertyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownPropertyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var PropertyTypeController = new PropertyTypeController(mediator.Object);

        var dropDownPropertyTypeReq = new DropDownPropertyTypeRequest();

        var result = await PropertyTypeController.DropDown(dropDownPropertyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePropertyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdatePropertyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PropertyTypeController = new PropertyTypeController(mediator.Object);

        var updatePropertyTypeReq = new UpdatePropertyTypeRequest();

        var result = await PropertyTypeController.Update(updatePropertyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeletePropertyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeletePropertyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PropertyTypeController = new PropertyTypeController(mediator.Object);

        var deletePropertyTypeReq = new DeletePropertyTypeRequest();

        var result = await PropertyTypeController.Delete(deletePropertyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
