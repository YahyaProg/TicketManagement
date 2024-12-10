using Application.Services.CreditManagementService;
using Core.Helpers;
using Core.Utils;
using Core.ViewModel.CreditManagement;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using TanvirArjel.EFCore.GenericRepository;
using Test.Helper;
using ZstdSharp.Unsafe;

namespace Test.TestCases.Services.CreditManagementService;

public class ActiveProposalSchemeAdvanceSearchTest
{

    readonly Mock<IUnitOfWork> unitofwork = new();
    readonly Mock<IUserHelper> helper = new();
    [Fact]
    public async Task ActiveProposalSchemeAdvanceSearch_HaveAny()
    {
        //Arrange
        var returnValue = new PaginatedList<ActiveProposalSchemeAdvanceSearchVM>([], 2, 1, 2);

        _ = unitofwork.Setup(x => x.CreditManagementRepo.ActiveProposalSchemeAdvanceSearch
         (It.IsAny<ActiveProposalSchemeAdvanceSearchIM>(), false, It.IsAny<CancellationToken>()))
         .ReturnsAsync(returnValue);

        helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto() { BranchCode = "" });
        var handler = new ActiveProposalSchemeAdvanceSearchRequestHandler(unitofwork.Object, helper.Object);

        //Act
        var result = await handler.Handle(new()
        {
            CorporateCustomerName = "mikaeel",
            OrganizationTitle = "007"
        }, CancellationToken.None);

        //Assert
        Assert.True(result.IsSuccess);
    }


    [Fact]
    public async Task AdminProposalSchemeAdvanceSearch_HaveAny()
    {
        //Arrange
        var returnValue = new PaginatedList<ActiveProposalSchemeAdvanceSearchVM>([], 2, 1, 2);

        var memory = new Mock<IMemoryCache>();
        var external = new Mock<IExternalServices>();

        _ = unitofwork.Setup(x => x.CreditManagementRepo.ActiveProposalSchemeAdvanceSearch
         (It.IsAny<ActiveProposalSchemeAdvanceSearchIM>(), false, It.IsAny<CancellationToken>()))
         .ReturnsAsync(returnValue);

        

        var externalres = MoqHelper.GetExternalResponseMoq(new BranchResponse()
        {
            Branch = new()
            {
                Status = true,
                Code = "salam",
                Name = "amir"
            },
            RelatedBranch = new List<BranchModel>()
            {
                new BranchModel(){Code = "salam"}
            }
        }, false);
        external.Setup(x => x.GetAllBranchRelations(It.IsAny<BranchCodeRequest>())).ReturnsAsync(externalres);

        helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto() { BranchCode = "", Id = "123" });
        var handler = new ActiveProposalSchemeAdminSearchRequestHandler(unitofwork.Object, helper.Object, memory.Object, external.Object);

        //Act
        var result = await handler.Handle(new()
        {
            CorporateCustomerName = "mikaeel",
            OrganizationTitle = "007"
        }, CancellationToken.None);

        //Assert
        Assert.True(result.IsSuccess);
    }
}