using Api.Controllers.v1;
using Application.Services.ManufacturingLicenceItemService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.ManufacturingLicenceItemItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ManufacturingLicenceItem
{
    public class ManufacturingLicenceItemControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ManufacturingLicenceItemGetVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ManufacturingLicenceItemVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddManufacturingLicenceItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddManufacturingLicenceItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingLicenceItemController = new ManufacturingLicenceItemController(mediator.Object);
            var addCurrncyReq = new AddManufacturingLicenceItemRequest();

            var result = await ManufacturingLicenceItemController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteManufacturingLicenceItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteManufacturingLicenceItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingLicenceItemController = new ManufacturingLicenceItemController(mediator.Object);
            var deleteCurrncyReq = new DeleteManufacturingLicenceItemRequest();

            var result = await ManufacturingLicenceItemController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownManufacturingLicenceItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownManufacturingLicenceItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var ManufacturingLicenceItemController = new ManufacturingLicenceItemController(mediator.Object);

            var dropDownManufacturingLicenceItemReq = new DropDownManufacturingLicenceItemRequest();

            var result = await ManufacturingLicenceItemController.DropDown(dropDownManufacturingLicenceItemReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateManufacturingLicenceItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateManufacturingLicenceItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ManufacturingLicenceItemController = new ManufacturingLicenceItemController(mediator.Object);
            var updateCurrncyReq = new UpdateManufacturingLicenceItemRequest();

            var result = await ManufacturingLicenceItemController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetManufacturingLicenceItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetManufacturingLicenceItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ManufacturingLicenceItemController = new ManufacturingLicenceItemController(mediator.Object);
            var getCurrncyReq = new GetManufacturingLicenceItemRequest();

            var result = await ManufacturingLicenceItemController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchManufacturingLicenceItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchManufacturingLicenceItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ManufacturingLicenceItemController = new ManufacturingLicenceItemController(mediator.Object);
            var searchCurrncyReq = new SearchManufacturingLicenceItemRequest();

            var result = await ManufacturingLicenceItemController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
