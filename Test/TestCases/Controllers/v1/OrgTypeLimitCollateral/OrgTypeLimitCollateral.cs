using Api.Controllers.v1;
using Application.Services.OrgTypeLimitCollateralService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.OrgTypeLimitCollateral;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.OrgTypeLimitCollateral;

public class OrgTypeLimitCollateralControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<OrgTypeLimitCollateralVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<OrgTypeLimitCollateralVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddOrgTypeLimitCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddOrgTypeLimitCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var OrgTypeLimitCollateralController = new OrgTypeLimitCollateralController(mediator.Object);

        var addOrgTypeLimitCollateralReq = new AddOrgTypeLimitCollateralRequest();

        var result = await OrgTypeLimitCollateralController.Add(addOrgTypeLimitCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetOrgTypeLimitCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetOrgTypeLimitCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var OrgTypeLimitCollateralController = new OrgTypeLimitCollateralController(mediator.Object);

        var getOrgTypeLimitCollateralReq = new GetOrgTypeLimitCollateralRequest();

        var result = await OrgTypeLimitCollateralController.Get(getOrgTypeLimitCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchOrgTypeLimitCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchOrgTypeLimitCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var OrgTypeLimitCollateralController = new OrgTypeLimitCollateralController(mediator.Object);

        var searchOrgTypeLimitCollateralReq = new SearchOrgTypeLimitCollateralRequest();

        var result = await OrgTypeLimitCollateralController.Search(searchOrgTypeLimitCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateOrgTypeLimitCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateOrgTypeLimitCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var OrgTypeLimitCollateralController = new OrgTypeLimitCollateralController(mediator.Object);

        var updateOrgTypeLimitCollateralReq = new UpdateOrgTypeLimitCollateralRequest();

        var result = await OrgTypeLimitCollateralController.Update(updateOrgTypeLimitCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteOrgTypeLimitCollateralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteOrgTypeLimitCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var OrgTypeLimitCollateralController = new OrgTypeLimitCollateralController(mediator.Object);

        var deleteOrgTypeLimitCollateralReq = new DeleteOrgTypeLimitCollateralRequest();

        var result = await OrgTypeLimitCollateralController.Delete(deleteOrgTypeLimitCollateralReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
