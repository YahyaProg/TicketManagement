
using Api.Controllers.v1;
using Application.Services.ProposalManagerLcService;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class ProposalManagerLcControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ProposalManagerLcVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task UpdateProposalManagerLcTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<PartialUpdateProposalManagerLcRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ProposalManagerLcController = new ProposalManagerLcController(mediator.Object);
        var updateProposalManagerLcReq = new PartialUpdateProposalManagerLcRequest();

        var result = await ProposalManagerLcController.PartialUpdate(updateProposalManagerLcReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchProposalManagerLcTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchProposalManagerLcRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var ProposalManagerLcController = new ProposalManagerLcController(mediator.Object);
        var searchProposalManagerLcReq = new SearchProposalManagerLcRequest();

        var result = await ProposalManagerLcController.Search(searchProposalManagerLcReq);


        Assert.IsType<OkObjectResult>(result);
    }
}