
using Api.Controllers.v1;
using Application.Services.CmntService;
using Core.GenericResultModel;
using Core.Helpers;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1;


public class CommentControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly Mock<IUserHelper> userHelper = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<CmntVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<CmntVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddCmntTest()
    {
        var userDto = new UserDto()
        {
            Id = Guid.NewGuid().ToString(),
            Name =  "test"
        };
        _ = userHelper.Setup(x => x.GetUserFromToken()).Returns(userDto);
        mediator.Setup(x => x.Send(It.IsAny<AddCmntRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CommentController = new CommentController(mediator.Object, userHelper.Object);
        var addCmntReq = new AddCmntRequest();

        var result = await CommentController.Add(addCmntReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCmntTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteCmntRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CommentController = new CommentController(mediator.Object, userHelper.Object);
        var deleteCmntReq = new DeleteCmntRequest();

        var result = await CommentController.Delete(deleteCmntReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCmntTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCmntRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CommentController = new CommentController(mediator.Object, userHelper.Object);
        var updateCmntReq = new UpdateCmntRequest();

        var result = await CommentController.Update(updateCmntReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCmntTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetCmntRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var CommentController = new CommentController(mediator.Object, userHelper.Object);
        var getCmntReq = new GetCmntRequest();

        var result = await CommentController.Get(getCmntReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchCmntTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchCmntRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var CommentController = new CommentController(mediator.Object, userHelper.Object);
        var searchCmntReq = new SearchCmntRequest();

        var result = await CommentController.Search(searchCmntReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AdvanceSearchCmntTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AdvancedSearchCmntRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var CommentController = new CommentController(mediator.Object, userHelper.Object);
        var searchCmntReq = new AdvancedSearchCmntRequest();

        var result = await CommentController.AdvancedSearch(searchCmntReq);


        Assert.IsType<OkObjectResult>(result);
    }
}