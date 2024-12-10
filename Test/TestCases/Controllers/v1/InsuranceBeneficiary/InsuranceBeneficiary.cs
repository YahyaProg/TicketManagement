using Api.Controllers.v1;
using Application.Services.InsuranceBeneficiaryService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.InsuranceBeneficiary;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.InsuranceBeneficiary;
public class InsuranceBeneficiaryTestController
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<InsuranceBeneficiaryVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<InsuranceBeneficiaryVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddInsuranceBeneficiaryTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddInsuranceBeneficiaryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var insuranceBeneficiaryController = new InsuranceBeneficiaryController(mediator.Object);
        var addCurrncyReq = new AddInsuranceBeneficiaryRequest();

        var result = await insuranceBeneficiaryController.Add(addCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteInsuranceBeneficiaryTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteInsuranceBeneficiaryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var insuranceBeneficiaryController = new InsuranceBeneficiaryController(mediator.Object);
        var deleteCurrncyReq = new DeleteInsuranceBeneficiaryRequest();

        var result = await insuranceBeneficiaryController.Delete(deleteCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateInsuranceBeneficiaryTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateInsuranceBeneficiaryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var insuranceBeneficiaryController = new InsuranceBeneficiaryController(mediator.Object);
        var updateCurrncyReq = new UpdateInsuranceBeneficiaryRequest();

        var result = await insuranceBeneficiaryController.Update(updateCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetInsuranceBeneficiaryTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetInsuranceBeneficiaryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var insuranceBeneficiaryController = new InsuranceBeneficiaryController(mediator.Object);
        var getCurrncyReq = new GetInsuranceBeneficiaryRequest();

        var result = await insuranceBeneficiaryController.Get(getCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchInsuranceBeneficiaryTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchInsuranceBeneficiaryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var insuranceBeneficiaryController = new InsuranceBeneficiaryController(mediator.Object);
        var searchCurrncyReq = new SearchInsuranceBeneficiaryRequest();

        var result = await insuranceBeneficiaryController.Search(searchCurrncyReq);


        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownInsuranceBeneficiaryTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownInsuranceBeneficiaryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var InsuranceBeneficiaryController = new InsuranceBeneficiaryController(mediator.Object);

        var dropDownInsuranceBeneficiaryReq = new DropDownInsuranceBeneficiaryRequest();

        var result = await InsuranceBeneficiaryController.DropDown(dropDownInsuranceBeneficiaryReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
