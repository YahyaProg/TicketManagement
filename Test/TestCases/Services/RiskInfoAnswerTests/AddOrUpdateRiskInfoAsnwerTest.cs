using Application.Services.RiskInfoAnswerService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.RiskInfoAnswerTests;

public class AddOrUpdateRiskInfoAsnwerTest
{
    readonly AddOrUpdateRiskInfoAsnwerRequest request = new()
    {
        ProposalSchemeId = 1,
        List = [
            new(){
                Id = 1,
                AutoCalculate = Core.Enums.ERiskInfoItem_autoCalculate.NON_CURRENT_CUSTOMER_DEBTS,
                Description = "",
                RiskInfoItemId = 1,
                Response = 12,
                RiskInfoSubItemId = 1,
                Value = 1
            },
            new(){
                Id = null,
                AutoCalculate = Core.Enums.ERiskInfoItem_autoCalculate.NON_CURRENT_CUSTOMER_DEBTS,
                Description = "",
                RiskInfoItemId = 1,
                Response = 12,
                RiskInfoSubItemId = 1,
                Value = 1
            }
            ]
    };

    readonly Mock<DBContext> context = new();
    readonly AddOrUpdateRiskInfoAsnwerRequestHandler handler;
    public AddOrUpdateRiskInfoAsnwerTest()
    {
        handler = new(context.Object);
    }

    [Fact]
    public async Task ProposalNotFound()
    {
        context.Setup(x => x.RiskInfos).ReturnsDbSet([new() { Id = 1 }]);
        context.Setup(x => x.ProposalSchemes).ReturnsDbSet([]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task CustomerRequestNotFound()
    {
        context.Setup(x => x.RiskInfos).ReturnsDbSet([new() { Id = 1 }]);
        context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() { Id = 1, CustomerId = 1, ProposalId = 1 }]);
        context.Setup(x => x.CustomerRequestDetails).ReturnsDbSet([]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task Success()
    {
        context.Setup(x => x.RiskInfos).ReturnsDbSet([new() { Id = 1 }]);
        context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() { Id = 1, CustomerId = 1, ProposalId = 1 }]);
        context.Setup(x => x.CustomerRequestDetails).ReturnsDbSet([new() {
            ProposalId = 1, CustomerId = 1, Amount = 1000
        }]);
        context.Setup(x => x.CorporateCustomers).ReturnsDbSet([]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);
        var res = await handler.Handle(request, CancellationToken.None);

        Assert.True(res.IsSuccess);
    }

}
