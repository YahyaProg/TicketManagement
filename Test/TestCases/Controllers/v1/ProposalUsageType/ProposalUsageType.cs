using Api.Controllers.v1;
using Application.Services.ProposalUsageTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.ProposalUsageType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ProposalUsageType;
public class ProposalUsageTypeTestController
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ProposalUsageTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ProposalUsageTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddProposalUsageTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddProposalUsageTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var proposalUsageTypeController = new ProposalUsageTypeController(mediator.Object);
        var addCurrncyReq = new AddProposalUsageTypeRequest();

        var result = await proposalUsageTypeController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProposalUsageTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteProposalUsageTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var proposalUsageTypeController = new ProposalUsageTypeController(mediator.Object);
        var deleteCurrncyReq = new DeleteProposalUsageTypeRequest();

        var result = await proposalUsageTypeController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateProposalUsageTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateProposalUsageTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var proposalUsageTypeController = new ProposalUsageTypeController(mediator.Object);
        var updateCurrncyReq = new UpdateProposalUsageTypeRequest();

        var result = await proposalUsageTypeController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProposalUsageTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetProposalUsageTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var proposalUsageTypeController = new ProposalUsageTypeController(mediator.Object);
        var getCurrncyReq = new GetProposalUsageTypeRequest();

        var result = await proposalUsageTypeController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchProposalUsageTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchProposalUsageTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var proposalUsageTypeController = new ProposalUsageTypeController(mediator.Object);
        var searchCurrncyReq = new SearchProposalUsageTypeRequest();

        var result = await proposalUsageTypeController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownProposalUsageTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownProposalUsageTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var ProposalUsageTypeController = new ProposalUsageTypeController(mediator.Object);

        var dropDownProposalUsageTypeReq = new DropDownProposalUsageTypeRequest();

        var result = await ProposalUsageTypeController.DropDown(dropDownProposalUsageTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
