using Core.Entities;
using Core.ViewModel.CompanyMembersInfo;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Repositories.CompanyMembersInfoRepo;

public class CompanyMembersInfoRepoTest
{
    private readonly Mock<DBContext> _contextMoq = new();

    [Fact]
    public async Task Get()
    {
        _contextMoq.Setup(x => x.Managers).ReturnsDbSet(
            [
                new()
                {
                    CustomerSchemeId = 0,
                    PersonId = 0,
                    PositionTypeId = 0,
                    CorpBoardOfDirectorId = 0
                }
            ]);
        _contextMoq.Setup(x => x.CustomerSchemes).ReturnsDbSet([new() { Id = 0, ProposalSchemeId = 0 }]);
        _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 0 }]);
        _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 0 }]);
        _contextMoq.Setup(x => x.PositionTypes).ReturnsDbSet([new() { Id = 0 }]);

        var companyMembersInfoRepo = new Infrastructure.Repositories.CompanyMembersInfoRepository.CompanyMembersInfoRepo(_contextMoq.Object);

        var result = await companyMembersInfoRepo.GetAllBoardMembersAndManagers
            (new GetAllBoardMembersAndManagersIM { ProposalSchemeId = 0, Page = 1, Size = 1 });

        Assert.NotEmpty(result.Items);
    }

    [Fact]
    public async Task GetAllMajorShareholders()
    {
        _contextMoq.Setup(x => x.MajorStocksHolders).ReturnsDbSet(
            new List<MajorStocksHolder>
            {
                new()
                {
                    CustomerSchemeId = 0,
                    PersonId = 0
                }
            });
        _contextMoq.Setup(x => x.Customers).ReturnsDbSet(new List<Customer> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.CustomerSchemes).ReturnsDbSet(new List<CustomerScheme> { new() { Id = 0, ProposalSchemeId = 0 } });

        var companyMembersInfoRepo = new Infrastructure.Repositories.CompanyMembersInfoRepository.CompanyMembersInfoRepo(_contextMoq.Object);

        var result = await companyMembersInfoRepo.GetAllMajorShareholders(new CompanyMembersInfoGetIM { ProposalSchemeId = 0, Page = 1, Size = 1 });

        Assert.NotEmpty(result.Items);
    }

    [Fact]
    public async Task SumOfStocksCount()
    {
        _contextMoq.Setup(x => x.MajorStocksHolders).ReturnsDbSet(
            new List<MajorStocksHolder>
            {
                new()
                {
                    CustomerSchemeId = 0,
                    StocksCount = 0
                }
            });
        _contextMoq.Setup(x => x.CustomerSchemes).ReturnsDbSet(new List<CustomerScheme> { new() { Id = 0, ProposalSchemeId = 0 } });

        var companyMembersInfoRepo = new Infrastructure.Repositories.CompanyMembersInfoRepository.CompanyMembersInfoRepo(_contextMoq.Object);

        var result = await companyMembersInfoRepo.SumOfStocksCount(0);

        Assert.Equal(0, result);
    }

    [Fact]
    public async Task GetOne()
    {
        _contextMoq.Setup(x => x.Managers).ReturnsDbSet(
            new List<Manager>
            {
                new()
                {
                    Id = 0,
                    PersonId = 0,
                    PositionTypeId = 0,
                    CorpBoardOfDirectorId = 0
                }
            });
        _contextMoq.Setup(x => x.Customers).ReturnsDbSet(new List<Customer> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.PositionTypes).ReturnsDbSet(new List<PositionType> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer> { new() { Id = 0, CompanyTypeId = 0 } });
        _contextMoq.Setup(x => x.CompanyTypes).ReturnsDbSet(new List<CompanyType> { new() { Id = 0 } });

        var companyMembersInfoRepo = new Infrastructure.Repositories.CompanyMembersInfoRepository.CompanyMembersInfoRepo(_contextMoq.Object);

        var result = await companyMembersInfoRepo.GetOne(0);

        Assert.Equal(0, result.ManagerId);
    }
}
