using Api.Controllers.v1;
using Application.Services.UnitTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.UnitType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.UnitType
{
    public class UnitTypeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<UnitTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<UnitTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddUnitTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddUnitTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var UnitTypeController = new UnitTypeController(mediator.Object);
            var addCurrncyReq = new AddUnitTypeRequest();

            var result = await UnitTypeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteUnitTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteUnitTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var UnitTypeController = new UnitTypeController(mediator.Object);
            var deleteCurrncyReq = new DeleteUnitTypeRequest();

            var result = await UnitTypeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownUnitTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownUnitTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var UnitTypeController = new UnitTypeController(mediator.Object);

            var dropDownUnitTypeReq = new DropDownUnitTypeRequest();

            var result = await UnitTypeController.DropDown(dropDownUnitTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateUnitTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateUnitTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var UnitTypeController = new UnitTypeController(mediator.Object);
            var updateCurrncyReq = new UpdateUnitTypeRequest();

            var result = await UnitTypeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUnitTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetUnitTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var UnitTypeController = new UnitTypeController(mediator.Object);
            var getCurrncyReq = new GetUnitTypeRequest();

            var result = await UnitTypeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchUnitTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchUnitTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var UnitTypeController = new UnitTypeController(mediator.Object);
            var searchCurrncyReq = new SearchUnitTypeRequest();

            var result = await UnitTypeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
