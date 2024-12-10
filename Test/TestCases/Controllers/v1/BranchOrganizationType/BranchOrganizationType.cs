using Api.Controllers.v1;
using Application.Services.BranchOrganizationTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.BranchOrganizationType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BranchOrganizationType;

public class BranchOrganizationTypeControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<BranchOrganizationTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<BranchOrganizationTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddBranchOrganizationTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddBranchOrganizationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BranchOrganizationTypeController = new BranchOrganizationTypeController(mediator.Object);

        var addBranchOrganizationTypeReq = new AddBranchOrganizationTypeRequest();

        var result = await BranchOrganizationTypeController.Add(addBranchOrganizationTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetBranchOrganizationTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetBranchOrganizationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var BranchOrganizationTypeController = new BranchOrganizationTypeController(mediator.Object);

        var getBranchOrganizationTypeReq = new GetBranchOrganizationTypeRequest();

        var result = await BranchOrganizationTypeController.Get(getBranchOrganizationTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchBranchOrganizationTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchBranchOrganizationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var BranchOrganizationTypeController = new BranchOrganizationTypeController(mediator.Object);

        var searchBranchOrganizationTypeReq = new SearchBranchOrganizationTypeRequest();

        var result = await BranchOrganizationTypeController.Search(searchBranchOrganizationTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownBranchOrganizationTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownBranchOrganizationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var BranchOrganizationTypeController = new BranchOrganizationTypeController(mediator.Object);

        var dropDownBranchOrganizationTypeReq = new DropDownBranchOrganizationTypeRequest();

        var result = await BranchOrganizationTypeController.DropDown(dropDownBranchOrganizationTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateBranchOrganizationTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateBranchOrganizationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BranchOrganizationTypeController = new BranchOrganizationTypeController(mediator.Object);

        var updateBranchOrganizationTypeReq = new UpdateBranchOrganizationTypeRequest();

        var result = await BranchOrganizationTypeController.Update(updateBranchOrganizationTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteBranchOrganizationTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteBranchOrganizationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BranchOrganizationTypeController = new BranchOrganizationTypeController(mediator.Object);

        var deleteBranchOrganizationTypeReq = new DeleteBranchOrganizationTypeRequest();

        var result = await BranchOrganizationTypeController.Delete(deleteBranchOrganizationTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
