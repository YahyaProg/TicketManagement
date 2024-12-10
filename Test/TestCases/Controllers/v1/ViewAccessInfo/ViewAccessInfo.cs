using Api.Controllers.v1;
using Application.Services.ViewAccessInfoService;
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
using Core.ViewModel.ViewAccessInfo;

namespace Test.TestCases.Controllers.v1.ViewAccessInfo;
public class ViewAccessInfo
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ViewAccessInfoVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ViewAccessInfoVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddViewAccessInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddViewAccessInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var viewAccessInfoController = new ViewAccessInfoController(mediator.Object);
        var addViewAccessInfoReq = new AddViewAccessInfoRequest();

        var result = await viewAccessInfoController.Add(addViewAccessInfoReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteViewAccessInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteViewAccessInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var viewAccessInfoController = new ViewAccessInfoController(mediator.Object);
        var deleteViewAccessInfoReq = new DeleteViewAccessInfoRequest();

        var result = await viewAccessInfoController.Delete(deleteViewAccessInfoReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateViewAccessInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateViewAccessInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var viewAccessInfoController = new ViewAccessInfoController(mediator.Object);
        var updateViewAccessInfoReq = new UpdateViewAccessInfoRequest();

        var result = await viewAccessInfoController.Update(updateViewAccessInfoReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetViewAccessInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetViewAccessInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var viewAccessInfoController = new ViewAccessInfoController(mediator.Object);
        var getViewAccessInfoReq = new GetViewAccessInfoRequest();

        var result = await viewAccessInfoController.Get(getViewAccessInfoReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchViewAccessInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchViewAccessInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var viewAccessInfoController = new ViewAccessInfoController(mediator.Object);
        var searchViewAccessInfoReq = new SearchViewAccessInfoRequest();

        var result = await viewAccessInfoController.Search(searchViewAccessInfoReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownViewAccessInfoTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownViewAccessInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var ViewAccessInfoController = new ViewAccessInfoController(mediator.Object);

        var dropDownViewAccessInfoReq = new DropDownViewAccessInfoRequest();

        var result = await ViewAccessInfoController.DropDown(dropDownViewAccessInfoReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
