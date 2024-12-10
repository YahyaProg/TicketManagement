using Api.Controllers.v1;
using Application.Services.MovableAssetService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;
using Core.ViewModel.MovableAsset;

namespace Test.TestCases.Controllers.v1.MovableAsset;
public class MovableAssetControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<MovableAssetVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<MovableAssetVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddMovableAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddMovableAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var movableAssetController = new MovableAssetController(mediator.Object);
        var addCurrncyReq = new AddMovableAssetRequest();

        var result = await movableAssetController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMovableAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteMovableAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var movableAssetController = new MovableAssetController(mediator.Object);
        var deleteCurrncyReq = new DeleteMovableAssetRequest();

        var result = await movableAssetController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMovableAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateMovableAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var movableAssetController = new MovableAssetController(mediator.Object);
        var updateCurrncyReq = new UpdateMovableAssetRequest();

        var result = await movableAssetController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetMovableAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetMovableAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var movableAssetController = new MovableAssetController(mediator.Object);
        var getCurrncyReq = new GetMovableAssetRequest();

        var result = await movableAssetController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchMovableAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchMovableAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var movableAssetController = new MovableAssetController(mediator.Object);
        var searchCurrncyReq = new SearchMovableAssetRequest();

        var result = await movableAssetController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownMovableAssetTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownMovableAssetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var MovableAssetController = new MovableAssetController(mediator.Object);

        var dropDownMovableAssetReq = new DropDownMovableAssetRequest();

        var result = await MovableAssetController.DropDown(dropDownMovableAssetReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
