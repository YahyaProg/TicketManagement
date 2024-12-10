using Api.Controllers.v1;
using Application.Services.OtheracctchqcolatralService;
using Core.GenericResultModel;
using Core.ViewModel.Otheracctchqcolatral;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Otheracctchqcolatral;

public class OtheracctchqcolatralControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<OtheracctchqcolatralVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<OtheracctchqcolatralVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddOtheracctchqcolatralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddOtheracctchqcolatralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var OtheracctchqcolatralController = new OtheracctchqcolatralController(mediator.Object);

        var addOtheracctchqcolatralReq = new AddOtheracctchqcolatralRequest();

        var result = await OtheracctchqcolatralController.Add(addOtheracctchqcolatralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetOtheracctchqcolatralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetOtheracctchqcolatralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var OtheracctchqcolatralController = new OtheracctchqcolatralController(mediator.Object);

        var getOtheracctchqcolatralReq = new GetOtheracctchqcolatralRequest();

        var result = await OtheracctchqcolatralController.Get(getOtheracctchqcolatralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchOtheracctchqcolatralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchOtheracctchqcolatralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var OtheracctchqcolatralController = new OtheracctchqcolatralController(mediator.Object);

        var searchOtheracctchqcolatralReq = new SearchOtheracctchqcolatralRequest();

        var result = await OtheracctchqcolatralController.Search(searchOtheracctchqcolatralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateOtheracctchqcolatralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateOtheracctchqcolatralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var OtheracctchqcolatralController = new OtheracctchqcolatralController(mediator.Object);

        var updateOtheracctchqcolatralReq = new UpdateOtheracctchqcolatralRequest();

        var result = await OtheracctchqcolatralController.Update(updateOtheracctchqcolatralReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteOtheracctchqcolatralTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteOtheracctchqcolatralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var OtheracctchqcolatralController = new OtheracctchqcolatralController(mediator.Object);

        var deleteOtheracctchqcolatralReq = new DeleteOtheracctchqcolatralRequest();

        var result = await OtheracctchqcolatralController.Delete(deleteOtheracctchqcolatralReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
