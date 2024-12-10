using Application.Services.CapacityService;
using Application.Services.FinancialService;
using Core.Entities;
using Core.Enums;
using Core.GenericResultModel;
using Core.Helpers;
using Core.ViewModel.FinancialModels;
using Infrastructure;
using MediatR;
using Microsoft.Identity.Client;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.CapacityTests;

public class CalculateCapacityTests
{
    readonly ECapacityMeasurement[] neededCapacityTypes =
    [
            ECapacityMeasurement.AnnualInflationRateOfTheIndustry,
            ECapacityMeasurement.TotalShortTermDebtBalanceWithTheBankingSystem,
            ECapacityMeasurement.BalanceOfThePaymentObligationGuaranteeWithTheBankingSystem,
            ECapacityMeasurement.BalanceOfGamPapersWithTheBankingSystem,
            ECapacityMeasurement.UzanceBalanceWithTheBankingSystem,
            ECapacityMeasurement.ShortTermDebtBalanceOfBankContractsOtherThanDebtPurchaseWithTheBankingSystem,
            ECapacityMeasurement.DebtBalanceOfDebtPurchaseFacilityWithTheBankingSystem,
            ECapacityMeasurement.TotalDebtBalanceWithBankParsian,
            ECapacityMeasurement.DebtBalanceOfDebtPurchaseFacilityWithBankParsian
    ];
    readonly List<FinancialYearInfo> years;
    readonly List<CompanyFinancialInfo> companyFinancialInfos = new();
    readonly SearchCalcFinancialVM calcFinancial = new();
    readonly Proposal proposal;
    readonly List<Capacity> Capacities;
    readonly List<CreditSubType> creditSubTypes =
    [
        new(){Id = 1,CreditTypeId = 1,Code = "stcdp" },
        new(){Id = 2,CreditTypeId = 2,Code = "stcmp" },
        new(){Id = 3,CreditTypeId = 3,Code = "stcp" },
        new(){Id = 4,CreditTypeId = 4,Code = "nogomrok" },
        new(){Id = 5,CreditTypeId = 5,Code = "gomrok" } 
    ];

    readonly CalculateCapacityMeasurementRequest request;
    readonly InitSetting initSetting = new() { CompanyTypePercent = new() { IsManufacturing = 0.1, IsNotManufacturing = 0.1, } };
    readonly Mock<DBContext> context = new();
    readonly Mock<IMediator> mediator = new();
    readonly CalculateCapacityMeasurementRequestHandler handler;

    public CalculateCapacityTests()
    {
        proposal = new()
        {
            Id = 1,
            CustomerId = 1,
        };
        request = new() { ProposalId = proposal.Id };
        handler = new(context.Object, mediator.Object, initSetting);

        Capacities = neededCapacityTypes.Select(x => new Capacity() { CustomerId = proposal.CustomerId, CapacityMeasurement = x, Value = 10 }).ToList();

        years = [
            new() {Id = 1, CorporateCustomerId = proposal.CustomerId, Deleted = false ,ToDate = DateTime.Now},
            new() {Id = 2, CorporateCustomerId = proposal.CustomerId, Deleted = false ,ToDate = DateTime.Now.AddYears(2) },
            new() {Id = 3, CorporateCustomerId = proposal.CustomerId, Deleted = false ,ToDate = DateTime.Now.AddYears(4) },
            ];

        CompanyFinancialInfo model = new()
        {
            Code = "",
            Indx = 1,
            Sign = ECompanyFinancialInfo_sign.one,
            SubType = ECompanyFinancialInfo_subType.calculable,
            ParentId = 1,
            Type = ECompanyFinancialInfo_type.income_statement,
            FinancialInfoItems = [
                new(){
                    CorporateCustomerId = proposal.CustomerId,
                    Title = "",
                    Value = 10,
                    FinancialInfo = new(){
                        FinancialYearInfoId = 1,
                    }
                },
                new(){
                    CorporateCustomerId = proposal.CustomerId,
                    Title = "",
                    Value = 10,
                    FinancialInfo = new(){
                        FinancialYearInfoId = 2,
                    }
                },
                new(){
                    CorporateCustomerId = proposal.CustomerId,
                    Title = "",
                    Value = 10,
                    FinancialInfo = new(){
                        FinancialYearInfoId = 3,
                    }
                }
            ],
        };

        var financialCodes = new List<string> { "saleincome", "profit", "amountofimportofforeignrawmaterials", "totalpurchaseamount", };
        int counter = 1;
        foreach (var code in financialCodes)
        {
            model.Id = counter;
            model.Code = code;
            model.FinancialInfoItems = model.FinancialInfoItems.Select(x => { x.FinancialInfoDefId = counter; return x; });
            companyFinancialInfos.Add(model);
            counter++;
        }

        var calcFinancialRow = new CalcFinancialRow()
        {
            Indx = 1,
            RowItems = [new() { YearId = 1, Value = 10 }, new() { YearId = 2, Value = 10 }, new() { YearId = 2, Value = 10 }]
        };

        var calcFinancialCodes = new List<string> { "" };

        calcFinancial.Rows = calcFinancialCodes.Select(x => { calcFinancialRow.Code = x; return calcFinancialRow; }).ToList();
    }

