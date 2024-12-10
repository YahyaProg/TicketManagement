using Api.Controllers.v1;
using Application.Services.IncomeTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.IncomeType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.IncomeType
{
    public class IncomeTypeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<IncomeTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<IncomeTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddIncomeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddIncomeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var incomeTypeController = new IncomeTypeController(mediator.Object);
            var addCurrncyReq = new AddIncomeTypeRequest();

            var result = await incomeTypeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteIncomeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteIncomeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var IncomeTypeController = new IncomeTypeController(mediator.Object);
            var deleteCurrncyReq = new DeleteIncomeTypeRequest();

            var result = await IncomeTypeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownIncomeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownIncomeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var IncomeTypeController = new IncomeTypeController(mediator.Object);

            var dropDownIncomeTypeReq = new DropDownIncomeTypeRequest();

            var result = await IncomeTypeController.DropDown(dropDownIncomeTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateIncomeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateIncomeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var IncomeTypeController = new IncomeTypeController(mediator.Object);
            var updateCurrncyReq = new UpdateIncomeTypeRequest();

            var result = await IncomeTypeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetIncomeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetIncomeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var IncomeTypeController = new IncomeTypeController(mediator.Object);
            var getCurrncyReq = new GetIncomeTypeRequest();

            var result = await IncomeTypeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchIncomeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchIncomeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var IncomeTypeController = new IncomeTypeController(mediator.Object);
            var searchCurrncyReq = new SearchIncomeTypeRequest();

            var result = await IncomeTypeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
