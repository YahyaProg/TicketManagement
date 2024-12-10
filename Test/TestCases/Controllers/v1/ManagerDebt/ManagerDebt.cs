using Api.Controllers.v1;
using Application.Services.ManagerDebtService;
using Core.Entities;
using Core.GenericResultModel;
using Core.ViewModel.ManagerDebt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ManagerDebt;

public class ManagerDebtControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult<PaginatedList<GetAllManagerDebtVM>> getAllManagerDebtSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<ManagerDebtTotal>> getAllManagerDebtTotalSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task GetAllIndividualManagerDebt()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAllIndividualManagerDebtRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getAllManagerDebtSuccessRes);

        var managerDebtController = new ManagerDebtController(mediator.Object);

        var getAllIndividualManagerDebtRequest = new GetAllIndividualManagerDebtRequest();

        var result = await managerDebtController.GetAllIndividualManagerDebt(getAllIndividualManagerDebtRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAllCorporateManagerDebtTotal()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAllCorporateManagerDebtTotalRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getAllManagerDebtTotalSuccessRes);

        var managerDebtController = new ManagerDebtController(mediator.Object);

        var getAllCorporateManagerDebtTotalRequest = new GetAllCorporateManagerDebtTotalRequest();

        var result = await managerDebtController.GetAllCorporateManagerDebtTotal(getAllCorporateManagerDebtTotalRequest);

        Assert.IsType<OkObjectResult>(result);
    }
}
