using Api.Controllers.v1;
using Application.Services.ServiceCompanyRankService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.ServiceCompanyRank;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ServiceCompanyRank
{
    public class ServiceCompanyRankControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ServiceCompanyRankVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ServiceCompanyRankVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddServiceCompanyRankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddServiceCompanyRankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ServiceCompanyRankController = new ServiceCompanyRankController(mediator.Object);
            var addCurrncyReq = new AddServiceCompanyRankRequest();

            var result = await ServiceCompanyRankController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteServiceCompanyRankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteServiceCompanyRankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ServiceCompanyRankController = new ServiceCompanyRankController(mediator.Object);
            var deleteCurrncyReq = new DeleteServiceCompanyRankRequest();

            var result = await ServiceCompanyRankController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownServiceCompanyRankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownServiceCompanyRankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var ServiceCompanyRankController = new ServiceCompanyRankController(mediator.Object);

            var dropDownServiceCompanyRankReq = new DropDownServiceCompanyRankRequest();

            var result = await ServiceCompanyRankController.DropDown(dropDownServiceCompanyRankReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateServiceCompanyRankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateServiceCompanyRankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ServiceCompanyRankController = new ServiceCompanyRankController(mediator.Object);
            var updateCurrncyReq = new UpdateServiceCompanyRankRequest();

            var result = await ServiceCompanyRankController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetServiceCompanyRankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetServiceCompanyRankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ServiceCompanyRankController = new ServiceCompanyRankController(mediator.Object);
            var getCurrncyReq = new GetServiceCompanyRankRequest();

            var result = await ServiceCompanyRankController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchServiceCompanyRankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchServiceCompanyRankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ServiceCompanyRankController = new ServiceCompanyRankController(mediator.Object);
            var searchCurrncyReq = new SearchServiceCompanyRankRequest();

            var result = await ServiceCompanyRankController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
