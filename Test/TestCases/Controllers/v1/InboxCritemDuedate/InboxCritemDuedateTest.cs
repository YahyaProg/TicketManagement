using Api.Controllers.v1;
using Application.Services.InboxCritemDuedateService;
using Core.GenericResultModel;
using Core.ViewModel.InboxCritemDuedate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.InboxCritemDuedate;
public class InboxCritemDuedateTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<InboxCritemDuedateVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<InboxCritemDuedateVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddInboxCritemDuedateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddInboxCritemDuedateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var inboxCritemDuedateController = new InboxCritemDuedateController(mediator.Object);
        var addInboxCritemDuedateReq = new AddInboxCritemDuedateRequest();

        var result = await inboxCritemDuedateController.Add(addInboxCritemDuedateReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteInboxCritemDuedateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteInboxCritemDuedateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var inboxCritemDuedateController = new InboxCritemDuedateController(mediator.Object);
        var deleteInboxCritemDuedateReq = new DeleteInboxCritemDuedateRequest();

        var result = await inboxCritemDuedateController.Delete(deleteInboxCritemDuedateReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateInboxCritemDuedateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateInboxCritemDuedateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var inboxCritemDuedateController = new InboxCritemDuedateController(mediator.Object);
        var updateInboxCritemDuedateReq = new UpdateInboxCritemDuedateRequest();

        var result = await inboxCritemDuedateController.Update(updateInboxCritemDuedateReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetInboxCritemDuedateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetInboxCritemDuedateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var inboxCritemDuedateController = new InboxCritemDuedateController(mediator.Object);
        var getInboxCritemDuedateReq = new GetInboxCritemDuedateRequest();

        var result = await inboxCritemDuedateController.Get(getInboxCritemDuedateReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchInboxCritemDuedateTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchInboxCritemDuedateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var inboxCritemDuedateController = new InboxCritemDuedateController(mediator.Object);
        var searchInboxCritemDuedateReq = new SearchInboxCritemDuedateRequest();

        var result = await inboxCritemDuedateController.Search(searchInboxCritemDuedateReq);


        Assert.IsType<OkObjectResult>(result);
    }
}
