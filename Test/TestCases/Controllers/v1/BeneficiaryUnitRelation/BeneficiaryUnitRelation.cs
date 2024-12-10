using Api.Controllers.v1;
using Application.Services.BeneficiaryUnitRelationService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.BeneficiaryUnitRelation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BeneficiaryUnitRelation;

public class BeneficiaryUnitRelationControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<BeneficiaryUnitRelationVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<BeneficiaryUnitRelationVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddBeneficiaryUnitRelationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddBeneficiaryUnitRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BeneficiaryUnitRelationController = new BeneficiaryUnitRelationController(mediator.Object);

        var addBeneficiaryUnitRelationReq = new AddBeneficiaryUnitRelationRequest();

        var result = await BeneficiaryUnitRelationController.Add(addBeneficiaryUnitRelationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetBeneficiaryUnitRelationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetBeneficiaryUnitRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var BeneficiaryUnitRelationController = new BeneficiaryUnitRelationController(mediator.Object);

        var getBeneficiaryUnitRelationReq = new GetBeneficiaryUnitRelationRequest();

        var result = await BeneficiaryUnitRelationController.Get(getBeneficiaryUnitRelationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchBeneficiaryUnitRelationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchBeneficiaryUnitRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var BeneficiaryUnitRelationController = new BeneficiaryUnitRelationController(mediator.Object);

        var searchBeneficiaryUnitRelationReq = new SearchBeneficiaryUnitRelationRequest();

        var result = await BeneficiaryUnitRelationController.Search(searchBeneficiaryUnitRelationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownBeneficiaryUnitRelationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownBeneficiaryUnitRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var BeneficiaryUnitRelationController = new BeneficiaryUnitRelationController(mediator.Object);

        var dropDownBeneficiaryUnitRelationReq = new DropDownBeneficiaryUnitRelationRequest();

        var result = await BeneficiaryUnitRelationController.DropDown(dropDownBeneficiaryUnitRelationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateBeneficiaryUnitRelationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateBeneficiaryUnitRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BeneficiaryUnitRelationController = new BeneficiaryUnitRelationController(mediator.Object);

        var updateBeneficiaryUnitRelationReq = new UpdateBeneficiaryUnitRelationRequest();

        var result = await BeneficiaryUnitRelationController.Update(updateBeneficiaryUnitRelationReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteBeneficiaryUnitRelationTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteBeneficiaryUnitRelationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var BeneficiaryUnitRelationController = new BeneficiaryUnitRelationController(mediator.Object);

        var deleteBeneficiaryUnitRelationReq = new DeleteBeneficiaryUnitRelationRequest();

        var result = await BeneficiaryUnitRelationController.Delete(deleteBeneficiaryUnitRelationReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
