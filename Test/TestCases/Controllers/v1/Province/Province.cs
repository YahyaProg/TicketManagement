using Api.Controllers.v1;
using Application.Services.ProvinceService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.Province;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Province
{
    public class ProvinceControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ProvinceVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ProvinceVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddProvinceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddProvinceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ProvinceController = new ProvinceController(mediator.Object);
            var addCurrncyReq = new AddProvinceRequest();

            var result = await ProvinceController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProvinceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteProvinceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ProvinceController = new ProvinceController(mediator.Object);
            var deleteCurrncyReq = new DeleteProvinceRequest();

            var result = await ProvinceController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownProvinceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownProvinceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var ProvinceController = new ProvinceController(mediator.Object);

            var dropDownProvinceReq = new DropDownProvinceRequest();

            var result = await ProvinceController.DropDown(dropDownProvinceReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateProvinceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateProvinceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ProvinceController = new ProvinceController(mediator.Object);
            var updateCurrncyReq = new UpdateProvinceRequest();

            var result = await ProvinceController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProvinceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetProvinceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ProvinceController = new ProvinceController(mediator.Object);
            var getCurrncyReq = new GetProvinceRequest();

            var result = await ProvinceController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchProvinceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchProvinceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ProvinceController = new ProvinceController(mediator.Object);
            var searchCurrncyReq = new SearchProvinceRequest();

            var result = await ProvinceController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
