using Api.Controllers.v1;
using Application.Services.CalculationTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CalculationType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CalculationType
{
    public class CalculationType
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CalculationTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CalculationTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCalculationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCalculationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CalculationTypeController = new CalculationTypesController(mediator.Object);
            var addCurrncyReq = new AddCalculationTypeRequest();

            var result = await CalculationTypeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCalculationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCalculationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CalculationTypeController = new CalculationTypesController(mediator.Object);
            var deleteCurrncyReq = new DeleteCalculationTypeRequest();

            var result = await CalculationTypeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownCalculationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownCalculationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var CalculationTypeController = new CalculationTypesController(mediator.Object);

            var dropDownCalculationTypeReq = new DropDownCalculationTypeRequest();

            var result = await CalculationTypeController.DropDown(dropDownCalculationTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateCalculationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCalculationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CalculationTypeController = new CalculationTypesController(mediator.Object);
            var updateCurrncyReq = new UpdateCalculationTypeRequest();

            var result = await CalculationTypeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCalculationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCalculationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CalculationTypeController = new CalculationTypesController(mediator.Object);
            var getCurrncyReq = new GetCalculationTypeRequest();

            var result = await CalculationTypeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCalculationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCalculationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CalculationTypeController = new CalculationTypesController(mediator.Object);
            var searchCurrncyReq = new SearchCalculationTypeRequest();

            var result = await CalculationTypeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
