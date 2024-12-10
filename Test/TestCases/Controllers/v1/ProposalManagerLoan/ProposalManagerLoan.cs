using Api.Controllers.v1;
using Application.Services.ProposalManagerLoanService;
using Core.GenericResultModel;
using Core.ViewModel.ProposalManagerLoan;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;


namespace Test.TestCases.Controllers.v1.ProposalManagerLoan
{
    public class ProposalManagerLoanRequestTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ProposalManagerLoanVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ProposalManagerLoanVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddProposalManagerLoanTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddProposalManagerLoanRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ProposalManagerLoanController = new ProposalManagerLoanController(mediator.Object);
            var addCurrncyReq = new AddProposalManagerLoanRequest();

            var result = await ProposalManagerLoanController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProposalManagerLoanTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateProposalManagerLoanRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ProposalManagerLoanController = new ProposalManagerLoanController(mediator.Object);
            var updateCurrncyReq = new UpdateProposalManagerLoanRequest();

            var result = await ProposalManagerLoanController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProposalManagerLoanTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetProposalManagerLoanRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ProposalManagerLoanController = new ProposalManagerLoanController(mediator.Object);
            var getCurrncyReq = new GetProposalManagerLoanRequest();

            var result = await ProposalManagerLoanController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchProposalManagerLoanTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchProposalManagerLoanRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ProposalManagerLoanController = new ProposalManagerLoanController(mediator.Object);
            var searchCurrncyReq = new SearchProposalManagerLoanRequest();

            var result = await ProposalManagerLoanController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
