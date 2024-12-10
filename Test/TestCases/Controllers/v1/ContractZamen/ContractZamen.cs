using Api.Controllers.v1;
using Application.Services.ContractZamenService;
using Core.GenericResultModel;
using Core.ViewModel.ContractZamen;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ContractZamen;

public class ContractZamenControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<ContractZamenVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ContractZamenVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddContractZamenTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddContractZamenRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ContractZamenController = new ContractZamenController(mediator.Object);

        var addContractZamenReq = new AddContractZamenRequest();

        var result = await ContractZamenController.Add(addContractZamenReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetContractZamenTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetContractZamenRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var ContractZamenController = new ContractZamenController(mediator.Object);

        var getContractZamenReq = new GetContractZamenRequest();

        var result = await ContractZamenController.Get(getContractZamenReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchContractZamenTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchContractZamenRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var ContractZamenController = new ContractZamenController(mediator.Object);

        var searchContractZamenReq = new SearchContractZamenRequest();

        var result = await ContractZamenController.Search(searchContractZamenReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateContractZamenTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateContractZamenRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ContractZamenController = new ContractZamenController(mediator.Object);

        var updateContractZamenReq = new UpdateContractZamenRequest();

        var result = await ContractZamenController.Update(updateContractZamenReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteContractZamenTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteContractZamenRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var ContractZamenController = new ContractZamenController(mediator.Object);

        var deleteContractZamenReq = new DeleteContractZamenRequest();

        var result = await ContractZamenController.Delete(deleteContractZamenReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
