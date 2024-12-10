using Api.Controllers.v1;
using Application.Services.InqueryValidationConfigService;
using Core.GenericResultModel;
using Core.ViewModel.InqueryValidationConfig;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.InqueryValidationConfig;

public class InqueryValidationConfigControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<InqueryValidationConfigVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<InqueryValidationConfigVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddInqueryValidationConfigTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddInqueryValidationConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var InqueryValidationConfigController = new InqueryValidationConfigController(mediator.Object);

        var addInqueryValidationConfigReq = new AddInqueryValidationConfigRequest();

        var result = await InqueryValidationConfigController.Add(addInqueryValidationConfigReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetInqueryValidationConfigTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetInqueryValidationConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var InqueryValidationConfigController = new InqueryValidationConfigController(mediator.Object);

        var getInqueryValidationConfigReq = new GetInqueryValidationConfigRequest();

        var result = await InqueryValidationConfigController.Get(getInqueryValidationConfigReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchInqueryValidationConfigTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchInqueryValidationConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var InqueryValidationConfigController = new InqueryValidationConfigController(mediator.Object);

        var searchInqueryValidationConfigReq = new SearchInqueryValidationConfigRequest();

        var result = await InqueryValidationConfigController.Search(searchInqueryValidationConfigReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateInqueryValidationConfigTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateInqueryValidationConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var InqueryValidationConfigController = new InqueryValidationConfigController(mediator.Object);

        var updateInqueryValidationConfigReq = new UpdateInqueryValidationConfigRequest();

        var result = await InqueryValidationConfigController.Update(updateInqueryValidationConfigReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteInqueryValidationConfigTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteInqueryValidationConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var InqueryValidationConfigController = new InqueryValidationConfigController(mediator.Object);

        var deleteInqueryValidationConfigReq = new DeleteInqueryValidationConfigRequest();

        var result = await InqueryValidationConfigController.Delete(deleteInqueryValidationConfigReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
