using Api.Controllers.v1;
using Application.Services.PersonRelationService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.PersonRelation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.PersonRelation
{
    public class PersonRelationControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PersonRelationVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<PersonRelationVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddPersonRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddPersonRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var PersonRelationController = new PersonRelationController(mediator.Object);
            var addCurrncyReq = new AddPersonRelationRequest();

            var result = await PersonRelationController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeletePersonRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeletePersonRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var PersonRelationController = new PersonRelationController(mediator.Object);
            var deleteCurrncyReq = new DeletePersonRelationRequest();

            var result = await PersonRelationController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownPersonRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownPersonRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var PersonRelationController = new PersonRelationController(mediator.Object);

            var dropDownPersonRelationReq = new DropDownPersonRelationRequest();

            var result = await PersonRelationController.DropDown(dropDownPersonRelationReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdatePersonRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdatePersonRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var PersonRelationController = new PersonRelationController(mediator.Object);
            var updateCurrncyReq = new UpdatePersonRelationRequest();

            var result = await PersonRelationController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPersonRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetPersonRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var PersonRelationController = new PersonRelationController(mediator.Object);
            var getCurrncyReq = new GetPersonRelationRequest();

            var result = await PersonRelationController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchPersonRelationTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchPersonRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var PersonRelationController = new PersonRelationController(mediator.Object);
            var searchCurrncyReq = new SearchPersonRelationRequest();

            var result = await PersonRelationController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
