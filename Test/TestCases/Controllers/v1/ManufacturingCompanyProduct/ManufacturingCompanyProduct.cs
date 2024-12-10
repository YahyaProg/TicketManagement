using Api.Controllers.v1;
using Application.Services.ManufacturingCompanyProductService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.ManufacturingCompanyProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ManufacturingCompanyProduct
{
    public class ManufacturingCompanyProductControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ManufacturingCompanyProductVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ManufacturingCompanyProductVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
       
        [Fact]
        public async Task AddManufacturingCompanyProductTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddManufacturingCompanyProductRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingCompanyProductController = new ManufacturingCompanyProductController(mediator.Object);
            var addCurrncyReq = new AddManufacturingCompanyProductRequest();

            var result = await ManufacturingCompanyProductController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteManufacturingCompanyProductTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteManufacturingCompanyProductRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingCompanyProductController = new ManufacturingCompanyProductController(mediator.Object);
            var deleteCurrncyReq = new DeleteManufacturingCompanyProductRequest();

            var result = await ManufacturingCompanyProductController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateManufacturingCompanyProductTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateManufacturingCompanyProductRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingCompanyProductController = new ManufacturingCompanyProductController(mediator.Object);
            var updateCurrncyReq = new UpdateManufacturingCompanyProductRequest();

            var result = await ManufacturingCompanyProductController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetManufacturingCompanyProductTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetManufacturingCompanyProductRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ManufacturingCompanyProductController = new ManufacturingCompanyProductController(mediator.Object);
            var getCurrncyReq = new GetManufacturingCompanyProductRequest();

            var result = await ManufacturingCompanyProductController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchManufacturingCompanyProductTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchManufacturingCompanyProductRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ManufacturingCompanyProductController = new ManufacturingCompanyProductController(mediator.Object);
            var searchCurrncyReq = new SearchManufacturingCompanyProductRequest();

            var result = await ManufacturingCompanyProductController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
