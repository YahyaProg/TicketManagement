using Application.Services.Report;
using Core.Entities;
using Core.Helpers;
using Core.ViewModel;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.ReportTest;


public class GetTotalReportRequestHandlerTests
{


    private Mock<IUserHelper> CreateMockUserHelper(string branchCode = "B001")
    {
        var mockHelper = new Mock<IUserHelper>();
        mockHelper.Setup(x => x.GetUserFromToken())
            .Returns(new UserDto { BranchCode = branchCode });
        return mockHelper;
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectCounts_WhenDatabaseIsPopulated()
    {
        // Arrange

        var userHelper = CreateMockUserHelper("B001");
        // Arrange
        var collection = MoqHelper.GetUnitOfWorkMoqCollection();

        collection.Context.Setup(x => x.Proposals).ReturnsDbSet([
               new Proposal { Id = 1 },
               new Proposal { Id = 2 }
            ]);

        collection.Context.Setup(x => x.RiskRankHists).ReturnsDbSet([
                new RiskRankHist { ProposalId = 1 },
                new RiskRankHist { ProposalId = 2 }
          ]);

        collection.Context.Setup(x => x.Customers).ReturnsDbSet([
            new Customer { Id = 1, BranchId = 1, BankStaffId = 1 },
            new Customer { Id = 2, BranchId = 2, BankStaffId = 2 }
          ]);

        collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([
            new CorporateCustomer { Id = 1 },
            new CorporateCustomer { Id = 2 }
        ]);

        collection.Context.Setup(x => x.BankStaffs).ReturnsDbSet([
          new() { Id = 1,BranchCode="B001" },
            new () { Id = 2,BranchCode="B002" }
         ]);

        collection.Context.Setup(x => x.Branches).ReturnsDbSet([
            new Branch { Id = 1, Code = "B001" },
            new Branch { Id = 2, Code = "B002" }
        ]);


        var handler = new GetTotalReportRequest.GetTotalReportRequestHandler(collection.Context.Object, userHelper.Object);

        // Act
        var result = await handler.Handle(new GetTotalReportRequest(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(4, result.Data.Count);

        Assert.Equal("مشتریان حقوقی", result.Data[0].Key);
        Assert.Equal("2", result.Data[0].Value);

        Assert.Equal("کل پرونده ها", result.Data[1].Key);
        Assert.Equal("2", result.Data[1].Value);

        Assert.Equal("پرونده های اعتبارسنجی شده", result.Data[2].Key);
        Assert.Equal("2", result.Data[2].Value);

        Assert.Equal("مشتریان این شعبه", result.Data[3].Key);
        Assert.Equal("1", result.Data[3].Value); // Only one customer belongs to branch "B001"
    }

  
}