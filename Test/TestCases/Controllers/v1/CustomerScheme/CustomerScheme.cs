using Api.Controllers.v1;
using Application.Services.CustomerSchemeService;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1
{
    public class CustomerSchemeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult<long> addOrUpdateSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CustomerSchemeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CustomerSchemeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddOrUpdateCustomerSchemeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddOrUpdateCustomerSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(addOrUpdateSuccessRes);

            var customerSchemeController = new CustomerSchemeController(mediator.Object);
            var addCustomerSchemeReq = new AddOrUpdateCustomerSchemeRequest();

            var result = await customerSchemeController.AddOrUpdate(addCustomerSchemeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCustomerSchemeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCustomerSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var customerSchemeController = new CustomerSchemeController(mediator.Object);
            var deleteCustomerSchemeReq = new DeleteCustomerSchemeRequest();

            var result = await customerSchemeController.Delete(deleteCustomerSchemeReq);


            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task GetCustomerSchemeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCustomerSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var customerSchemeController = new CustomerSchemeController(mediator.Object);
            var getCustomerSchemeReq = new GetCustomerSchemeRequest();

            var result = await customerSchemeController.Get(getCustomerSchemeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]                   
        public async Task GetCustomerSchemeByProposalSchemeIdTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCustomerSchemeByProposalSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);
            var customerSchemeController = new CustomerSchemeController(mediator.Object);

            var result = await customerSchemeController.GetByProposalSchemeId(It.IsAny<long>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CustomerSchemeRegisterHistoryTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddOrUpdateCustomerSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(addOrUpdateSuccessRes);
            var customerSchemeController = new CustomerSchemeController(mediator.Object);
            CustomerSchemeRegisterHistory registerHistory = new();
            var result = await customerSchemeController.RegisterHistory(registerHistory);
            Assert.IsType<OkObjectResult>(result);
        }

        //RegisterHistory

        [Fact]
        public async Task SearchCustomerSchemeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCustomerSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var customerSchemeController = new CustomerSchemeController(mediator.Object);
            var searchCustomerSchemeReq = new SearchCustomerSchemeRequest();

            var result = await customerSchemeController.Search(searchCustomerSchemeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCustomerSchemeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCustomerSchemeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var customerSchemeController = new CustomerSchemeController(mediator.Object);
            var searchCustomerSchemeReq = new UpdateCustomerSchemeRequest();

            var result = await customerSchemeController.Update(searchCustomerSchemeReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
