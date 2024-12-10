using Api.Controllers.v1;
using Application.Services.CorporateCustomerService;
using Application.Services.DocumentListService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CorporateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CorporateCustomer
{
    public class CorporateCustomerControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<SearchCorporateCustomerVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };       

        [Fact]
        public async Task AddCorporateCustomerTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCorporateCustomerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CorporateCustomerController = new CorporateCustomerController(mediator.Object);

            var addCurrncyReq = new AddCorporateCustomerRequest();

            var result = await CorporateCustomerController.Add(addCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCorporateCustomerTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCorporateCustomerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CorporateCustomerController = new CorporateCustomerController(mediator.Object);

            var searchCurrncyReq = new SearchCorporateCustomerRequest();

            var result = await CorporateCustomerController.Search(searchCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCorporateCustomerTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCorporateCustomerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CorporateCustomerController = new CorporateCustomerController(mediator.Object);

            var updateCurrncyReq = new UpdateCorporateCustomerRequest();

            var result = await CorporateCustomerController.Update(updateCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateCompanyImageTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCorporateCustomerImageRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CorporateCustomerController = new CorporateCustomerController(mediator.Object);

            var updateCustomerImageReq = new UpdateCorporateCustomerImageRequest();

            var result = await CorporateCustomerController.AddOrUpdateCompanyImage(updateCustomerImageReq);

            Assert.IsType<OkObjectResult>(result);
        }

    }
}
