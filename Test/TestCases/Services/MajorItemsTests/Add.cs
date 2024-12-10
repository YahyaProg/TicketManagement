using Amazon.Runtime.Internal.Util;
using Application.Services.MajorItemsService;
using Core.GenericResultModel;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.MajorItemsTests;

public class AddMajorItemsTests
{
    private readonly AddMajorItemsRequest request = new()
    {
        CompanyFinancialInfoId = 1,
        CustomerId = 1,
        ProposalId = 1,
        ProposalSchemeId = 1,
    };

    private readonly Mock<DBContext> context = new();
    private readonly AddMajorItemsRequestHandler handler;
    public AddMajorItemsTests()
    {
        handler = new(context.Object);
    }

    [Fact]
    public async Task NotFound()
    {
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task TypeNotValid()
    {
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([new() {
            Id = request.CompanyFinancialInfoId,
            Type = Core.Enums.ECompanyFinancialInfo_type.balance_sheet,
        }]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task Customer_NotFound()
    {
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([new() {
            Id = request.CompanyFinancialInfoId,
            Type = Core.Enums.ECompanyFinancialInfo_type.MajorItemsTrialBalance,
        }]);

        context.Setup(x => x.Customers).ReturnsDbSet([]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task Proposal_NotFound()
    {
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([new() {
            Id = request.CompanyFinancialInfoId,
            Type = Core.Enums.ECompanyFinancialInfo_type.MajorItemsTrialBalance,
        }]);

        context.Setup(x => x.Customers).ReturnsDbSet([new() {
            Id = request.CustomerId,
        }]);

        context.Setup(x => x.Proposals).ReturnsDbSet([]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task ProposalScheme_NotFound()
    {
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([new() {
            Id = request.CompanyFinancialInfoId,
            Type = Core.Enums.ECompanyFinancialInfo_type.MajorItemsTrialBalance,
        }]);

        context.Setup(x => x.Customers).ReturnsDbSet([new() {
            Id = request.CustomerId,
        }]);

        context.Setup(x => x.Proposals).ReturnsDbSet([new() {
            Id = request.ProposalId,
        }]);
        
        context.Setup(x => x.ProposalSchemes).ReturnsDbSet([]);

        
        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task Success()
    {
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([new() {
            Id = request.CompanyFinancialInfoId,
            Type = Core.Enums.ECompanyFinancialInfo_type.MajorItemsTrialBalance,
        }]);

        context.Setup(x => x.Customers).ReturnsDbSet([new() {
            Id = request.CustomerId,
        }]);

        context.Setup(x => x.Proposals).ReturnsDbSet([new() {
            Id = request.ProposalId,
        }]);
        
        context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() {
            Id = request.ProposalSchemeId,
        }]);

        context.Setup(x => x.MajorItems).ReturnsDbSet([]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.True(res.IsSuccess);
    }
}
