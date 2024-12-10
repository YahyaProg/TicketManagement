using Api.Controllers.v1;
using Application.Services.InqueryData;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.TestCases.Controllers.v1.InqueryData;

public class InqueryDataControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult<InqueryDataVM> getSuccessRes = new();

    [Fact]
    public async Task GetInqueriesTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetInqueriesRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var inqueryDataController = new InqueryDataController(mediator.Object);

        var request = new GetInqueriesRequest();

        var result = await inqueryDataController.GetInqueries(request);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetIndividualInqueriesTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetIndividualInqueriesRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var inqueryDataController = new InqueryDataController(mediator.Object);

        var request = new GetIndividualInqueriesRequest();

        var result = await inqueryDataController.GetIndividualInqueries(request);

        Assert.IsType<OkObjectResult>(result);
    }
}
