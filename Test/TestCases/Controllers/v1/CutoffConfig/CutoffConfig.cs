using Api.Controllers.v1;
using Application.Services.CutoffConfigService;
using Core.Entities;
using Core.GenericResultModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.TestCases.Controllers.v1.City;

public class CutoffConfigControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<CutoffConfig> getSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task GetCutoffConfigByTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetCutoffConfigByTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var cutoffConfigController = new CutoffConfigController(mediator.Object);

        var getCutoffConfigByTypeRequest = new GetCutoffConfigByTypeRequest();

        var result = await cutoffConfigController.GetCutoffConfigByType(getCutoffConfigByTypeRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCutoffConfigByTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCutoffConfigByTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var cutoffConfigController = new CutoffConfigController(mediator.Object);

        var updateCutoffConfigByTypeRequest = new UpdateCutoffConfigByTypeRequest();

        var result = await cutoffConfigController.UpdateCutoffConfigByType(updateCutoffConfigByTypeRequest);

        Assert.IsType<OkObjectResult>(result);
    }
}
