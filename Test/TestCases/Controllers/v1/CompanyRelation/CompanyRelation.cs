using Api.Controllers.v1;
using Application.Services.CompanyRelationService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CompanyRelation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CompanyRelation
{
    public class CompanyRelationControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CompanyRelationVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CompanyRelationSearchVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCompanyRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCompanyRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CompanyRelationController = new CompanyRelationController(mediator.Object);
            var addCurrncyReq = new AddCompanyRelationRequest();

            var result = await CompanyRelationController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCompanyRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCompanyRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CompanyRelationController = new CompanyRelationController(mediator.Object);
            var deleteCurrncyReq = new DeleteCompanyRelationRequest();

            var result = await CompanyRelationController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownCompanyRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownCompanyRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var CompanyRelationController = new CompanyRelationController(mediator.Object);

            var dropDownCompanyRelationReq = new DropDownCompanyRelationRequest();

            var result = await CompanyRelationController.DropDown(dropDownCompanyRelationReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateCompanyRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCompanyRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CompanyRelationController = new CompanyRelationController(mediator.Object);
            var updateCurrncyReq = new UpdateCompanyRelationRequest();

            var result = await CompanyRelationController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCompanyRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCompanyRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CompanyRelationController = new CompanyRelationController(mediator.Object);
            var getCurrncyReq = new GetCompanyRelationRequest();

            var result = await CompanyRelationController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCompanyRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCompanyRelationRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(searchSuccessRes));

            var CompanyRelationController = new CompanyRelationController(mediator.Object);
            var searchCurrncyReq = new SearchCompanyRelationRequest();

            var result = await CompanyRelationController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
