using Api.Controllers.v1;
using Application.Services.KomiteVosoolAdamTasvieService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.KomiteVosoolAdamTasvie;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.KomiteVosoolAdamTasvie;

public class KomiteVosoolAdamTasvieControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<KomiteVosoolAdamTasvieVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<KomiteVosoolAdamTasvieVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task AddKomiteVosoolAdamTasvieTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddKomiteVosoolAdamTasvieRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var KomiteVosoolAdamTasvieController = new KomiteVosoolAdamTasvieController(mediator.Object);

        var addKomiteVosoolAdamTasvieReq = new AddKomiteVosoolAdamTasvieRequest();

        var result = await KomiteVosoolAdamTasvieController.Add(addKomiteVosoolAdamTasvieReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetKomiteVosoolAdamTasvieTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetKomiteVosoolAdamTasvieRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

        var KomiteVosoolAdamTasvieController = new KomiteVosoolAdamTasvieController(mediator.Object);

        var getKomiteVosoolAdamTasvieReq = new GetKomiteVosoolAdamTasvieRequest();

        var result = await KomiteVosoolAdamTasvieController.Get(getKomiteVosoolAdamTasvieReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SearchKomiteVosoolAdamTasvieTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<SearchKomiteVosoolAdamTasvieRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        var KomiteVosoolAdamTasvieController = new KomiteVosoolAdamTasvieController(mediator.Object);

        var searchKomiteVosoolAdamTasvieReq = new SearchKomiteVosoolAdamTasvieRequest();

        var result = await KomiteVosoolAdamTasvieController.Search(searchKomiteVosoolAdamTasvieReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DropDownKomiteVosoolAdamTasvieTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DropDownKomiteVosoolAdamTasvieRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

        var KomiteVosoolAdamTasvieController = new KomiteVosoolAdamTasvieController(mediator.Object);

        var dropDownKomiteVosoolAdamTasvieReq = new DropDownKomiteVosoolAdamTasvieRequest();

        var result = await KomiteVosoolAdamTasvieController.DropDown(dropDownKomiteVosoolAdamTasvieReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateKomiteVosoolAdamTasvieTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateKomiteVosoolAdamTasvieRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var KomiteVosoolAdamTasvieController = new KomiteVosoolAdamTasvieController(mediator.Object);

        var updateKomiteVosoolAdamTasvieReq = new UpdateKomiteVosoolAdamTasvieRequest();

        var result = await KomiteVosoolAdamTasvieController.Update(updateKomiteVosoolAdamTasvieReq);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteKomiteVosoolAdamTasvieTest()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteKomiteVosoolAdamTasvieRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

        var KomiteVosoolAdamTasvieController = new KomiteVosoolAdamTasvieController(mediator.Object);

        var deleteKomiteVosoolAdamTasvieReq = new DeleteKomiteVosoolAdamTasvieRequest();

        var result = await KomiteVosoolAdamTasvieController.Delete(deleteKomiteVosoolAdamTasvieReq);

        Assert.IsType<OkObjectResult>(result);
    }
}
