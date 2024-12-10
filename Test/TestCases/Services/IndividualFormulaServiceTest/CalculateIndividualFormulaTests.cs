using Amazon.Runtime.Internal.Util;
using Application.Services.IndividualFormulaService;
using Core.Enums;
using Core.GenericResultModel;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.IndividualFormulaServiceTest;

public class CalculateIndividualFormulaTests
{
    readonly CalculateIndividualRequest request = new()
    {
        ProposalSchemeId = 1,
    };

    readonly Mock<DBContext> context = new();
    readonly Mock<IUnitOfWork> unitOfWork = new();

    readonly CalculateIndividualRequestHandler handler;
    public CalculateIndividualFormulaTests()
    {
        handler = new CalculateIndividualRequestHandler(context.Object, unitOfWork.Object);
    }

    [Fact]
    public async Task ProposalScheme_NotFound()
    {
        context.Setup(x => x.ProposalSchemes).ReturnsDbSet([]);

        var res = await GetResult();

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task RiskInfoAnswers_NotComplete()
    {
        context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() {
            Id = 1,
            CustomerId = 1,
            ProposalId = 1,
        }]);
        context.Setup(x => x.CustomerRequestDetails).ReturnsDbSet([new() { Amount = 10, CustomerId = 1 }]);
        unitOfWork.Setup(x => x.RiskInfoGroupRepo.GetRiskInfoWithAnswers(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<ERiskInfoGroup_category>(), It.IsAny<double>()))
            .ReturnsAsync([new() {
                Id = 1,
                Title = "ajab",
                Items = [
                    new(){
                        Id = 1,
                        Answer = null,
                    }
                    ]
            }]);

        var res = await GetResult();

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task Success()
    {
        context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() {
            Id = 1,
            CustomerId = 1,
            ProposalId = 1,
        }]);

        context.Setup(x => x.CustomerRequestDetails).ReturnsDbSet([new() { Amount = 10, ProposalId = 1, CustomerId = 1 }]);
        unitOfWork.Setup(x => x.RiskInfoGroupRepo.GetRiskInfoWithAnswers(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<ERiskInfoGroup_category>(), It.IsAny<double>()))
            .ReturnsAsync([new() {
                Id = 1,
                Title = "ajab",
                Items = [
                    new(){
                        Id = 1,
                        Answer = new(){
                            Value = 10
                        },
                    },
                    new(){
                        Id = 2,
                        Answer = new(){
                            Value = 11
                        },
                    }
                    ]
            }]);

        context.Setup(x => x.IndividualFormula).ReturnsDbSet([
            new() { Code = "ali", Formula = "reza+1" , HaveFormulaRelation = true},
            new() { Code = "reza", Formula = "1+2+ajab+mamad" , HaveFormulaRelation = true},
            new() { Code = "mamad" , Formula = "1+ajab"}]);

        context.Setup(x => x.IndividualRelations).ReturnsDbSet([
            new() { FormulaCode = "reza" , ParamCode = "ajab" },
            new() { FormulaCode = "reza" , UsedFormulaCode = "mamad"},
            new() { FormulaCode = "mamad" , ParamCode = "ajab"}
        ]);

        context.Setup(x => x.IndividualParam).ReturnsDbSet([new() { Code = "ajab" }]);
        context.Setup(x => x.IndividualValue).ReturnsDbSet([new() { CustomerId = 1, ParamCode = "ajab", ProposalId = 1, ProposalSchemeId = 1, Value = 10 }]);
        context.Setup(x => x.IndividualFinancialValue).ReturnsDbSet([]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var res = await GetResult();

        Assert.True(res.IsSuccess);
    }

    private async Task<ApiResult> GetResult()
        => await handler.Handle(request, CancellationToken.None);
}
