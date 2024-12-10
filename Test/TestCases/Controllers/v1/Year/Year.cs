using Api.Controllers.v1;
using Application.Services.YearService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.Year;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Year
{
    public class YearControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<YearVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<YearVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<string>>> SpecialDropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddYearTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddYearRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var YearController = new YearController(mediator.Object);
            var addCurrncyReq = new AddYearRequest();

            var result = await YearController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteYearTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteYearRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var YearController = new YearController(mediator.Object);
            var deleteCurrncyReq = new DeleteYearRequest();

            var result = await YearController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownYearTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownYearRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var YearController = new YearController(mediator.Object);

            var dropDownYearReq = new DropDownYearRequest();

            var result = await YearController.DropDown(dropDownYearReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateYearTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateYearRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var YearController = new YearController(mediator.Object);
            var updateCurrncyReq = new UpdateYearRequest();

            var result = await YearController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetYearTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetYearRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var YearController = new YearController(mediator.Object);
            var getCurrncyReq = new GetYearRequest();

            var result = await YearController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchYearTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchYearRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var YearController = new YearController(mediator.Object);
            var searchCurrncyReq = new SearchYearRequest();

            var result = await YearController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SpecialDropDownYearTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SpecialDropDownYearRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(SpecialDropDownSuccessRes);

            var YearController = new YearController(mediator.Object);

            var specialDropDownYearReq = new SpecialDropDownYearRequest();

            var result = await YearController.SpecialDropDown(specialDropDownYearReq);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
