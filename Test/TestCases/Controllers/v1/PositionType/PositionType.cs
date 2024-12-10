using Api.Controllers.v1;
using Application.Services.PositionTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.PositionType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.PositionType;

public class PositionTypeControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PositionTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<PositionTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddPositionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddPositionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PositionTypeController = new PositionTypeController(mediator.Object);

        var addPositionTypeReq = new AddPositionTypeRequest();

        var result = await PositionTypeController.Add(addPositionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetPositionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetPositionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var PositionTypeController = new PositionTypeController(mediator.Object);

        var getPositionTypeReq = new GetPositionTypeRequest();

        var result = await PositionTypeController.Get(getPositionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchPositionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchPositionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var PositionTypeController = new PositionTypeController(mediator.Object);

        var searchPositionTypeReq = new SearchPositionTypeRequest();

        var result = await PositionTypeController.Search(searchPositionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownPositionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownPositionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var PositionTypeController = new PositionTypeController(mediator.Object);

        var dropDownPositionTypeReq = new DropDownPositionTypeRequest();

        var result = await PositionTypeController.DropDown(dropDownPositionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePositionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdatePositionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PositionTypeController = new PositionTypeController(mediator.Object);

        var updatePositionTypeReq = new UpdatePositionTypeRequest();

        var result = await PositionTypeController.Update(updatePositionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeletePositionTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeletePositionTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var PositionTypeController = new PositionTypeController(mediator.Object);

        var deletePositionTypeReq = new DeletePositionTypeRequest();

        var result = await PositionTypeController.Delete(deletePositionTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
