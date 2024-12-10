
using Api.Controllers.v1;
using Application.Services.ProposalDescriptionService;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class ProposalDescriptionControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult<long> addOrUpdateSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ProposalDescriptionVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ProposalDescriptionVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddOrUpdateProposalDescriptionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddOrUpdateProposalDescriptionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(addOrUpdateSuccessRes);

        var proposalDescriptionController = new ProposalDescriptionController(mediator.Object);
        var addProposalDescriptionReq = new AddOrUpdateProposalDescriptionRequest();

        var result = await proposalDescriptionController.AddOrUpdate(addProposalDescriptionReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProposalDescriptionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteProposalDescriptionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var proposalDescriptionController = new ProposalDescriptionController(mediator.Object);
        var deleteProposalDescriptionReq = new DeleteProposalDescriptionRequest();

        var result = await proposalDescriptionController.Delete(deleteProposalDescriptionReq);


        Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task GetProposalDescriptionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetProposalDescriptionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var proposalDescriptionController = new ProposalDescriptionController(mediator.Object);
        var getProposalDescriptionReq = new GetProposalDescriptionRequest();

        var result = await proposalDescriptionController.Get(getProposalDescriptionReq);


        Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task SearchProposalDescriptionTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchProposalDescriptionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var proposalDescriptionController = new ProposalDescriptionController(mediator.Object);
        var searchProposalDescriptionReq = new SearchProposalDescriptionRequest();

        var result = await proposalDescriptionController.Search(searchProposalDescriptionReq);


        Assert.IsType<OkObjectResult>(result);
    }
}