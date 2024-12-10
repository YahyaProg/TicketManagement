
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using Repo = Infrastructure.Repositories.CreditManagementRepository;

namespace Test.TestCases.Repositories.CreditManagementRepo;

public class CreditManagementRepoTest
{

    private readonly Mock<DBContext> _contextMoq = new();

    [Fact]
    public async Task ActiveProposalSchemeAdvanceSearch_HaveAny()
    {
        //Arrange
        var proposalSchemeData = new List<Core.Entities.ProposalScheme>(){
            new(){Id= 1, OrganizationId=1,CustomerId=1,BankStaffId=1,Proposal = new(){ MosavabeNo = "",Status = EProposal_status.todo}, CreateDate = DateTime.Now }
        };

        var bankStaffData = new List<Core.Entities.BankStaff>(){
            new(){Id=1, FirstName = "amir", LastName = "hosein"}
        };

        var corporateCustomerData = new List<Core.Entities.CorporateCustomer>(){
            new(){Id=1, Name = "yes"}
        };
        var organizationsData = new List<Core.Entities.Organization>(){
            new(){Id=1}
        };

        _contextMoq.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemeData);
        _contextMoq.Setup(x => x.BankStaffs).ReturnsDbSet(bankStaffData);
        _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet(corporateCustomerData);
        _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>()
        {
            new IndividualCustomer()
            {
                Id =  1,
                FirstName = "amir",
                LastName = "hosein"
            }
        });
        _contextMoq.Setup(x => x.Organizations).ReturnsDbSet(organizationsData);
        _contextMoq.Setup(x => x.Customers).ReturnsDbSet([new Customer { Id = 1, BranchId = 1 }]);
        _contextMoq.Setup(x => x.Branches).ReturnsDbSet([new Branch { Id = 1 }]);

        var userHelper = new Mock<IUserHelper>();
        userHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto
        {
            Id = ""
        });
        //Act
        var creditManagementRepo = new Repo.CreditManagementRepo(_contextMoq.Object, userHelper.Object);
        var searchResult = await creditManagementRepo.ActiveProposalSchemeAdvanceSearch(new(), false, CancellationToken.None);

        //Assert
        Assert.NotEmpty(searchResult.Items);
        Assert.NotEqual(0, searchResult.TotalItems);
    }
}