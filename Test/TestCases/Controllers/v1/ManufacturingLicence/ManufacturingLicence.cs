using Api.Controllers.v1;
using Application.Services.ManufacturingLicenceService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.ManufacturingLicence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ManufacturingLicence
{
    public class ManufacturingLicenceControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ManufacturingLicenceVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ManufacturingLicenceVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddManufacturingLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddManufacturingLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingLicenceController = new ManufacturingLicenceController(mediator.Object);
            var addCurrncyReq = new AddManufacturingLicenceRequest();

            var result = await ManufacturingLicenceController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteManufacturingLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteManufacturingLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingLicenceController = new ManufacturingLicenceController(mediator.Object);
            var deleteCurrncyReq = new DeleteManufacturingLicenceRequest();

            var result = await ManufacturingLicenceController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownManufacturingLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownManufacturingLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var ManufacturingLicenceController = new ManufacturingLicenceController(mediator.Object);

            var dropDownManufacturingLicenceReq = new DropDownManufacturingLicenceRequest();

            var result = await ManufacturingLicenceController.DropDown(dropDownManufacturingLicenceReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateManufacturingLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateManufacturingLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingLicenceController = new ManufacturingLicenceController(mediator.Object);
            var updateCurrncyReq = new UpdateManufacturingLicenceRequest();

            var result = await ManufacturingLicenceController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetManufacturingLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetManufacturingLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ManufacturingLicenceController = new ManufacturingLicenceController(mediator.Object);
            var getCurrncyReq = new GetManufacturingLicenceRequest();

            var result = await ManufacturingLicenceController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchManufacturingLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchManufacturingLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ManufacturingLicenceController = new ManufacturingLicenceController(mediator.Object);
            var searchCurrncyReq = new SearchManufacturingLicenceRequest();

            var result = await ManufacturingLicenceController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
