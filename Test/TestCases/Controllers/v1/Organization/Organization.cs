using Api.Controllers.v1;
using Application.Services.OrganizationService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Organization
{
    public class OrganizationControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<OrganizationVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<OrganizationVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddOrganizationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddOrganizationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var OrganizationController = new OrganizationController(mediator.Object);
            var addCurrncyReq = new AddOrganizationRequest();

            var result = await OrganizationController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteOrganizationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteOrganizationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var OrganizationController = new OrganizationController(mediator.Object);
            var deleteCurrncyReq = new DeleteOrganizationRequest();

            var result = await OrganizationController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownOrganizationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownOrganizationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var OrganizationController = new OrganizationController(mediator.Object);

            var dropDownOrganizationReq = new DropDownOrganizationRequest();

            var result = await OrganizationController.DropDown(dropDownOrganizationReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateOrganizationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateOrganizationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var OrganizationController = new OrganizationController(mediator.Object);
            var updateCurrncyReq = new UpdateOrganizationRequest();

            var result = await OrganizationController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetOrganizationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetOrganizationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var OrganizationController = new OrganizationController(mediator.Object);
            var getCurrncyReq = new GetOrganizationRequest();

            var result = await OrganizationController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchOrganizationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchOrganizationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var OrganizationController = new OrganizationController(mediator.Object);
            var searchCurrncyReq = new SearchOrganizationRequest();

            var result = await OrganizationController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
