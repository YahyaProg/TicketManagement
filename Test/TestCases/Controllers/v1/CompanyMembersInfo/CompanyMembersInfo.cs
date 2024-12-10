using Api.Controllers.v1;
using Application.Services.CompanyMembersInfoService;
using Application.Services.Manager;
using Core.GenericResultModel;
using Core.ViewModel.CompanyMembersInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CompanyMembersInfo;

public class CompanyMembersInfoControllerTest
{
    readonly Mock<IMediator> mediator = new();
    readonly ApiResult SuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<GetOneManagerVM> getOneSuccessRes = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<GetAllBoardMembersAndManagersVM>> getSuccessRes1 = new() { IsSuccess = true, Code = 0 };
    readonly ApiResult<PaginatedList<MajorShareholdersGetAllVM>> getSuccessRes2 = new() { IsSuccess = true, Code = 0 };

    [Fact]
    public async Task GetAllBoardMembersAndManagers_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAllBoardMembersAndManagersRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes1);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var getAllBoardMembersAndManagersRequest = new GetAllBoardMembersAndManagersRequest();

        var result = await companyMembersInfoController.GetAllBoardMembersAndManagers(getAllBoardMembersAndManagersRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAllMajorShareholders_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetAllMajorShareholdersRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes2);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var getAllMajorShareholdersRequest = new GetAllMajorShareholdersRequest();

        var result = await companyMembersInfoController.GetAllMajorShareholders(getAllMajorShareholdersRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddOneMajorShareholders_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddOneMajorShareholdersRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(SuccessRes);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var addOneMajorShareholdersRequest = new AddOneMajorShareholdersRequest();

        var result = await companyMembersInfoController.AddOneMajorShareholders(addOneMajorShareholdersRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddManager_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<AddManagerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(SuccessRes);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var addCompanyMemberInfoRequest = new AddManagerRequest();

        var result = await companyMembersInfoController.AddManager(addCompanyMemberInfoRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetOneManager_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<GetOneManagerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getOneSuccessRes);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var getOneCompanyMembersInfoRequest = new GetOneManagerRequest();

        var result = await companyMembersInfoController.GetOneManager(getOneCompanyMembersInfoRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateBoardMemberAndManager_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateBoardMemberAndManagerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(SuccessRes);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var updateBoardMemberAndManagerRequest = new UpdateBoardMemberAndManagerRequest();

        var result = await companyMembersInfoController.UpdateBoardMemberAndManager(updateBoardMemberAndManagerRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMajorStockHolder_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<UpdateMajorStockHolderRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(SuccessRes);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var updateMajorStockHolderRequest = new UpdateMajorStockHolderRequest();

        var result = await companyMembersInfoController.UpdateMajorStockHolder(updateMajorStockHolderRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteBoardMemberAndManager_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteBoardMemberAndManagerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(SuccessRes);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var deleteBoardMemberAndManagerRequest = new DeleteBoardMemberAndManagerRequest();

        var result = await companyMembersInfoController.DeleteBoardMemberAndManager(deleteBoardMemberAndManagerRequest);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMajorStockHolder_Test()
    {
        mediator.Setup(x => x.Send(It.IsAny<DeleteMajorStockHolderRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(SuccessRes);

        var companyMembersInfoController = new CompanyMembersInfoController(mediator.Object);

        var deleteMajorStockHolderRequest = new DeleteMajorStockHolderRequest();

        var result = await companyMembersInfoController.DeleteMajorStockHolder(deleteMajorStockHolderRequest);

        Assert.IsType<OkObjectResult>(result);
    }
}
