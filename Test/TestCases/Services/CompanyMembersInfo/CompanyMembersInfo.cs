using Application.Services.CompanyMembersInfoService;
using Core.ViewModel.CompanyMembersInfo;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CompanyMembersInfoTest;

public class CompanyMembersInfo
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task GetAllBoardMembersAndManagersRequest()
    {
        var command = new GetAllBoardMembersAndManagersRequest { ProposalSchemeId = 0 };

        _unitOfWork.Setup(x => x.CompanyMembersInfoRepo.GetAllBoardMembersAndManagers(It.IsAny<GetAllBoardMembersAndManagersIM>()));

        var handler = new GetAllBoardMembersAndManagersRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetAllMajorShareholdersRequest()
    {
        var command = new GetAllMajorShareholdersRequest { ProposalSchemeId = 0 };

        _unitOfWork.Setup(x => x.CompanyMembersInfoRepo.GetAllMajorShareholders(It.IsAny<CompanyMembersInfoGetIM>()));

        var handler = new GetAllMajorShareholdersRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetOneCompanyMembersInfoRequest()
    {
        var command = new GetOneManagerRequest { ManagerId = 0 };

        _unitOfWork.Setup(x => x.CompanyMembersInfoRepo.GetOne(It.IsAny<long>())).ReturnsAsync(
            new GetOneManagerVM
            {
                ManagerId = 10,
                Foreign = true,
                NationalId = "10",
                BirthDate = null,
                PositionTypeTitle = "10",
                CorporateAgent = true,
                CorpId = "10",
                CompanyTypeTitle = "10",
                Name = "10"
            });

        var handler = new GetOneCompanyMembersInfoRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
