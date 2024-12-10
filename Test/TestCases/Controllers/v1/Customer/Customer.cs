using Api.Controllers.v1;
using Application.Services.CustomerService;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Customer
{
    public class CustomerControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CustomerVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CustomerGeneralInformationVM> getInformationSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CustomerVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCustomerTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCustomerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CustomerController = new CustomerController(mediator.Object);
            var addCurrncyReq = new AddCustomerRequest();

            var result = await CustomerController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCustomerTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCustomerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CustomerController = new CustomerController(mediator.Object);
            var deleteCurrncyReq = new DeleteCustomerRequest();

            var result = await CustomerController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task UpdateCustomerTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCustomerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CustomerController = new CustomerController(mediator.Object);
            var updateCurrncyReq = new UpdateCustomerRequest();

            var result = await CustomerController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCustomerTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCustomerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CustomerController = new CustomerController(mediator.Object);
            var getCurrncyReq = new GetCustomerRequest();

            var result = await CustomerController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCustomerTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCustomerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CustomerController = new CustomerController(mediator.Object);
            var searchCurrncyReq = new SearchCustomerRequest();

            var result = await CustomerController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GeneralInformationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<CustomerGeneralInformationRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(getInformationSuccessRes));

            var CustomerController = new CustomerController(mediator.Object);
            var getInformationCurrncyReq = new CustomerGeneralInformationRequest();

            var result = await CustomerController.GeneralInformation(getInformationCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task UpdateGeneralInformation()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCustomerGeneralInformationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CustomerController = new CustomerController(mediator.Object);

            var updateCustomerGeneralInformationRequest = new UpdateCustomerGeneralInformationRequest();

            var result = await CustomerController.UpdateGeneralInformation(updateCustomerGeneralInformationRequest);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
