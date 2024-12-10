using Api.Controllers.v1;
using Application.Services.ProposalTypeService;
using Core.GenericResultModel;
using Core.ViewModel.ProposalType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ProposalType
{
    public class ProposalTypeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ProposalTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ProposalTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddProposalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddProposalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ProposalTypeController = new ProposalTypesController(mediator.Object);
            var addCurrncyReq = new AddProposalTypeRequest();

            var result = await ProposalTypeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProposalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteProposalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ProposalTypeController = new ProposalTypesController(mediator.Object);
            var deleteCurrncyReq = new DeleteProposalTypeRequest();

            var result = await ProposalTypeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProposalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateProposalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ProposalTypeController = new ProposalTypesController(mediator.Object);
            var updateCurrncyReq = new UpdateProposalTypeRequest();

            var result = await ProposalTypeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProposalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetProposalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ProposalTypeController = new ProposalTypesController(mediator.Object);
            var getCurrncyReq = new GetProposalTypeRequest();

            var result = await ProposalTypeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchProposalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchProposalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ProposalTypeController = new ProposalTypesController(mediator.Object);
            var searchCurrncyReq = new SearchProposalTypeRequest();

            var result = await ProposalTypeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
