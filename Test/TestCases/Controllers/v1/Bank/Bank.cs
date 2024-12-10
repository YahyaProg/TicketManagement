using Api.Controllers.v1;
using Application.Services.BankService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Bank
{
    public class BankControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<BankVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<BankVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BankController = new BankController(mediator.Object);
            var addCurrncyReq = new AddBankRequest();

            var result = await BankController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BankController = new BankController(mediator.Object);
            var deleteCurrncyReq = new DeleteBankRequest();

            var result = await BankController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var BankController = new BankController(mediator.Object);

            var dropDownBankReq = new DropDownBankRequest();

            var result = await BankController.DropDown(dropDownBankReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BankController = new BankController(mediator.Object);
            var updateCurrncyReq = new UpdateBankRequest();

            var result = await BankController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var BankController = new BankController(mediator.Object);
            var getCurrncyReq = new GetBankRequest();

            var result = await BankController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var BankController = new BankController(mediator.Object);
            var searchCurrncyReq = new SearchBankRequest();

            var result = await BankController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