    [Fact]
    public async Task ProposalNotFound()
    {
        context.Setup(x => x.Proposals).ReturnsDbSet([]);

        var res = await Handle();

        Assert.False(res.IsSuccess);
        Assert.Equal(404, res.Code);
    }

    [Fact]
    public async Task CustomerDontHave2Year()
    {
        context.Setup(x => x.Proposals).ReturnsDbSet([proposal]);
        context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([]);

        var res = await Handle();

        Assert.False(res.IsSuccess);
        Assert.Equal(404, res.Code);
    }

    [Fact]
    public async Task CapacityNotFound()
    {
        context.Setup(x => x.Proposals).ReturnsDbSet([proposal]);
        context.Setup(x => x.FinancialYearInfos).ReturnsDbSet(years);
        context.Setup(x => x.Capacities).ReturnsDbSet([]);


        var res = await Handle();

        Assert.False(res.IsSuccess);
        Assert.Equal(406, res.Code);
    }

    [Fact]
    public async Task CreditsNotvalid()
    {
        context.Setup(x => x.Proposals).ReturnsDbSet([proposal]);
        context.Setup(x => x.FinancialYearInfos).ReturnsDbSet(years);
        context.Setup(x => x.Capacities).ReturnsDbSet(Capacities);
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet(companyFinancialInfos);

        mediator.Setup(x => x.Send(It.IsAny<GetCalcFinancialRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResult<SearchCalcFinancialVM>() { Data = calcFinancial });

        context.Setup(x => x.CreditSubTypes).ReturnsDbSet([]);


        var res = await Handle();

        Assert.False(res.IsSuccess);
        Assert.Equal(406, res.Code);
    }

    [Theory]
    [InlineData(ECorporateCustomer_corporateType.Manufacturing)]
    [InlineData(ECorporateCustomer_corporateType.Brokerage)]
    public async Task CalculateIsManufactoring(ECorporateCustomer_corporateType corporateType)
    {
        context.Setup(x => x.Proposals).ReturnsDbSet([proposal]);
        context.Setup(x => x.FinancialYearInfos).ReturnsDbSet(years);
        context.Setup(x => x.Capacities).ReturnsDbSet(Capacities);
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet(companyFinancialInfos);

        mediator.Setup(x => x.Send(It.IsAny<GetCalcFinancialRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResult<SearchCalcFinancialVM>() { Data = calcFinancial });

        context.Setup(x => x.CreditSubTypes).ReturnsDbSet(creditSubTypes);

        context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = proposal.CustomerId, CorporateType =  corporateType}]);

        context.Setup(x => x.Currencies).ReturnsDbSet([new() { Code = "mr", Id = 1 }]);

        context.Setup(x => x.ProposalCreditItems).ReturnsDbSet([]);

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var res = await Handle();

        Assert.True(res.IsSuccess);
    }


    private async Task<ApiResult> Handle()
        => await handler.Handle(request, CancellationToken.None);
}
