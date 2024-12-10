using Api.Controllers.v1;
using Application.Services.AccountModuleDescService;
using Core.GenericResultModel;
using Core.ViewModel.AccountModuleDesc;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.AccountModuleDesc;

public class AccountModuleDescControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<int> successRangeRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<AccountModuleDescVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<AccountModuleDescVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddAccountModuleDescTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddAccountModuleDescRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var AccountModuleDescController = new AccountModuleDescController(mediator.Object);

        var addAccountModuleDescReq = new AddAccountModuleDescRequest();

        var result = await AccountModuleDescController.Add(addAccountModuleDescReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAccountModuleDescTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAccountModuleDescRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var AccountModuleDescController = new AccountModuleDescController(mediator.Object);

        var getAccountModuleDescReq = new GetAccountModuleDescRequest();

        var result = await AccountModuleDescController.Get(getAccountModuleDescReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchAccountModuleDescTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchAccountModuleDescRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var AccountModuleDescController = new AccountModuleDescController(mediator.Object);

        var searchAccountModuleDescReq = new SearchAccountModuleDescRequest();

        var result = await AccountModuleDescController.Search(searchAccountModuleDescReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownAccountModuleDescTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownAccountModuleDescRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var AccountModuleDescController = new AccountModuleDescController(mediator.Object);

        var dropDownAccountModuleDescReq = new DropDownAccountModuleDescRequest();

        var result = await AccountModuleDescController.DropDown(dropDownAccountModuleDescReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAccountModuleDescTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateAccountModuleDescRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var AccountModuleDescController = new AccountModuleDescController(mediator.Object);

        var updateAccountModuleDescReq = new UpdateAccountModuleDescRequest();

        var result = await AccountModuleDescController.Update(updateAccountModuleDescReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAccountModuleDescTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteAccountModuleDescRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var AccountModuleDescController = new AccountModuleDescController(mediator.Object);

        var deleteAccountModuleDescReq = new DeleteAccountModuleDescRequest();

        var result = await AccountModuleDescController.Delete(deleteAccountModuleDescReq);

        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public async Task RangeAccountModuleDescTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddRangeAccountModuleDescRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRangeRes);

        var AccountModuleDescController = new AccountModuleDescController(mediator.Object);

        var deleteAccountModuleDescReq = new AddRangeAccountModuleDescRequest();

        var result = await AccountModuleDescController.Range(deleteAccountModuleDescReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
