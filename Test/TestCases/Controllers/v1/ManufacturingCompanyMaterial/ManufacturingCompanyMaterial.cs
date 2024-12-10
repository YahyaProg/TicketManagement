using Api.Controllers.v1;
using Application.Services.ManufacturingCompanyMaterialService;
using Core.GenericResultModel;
using Core.ViewModel.ManufacturingCompanyMaterial;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ManufacturingCompanyMaterial
{
    public class ManufacturingCompanyMaterialControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ManufacturingCompanyMaterialVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ManufacturingCompanyMaterialVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddManufacturingCompanyMaterialTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddManufacturingCompanyMaterialRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingCompanyMaterialController = new ManufacturingCompanyMaterialController(mediator.Object);
            var addCurrncyReq = new AddManufacturingCompanyMaterialRequest();

            var result = await ManufacturingCompanyMaterialController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteManufacturingCompanyMaterialTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteManufacturingCompanyMaterialRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingCompanyMaterialController = new ManufacturingCompanyMaterialController(mediator.Object);
            var deleteCurrncyReq = new DeleteManufacturingCompanyMaterialRequest();

            var result = await ManufacturingCompanyMaterialController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateManufacturingCompanyMaterialTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateManufacturingCompanyMaterialRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingCompanyMaterialController = new ManufacturingCompanyMaterialController(mediator.Object);
            var updateCurrncyReq = new UpdateManufacturingCompanyMaterialRequest();

            var result = await ManufacturingCompanyMaterialController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetManufacturingCompanyMaterialTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetManufacturingCompanyMaterialRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ManufacturingCompanyMaterialController = new ManufacturingCompanyMaterialController(mediator.Object);
            var getCurrncyReq = new GetManufacturingCompanyMaterialRequest();

            var result = await ManufacturingCompanyMaterialController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchManufacturingCompanyMaterialTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchManufacturingCompanyMaterialRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ManufacturingCompanyMaterialController = new ManufacturingCompanyMaterialController(mediator.Object);
            var searchCurrncyReq = new SearchManufacturingCompanyMaterialRequest();

            var result = await ManufacturingCompanyMaterialController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
