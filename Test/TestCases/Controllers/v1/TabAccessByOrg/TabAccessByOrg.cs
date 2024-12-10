using Api.Controllers.v1;
using Application.Services.TabAccessByOrgService;
using Core.GenericResultModel;
using Core.ViewModel.TabAccessByOrg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.TabAccessByOrg;
public class TabAccessByOrg
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<TabAccessByOrgVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<TabAccessByOrgSearchVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    [Fact]
    public async Task AddTabAccessByOrgTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddTabAccessByOrgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var tabAccessByOrgController = new TabAccessByOrgController(mediator.Object);
        var addTabAccessByOrgReq = new AddTabAccessByOrgRequest();

        var result = await tabAccessByOrgController.Add(addTabAccessByOrgReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteTabAccessByOrgTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteTabAccessByOrgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var tabAccessByOrgController = new TabAccessByOrgController(mediator.Object);
        var deleteTabAccessByOrgReq = new DeleteTabAccessByOrgRequest();

        var result = await tabAccessByOrgController.Delete(deleteTabAccessByOrgReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateTabAccessByOrgTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateTabAccessByOrgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var tabAccessByOrgController = new TabAccessByOrgController(mediator.Object);
        var updateTabAccessByOrgReq = new UpdateTabAccessByOrgRequest();

        var result = await tabAccessByOrgController.Update(updateTabAccessByOrgReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetTabAccessByOrgTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetTabAccessByOrgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var tabAccessByOrgController = new TabAccessByOrgController(mediator.Object);
        var getTabAccessByOrgReq = new GetTabAccessByOrgRequest();

        var result = await tabAccessByOrgController.Get(getTabAccessByOrgReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchSubTabAccessByOrgTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AdvanceSearchTabAccessByOrgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var tabAccessByOrgController = new TabAccessByOrgController(mediator.Object);
        var searchTabAccessByOrgReq = new AdvanceSearchTabAccessByOrgRequest();

        var result = await tabAccessByOrgController.Search(searchTabAccessByOrgReq);


        Assert.IsType<OkObjectResult>(result);
    }
}
