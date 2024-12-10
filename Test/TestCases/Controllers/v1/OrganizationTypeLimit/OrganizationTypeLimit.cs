using Api.Controllers.v1;
using Application.Services.OrganizationTypeLimitService;
using Core.GenericResultModel;
using Core.ViewModel.OrganizationTypeLimit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.OrganizationTypeLimit;
public class OrganizationTypeLimit
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<OrganizationTypeLimitVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<OrganizationTypeLimitSearchVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    [Fact]
    public async Task AddOrganizationTypeLimitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddOrganizationTypeLimitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var organizationTypeLimitController = new OrganizationTypeLimitController(mediator.Object);
        var addOrganizationTypeLimitReq = new AddOrganizationTypeLimitRequest();

        var result = await organizationTypeLimitController.Add(addOrganizationTypeLimitReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteOrganizationTypeLimitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteOrganizationTypeLimitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var organizationTypeLimitController = new OrganizationTypeLimitController(mediator.Object);
        var deleteOrganizationTypeLimitReq = new DeleteOrganizationTypeLimitRequest();

        var result = await organizationTypeLimitController.Delete(deleteOrganizationTypeLimitReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateOrganizationTypeLimitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateOrganizationTypeLimitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var organizationTypeLimitController = new OrganizationTypeLimitController(mediator.Object);
        var updateOrganizationTypeLimitReq = new UpdateOrganizationTypeLimitRequest();

        var result = await organizationTypeLimitController.Update(updateOrganizationTypeLimitReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetOrganizationTypeLimitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetOrganizationTypeLimitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var organizationTypeLimitController = new OrganizationTypeLimitController(mediator.Object);
        var getOrganizationTypeLimitReq = new GetOrganizationTypeLimitRequest();

        var result = await organizationTypeLimitController.Get(getOrganizationTypeLimitReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchSubOrganizationTypeLimitTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AdvanceSearchOrganizationTypeLimitRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var organizationTypeLimitController = new OrganizationTypeLimitController(mediator.Object);
        var searchOrganizationTypeLimitReq = new AdvanceSearchOrganizationTypeLimitRequest();

        var result = await organizationTypeLimitController.Search(searchOrganizationTypeLimitReq);


        Assert.IsType<OkObjectResult>(result);
    }
}
