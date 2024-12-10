
using Api.Controllers.v1;
using Application.Services.ProposalSchemeService;
using Core.Enums;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class ProposalSchemeControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ProposalSchemeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ProposalSchemeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddProposalSchemeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddProposalSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ProposalSchemeController = new ProposalSchemeController(mediator.Object);
        var addCurrncyReq = new AddProposalSchemeRequest();

        var result = await ProposalSchemeController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProposalSchemeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteProposalSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ProposalSchemeController = new ProposalSchemeController(mediator.Object);
        var deleteCurrncyReq = new DeleteProposalSchemeRequest();

        var result = await ProposalSchemeController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateProposalSchemeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateProposalSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ProposalSchemeController = new ProposalSchemeController(mediator.Object);
        var updateCurrncyReq = new UpdateProposalSchemeRequest();

        var result = await ProposalSchemeController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProposalSchemeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetProposalSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var ProposalSchemeController = new ProposalSchemeController(mediator.Object);
        var getCurrncyReq = new GetProposalSchemeRequest();

        var result = await ProposalSchemeController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchProposalSchemeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchProposalSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var ProposalSchemeController = new ProposalSchemeController(mediator.Object);
        var searchCurrncyReq = new SearchProposalSchemeRequest();

        var result = await ProposalSchemeController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }
}