using Api.Controllers.v1;
using Application.Services.CompanyTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CompanyType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CompanyType;

public class CompanyTypeControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<CompanyTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<CompanyTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddCompanyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddCompanyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CompanyTypeController = new CompanyTypeController(mediator.Object);

        var addCompanyTypeReq = new AddCompanyTypeRequest();

        var result = await CompanyTypeController.Add(addCompanyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCompanyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetCompanyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var CompanyTypeController = new CompanyTypeController(mediator.Object);

        var getCompanyTypeReq = new GetCompanyTypeRequest();

        var result = await CompanyTypeController.Get(getCompanyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchCompanyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchCompanyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var CompanyTypeController = new CompanyTypeController(mediator.Object);

        var searchCompanyTypeReq = new SearchCompanyTypeRequest();

        var result = await CompanyTypeController.Search(searchCompanyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownCompanyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownCompanyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var CompanyTypeController = new CompanyTypeController(mediator.Object);

        var dropDownCompanyTypeReq = new DropDownCompanyTypeRequest();

        var result = await CompanyTypeController.DropDown(dropDownCompanyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCompanyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateCompanyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CompanyTypeController = new CompanyTypeController(mediator.Object);

        var updateCompanyTypeReq = new UpdateCompanyTypeRequest();

        var result = await CompanyTypeController.Update(updateCompanyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCompanyTypeTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteCompanyTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var CompanyTypeController = new CompanyTypeController(mediator.Object);

        var deleteCompanyTypeReq = new DeleteCompanyTypeRequest();

        var result = await CompanyTypeController.Delete(deleteCompanyTypeReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
