using Api.Controllers.v1;
using Application.Services.BankGuaranteeTypeService;
using Core.GenericResultModel;
using Core.ViewModel.BankGuaranteeType;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BankGuaranteeTypeGuaranteeType
{
    public class BankGuaranteeTypeGuaranteeTypeControllerTest
    {

        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<BankGuaranteeTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<BankGuaranteeTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddBankGuaranteeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddBankGuaranteeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BankGuaranteeTypeController = new BankGuaranteeTypeController(mediator.Object);
            var addCurrncyReq = new AddBankGuaranteeTypeRequest();

            var result = await BankGuaranteeTypeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBankGuaranteeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteBankGuaranteeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BankGuaranteeTypeController = new BankGuaranteeTypeController(mediator.Object);
            var deleteCurrncyReq = new DeleteBankGuaranteeTypeRequest();

            var result = await BankGuaranteeTypeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownBankGuaranteeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownBankGuaranteeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var BankGuaranteeTypeController = new BankGuaranteeTypeController(mediator.Object);

            var dropDownBankGuaranteeTypeReq = new DropDownBankGuaranteeTypeRequest();

            var result = await BankGuaranteeTypeController.DropDown(dropDownBankGuaranteeTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateBankGuaranteeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateBankGuaranteeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BankGuaranteeTypeController = new BankGuaranteeTypeController(mediator.Object);
            var updateCurrncyReq = new UpdateBankGuaranteeTypeRequest();

            var result = await BankGuaranteeTypeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBankGuaranteeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetBankGuaranteeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var BankGuaranteeTypeController = new BankGuaranteeTypeController(mediator.Object);
            var getCurrncyReq = new GetBankGuaranteeTypeRequest();

            var result = await BankGuaranteeTypeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchBankGuaranteeTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchBankGuaranteeTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var BankGuaranteeTypeController = new BankGuaranteeTypeController(mediator.Object);
            var searchCurrncyReq = new SearchBankGuaranteeTypeRequest();

            var result = await BankGuaranteeTypeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
