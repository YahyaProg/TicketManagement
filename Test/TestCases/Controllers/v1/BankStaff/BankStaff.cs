using Api.Controllers.v1;
using Application.Services.BankStaffService;
using Core.GenericResultModel;
using Core.ViewModel.BanlStaff;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BankStaff
{
    public class BankStaffControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult<BankStaffVM> successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult successUpdateRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<BankStaffVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<BankStaffVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddBankStaffTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddBankStaffRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(successRes));

            var BankStaffController = new BankStaffController(mediator.Object);
            var addCurrncyReq = new AddBankStaffRequest();

            var result = await BankStaffController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task DropDownBankStaffTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownBankStaffRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var BankStaffController = new BankStaffController(mediator.Object);

            var dropDownBankStaffReq = new DropDownBankStaffRequest();

            var result = await BankStaffController.DropDown(dropDownBankStaffReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateBankStaffTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateBankStaffRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(successUpdateRes));

            var BankStaffController = new BankStaffController(mediator.Object);
            var updateCurrncyReq = new UpdateBankStaffRequest();

            var result = await BankStaffController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBankStaffTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetBankStaffRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var BankStaffController = new BankStaffController(mediator.Object);
            var getCurrncyReq = new GetBankStaffRequest();

            var result = await BankStaffController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchBankStaffTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchBankStaffRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var BankStaffController = new BankStaffController(mediator.Object);
            var searchCurrncyReq = new SearchBankStaffRequest();

            var result = await BankStaffController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
