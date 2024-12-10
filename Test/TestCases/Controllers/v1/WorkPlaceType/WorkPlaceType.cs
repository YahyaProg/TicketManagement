using Api.Controllers.v1;
using Application.Services.WorkPlaceTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.WorkPlaceType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.WorkPlaceType;

public class WorkPlaceTypeControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<WorkPlaceTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<WorkPlaceTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddWorkPlaceTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddWorkPlaceTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WorkPlaceTypeController = new WorkPlaceTypeController(mediator.Object);

        var addWorkPlaceTypeReq = new AddWorkPlaceTypeRequest();

        var result = await WorkPlaceTypeController.Add(addWorkPlaceTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetWorkPlaceTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetWorkPlaceTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var WorkPlaceTypeController = new WorkPlaceTypeController(mediator.Object);

        var getWorkPlaceTypeReq = new GetWorkPlaceTypeRequest();

        var result = await WorkPlaceTypeController.Get(getWorkPlaceTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchWorkPlaceTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchWorkPlaceTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var WorkPlaceTypeController = new WorkPlaceTypeController(mediator.Object);

        var searchWorkPlaceTypeReq = new SearchWorkPlaceTypeRequest();

        var result = await WorkPlaceTypeController.Search(searchWorkPlaceTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownWorkPlaceTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownWorkPlaceTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var WorkPlaceTypeController = new WorkPlaceTypeController(mediator.Object);

        var dropDownWorkPlaceTypeReq = new DropDownWorkPlaceTypeRequest();

        var result = await WorkPlaceTypeController.DropDown(dropDownWorkPlaceTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateWorkPlaceTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateWorkPlaceTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WorkPlaceTypeController = new WorkPlaceTypeController(mediator.Object);

        var updateWorkPlaceTypeReq = new UpdateWorkPlaceTypeRequest();

        var result = await WorkPlaceTypeController.Update(updateWorkPlaceTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteWorkPlaceTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteWorkPlaceTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var WorkPlaceTypeController = new WorkPlaceTypeController(mediator.Object);

        var deleteWorkPlaceTypeReq = new DeleteWorkPlaceTypeRequest();

        var result = await WorkPlaceTypeController.Delete(deleteWorkPlaceTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
