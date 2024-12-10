using System.Net;
using Application.Services.InqueryData;
using Core.Enums;
using Core.GenericResultModel;
using Core.Logger;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using RestEase;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.InqueryData;

public class GetInqueriesRequestTest
{
    private readonly Mock<IExternalServices> mockExternalServices = new();
    private readonly Mock<ILoggerManager> mockLoggerManager = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();
    private readonly GetInqueriesRequest request;

    public GetInqueriesRequestTest()
    {
        mockLoggerManager.Setup(x => x.LogInfo(It.IsAny<string>()));

        request = new GetInqueriesRequest { ProposalSchemeId = 1 };
    }

    // ------------------------- GetInqueriesRequest -------------------------

    [Theory]
    [InlineData(EInqueryType.ValidationOfIranians)]
    public async Task GetInqueriesRequest_Line46(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.MaxValue }]);

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.ValidationOfIranians)]
    public async Task GetInqueriesRequest_Line52(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);

        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 2 }]);

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.ValidationOfIranians)]
    public async Task GetInqueriesRequest_Line60(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);

        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);

        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 2 }]);

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData((EInqueryType)999)]
    public async Task GetInqueriesRequest_Line65(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);

        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);

        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, IndividualCustomer = new() { NationalId = "1" } }]);

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    // ------------------------- IranianGetReport -------------------------

    [Theory]
    [InlineData(EInqueryType.ValidationOfIranians)]
    public async Task IranianGetReport_Line24(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        mockExternalServices.Setup(x => x.IranianGetReport(It.IsAny<GetIranianReportRequest>())).ReturnsAsync
            (new Response<ApiResult<GetIranianReportResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new()));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.ValidationOfIranians)]
    public async Task IranianGetReport_Line28_Add_Line62(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([new() { InqueryType = inqueryType }]);
        moq.Context.Setup(x => x.InqueryDatas.AddAsync(It.IsAny<Core.Entities.InqueryData>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        mockExternalServices.Setup(x => x.IranianGetReport(It.IsAny<GetIranianReportRequest>())).ReturnsAsync
            (new Response<ApiResult<GetIranianReportResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new() { Data = new() { RiskScore = "1" }, Message = "a", MessageEn = "a" }));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    // ------------------------- CbAccountTurnover -------------------------

    [Theory]
    [InlineData(EInqueryType.DepositOfThreeMonthsCurrentAccountAtTheBranch)]
    [InlineData(EInqueryType.DepositOfSixMonthsCurrentAccountAtTheBranch)]
    public async Task CbAccountTurnover_Line20_60(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = null }]);

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.DepositOfThreeMonthsCurrentAccountAtTheBranch)]
    [InlineData(EInqueryType.DepositOfSixMonthsCurrentAccountAtTheBranch)]
    public async Task CbAccountTurnover_Line29_69(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        mockExternalServices.Setup(x => x.CbAccountTurnover(It.IsAny<CbAccountTurnoversRequest>())).ReturnsAsync
            (new Response<ApiResult<List<CbAccountTurnoverResponse>>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new()));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.DepositOfThreeMonthsCurrentAccountAtTheBranch)]
    [InlineData(EInqueryType.DepositOfSixMonthsCurrentAccountAtTheBranch)]
    [InlineData(EInqueryType.Individual_DepositOfSixMonthsCurrentAccountAtTheBranch)]
    [InlineData(EInqueryType.CreditorLastTransactions)]
    [InlineData(EInqueryType.LastBalanceScoreInParsian)]
    public async Task CbAccountTurnover_Line48_80_81_82_83_Add_Line65(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([
            new() { InqueryType = EInqueryType.DepositOfThreeMonthsCurrentAccountAtTheBranch},
            new() { InqueryType = EInqueryType.DepositOfSixMonthsCurrentAccountAtTheBranch },
            new() { InqueryType = EInqueryType.Individual_DepositOfSixMonthsCurrentAccountAtTheBranch },
            new() { InqueryType = EInqueryType.CreditorLastTransactions },
            new() { InqueryType = EInqueryType.LastBalanceScoreInParsian }]);
        moq.Context.Setup(x => x.InqueryDatas.AddAsync(It.IsAny<Core.Entities.InqueryData>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockExternalServices.Setup(x => x.CbAccountTurnover(It.IsAny<CbAccountTurnoversRequest>())).ReturnsAsync
            (new Response<ApiResult<List<CbAccountTurnoverResponse>>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new() { Data = [new() { AccountType = "201" }, new() { AccountType = "407" }], Message = "a", MessageEn = "a" }));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    // ------------------------- GetSamatLoansInfo -------------------------

    [Theory]
    [InlineData(EInqueryType.ShortTermDebtBalanceOfBankContractsOtherThanDebtPurchaseWithTheBankingSystem)]
    [InlineData(EInqueryType.DebtBalanceOfDebtPurchaseFacilityWithTheBankingSystem)]
    [InlineData(EInqueryType.TotalShortTermDebtBalanceWithTheBankingSystem)]
    public async Task GetSamatLoansInfo_Line27_44_60(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        mockExternalServices.Setup(x => x.GetSamatLoansInfo(It.IsAny<GetSamatLoansInfoRequest>())).ReturnsAsync
            (new Response<ApiResult<GetSamatLoansInfoResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new()));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.ShortTermDebtBalanceOfBankContractsOtherThanDebtPurchaseWithTheBankingSystem)]
    [InlineData(EInqueryType.DebtBalanceOfDebtPurchaseFacilityWithTheBankingSystem)]
    [InlineData(EInqueryType.TotalShortTermDebtBalanceWithTheBankingSystem)]
    [InlineData(EInqueryType.Individual_ShortTermDebtBalanceOfBankContractsWithTheBankingSystem)]
    [InlineData(EInqueryType.DebtBalanceOfLongTermFacilitiesWithTheBankingSystem)]
    [InlineData(EInqueryType.Individual_DebtBalanceOfLongTermFacilitiesWithTheBankingSystem)]
    public async Task GetSamatLoansInfo_Line31_48_91_92_123_124(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([
            new() { InqueryType = EInqueryType.ShortTermDebtBalanceOfBankContractsOtherThanDebtPurchaseWithTheBankingSystem },
            new() { InqueryType = EInqueryType.DebtBalanceOfDebtPurchaseFacilityWithTheBankingSystem },
            new() { InqueryType = EInqueryType.TotalShortTermDebtBalanceWithTheBankingSystem },
            new() { InqueryType =  EInqueryType.Individual_ShortTermDebtBalanceOfBankContractsWithTheBankingSystem },
            new() { InqueryType =  EInqueryType.DebtBalanceOfLongTermFacilitiesWithTheBankingSystem },
            new() { InqueryType = EInqueryType.Individual_DebtBalanceOfLongTermFacilitiesWithTheBankingSystem }]);
        moq.Context.Setup(x => x.InqueryDatas.AddAsync(It.IsAny<Core.Entities.InqueryData>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockExternalServices.Setup(x => x.GetSamatLoansInfo(It.IsAny<GetSamatLoansInfoRequest>())).ReturnsAsync
            (new Response<ApiResult<GetSamatLoansInfoResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new()
            {
                Data = new()
                {
                    ReturnValue = new()
                    { EstelamAsliRows = [new() { Date = "1403-01-01", DateSarResid = "1403-02-02" }] }
                },
                Message = "a",
                MessageEn = "a"
            }));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    // ------------------------- GetSamatGuaranteesInfo -------------------------

    [Theory]
    [InlineData(EInqueryType.BalanceOfGamPapersWithTheBankingSystem)]
    [InlineData(EInqueryType.BalanceOfThePaymentObligationGuaranteeWithTheBankingSystem)]
    [InlineData(EInqueryType.BalanceOfTheCustomsGuaranteeWithTheBankingSystem)]
    [InlineData(EInqueryType.TheBalanceOfOtherGuaranteesWithTheBankingSystem)]
    public async Task GetSamatGuaranteesInfo_Line24_40_56_72(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        mockExternalServices.Setup(x => x.GetSamatGuaranteesInfo(It.IsAny<GetSamatGuaranteesInfoRequest>())).ReturnsAsync
            (new Response<ApiResult<GetSamatLoansInfoResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new()));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.BalanceOfGamPapersWithTheBankingSystem)]
    [InlineData(EInqueryType.BalanceOfThePaymentObligationGuaranteeWithTheBankingSystem)]
    [InlineData(EInqueryType.BalanceOfTheCustomsGuaranteeWithTheBankingSystem)]
    [InlineData(EInqueryType.TheBalanceOfOtherGuaranteesWithTheBankingSystem)]
    public async Task GetSamatGuaranteesInfo_Line28_44_60_76(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([
            new() { InqueryType = EInqueryType.BalanceOfGamPapersWithTheBankingSystem },
            new() { InqueryType = EInqueryType.BalanceOfThePaymentObligationGuaranteeWithTheBankingSystem },
            new() { InqueryType =  EInqueryType.BalanceOfTheCustomsGuaranteeWithTheBankingSystem },
            new() { InqueryType = EInqueryType.TheBalanceOfOtherGuaranteesWithTheBankingSystem }]);
        moq.Context.Setup(x => x.InqueryDatas.AddAsync(It.IsAny<Core.Entities.InqueryData>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockExternalServices.Setup(x => x.GetSamatGuaranteesInfo(It.IsAny<GetSamatGuaranteesInfoRequest>())).ReturnsAsync
            (new Response<ApiResult<GetSamatLoansInfoResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new() { Data = new() { ReturnValue = new() { MandeBedehiKol = "1" } }, Message = "a", MessageEn = "a" }));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    // ------------------------- GetSamatDebtorLoansInClaim -------------------------

    [Theory]
    [InlineData(EInqueryType.BalanceOfNonCurrentFacilitiesWithTheBankingSystem)]
    public async Task GetSamatDebtorLoansInClaim_Line24(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        mockExternalServices.Setup(x => x.GetSamatDebtorLoansInClaim(It.IsAny<GetSamatDebtorLoansInClaimRequest>())).ReturnsAsync
            (new Response<ApiResult<double?>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK }, () => null));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.BalanceOfNonCurrentFacilitiesWithTheBankingSystem)]
    [InlineData(EInqueryType.Individual_BalanceOfNonCurrentFacilitiesWithTheBankingSystem)]
    public async Task GetSamatDebtorLoansInClaim_Line33_34(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([
            new() { InqueryType = EInqueryType.BalanceOfNonCurrentFacilitiesWithTheBankingSystem },
            new() { InqueryType = EInqueryType.Individual_BalanceOfNonCurrentFacilitiesWithTheBankingSystem }]);
        moq.Context.Setup(x => x.InqueryDatas.AddAsync(It.IsAny<Core.Entities.InqueryData>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockExternalServices.Setup(x => x.GetSamatDebtorLoansInClaim(It.IsAny<GetSamatDebtorLoansInClaimRequest>())).ReturnsAsync
            (new Response<ApiResult<double?>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new() { Data = 1, Message = "a", MessageEn = "a" }));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    // ------------------------- CbLc -------------------------

    [Theory]
    [InlineData(EInqueryType.UzanceBalanceWithTheBankingSystem)]
    public async Task CbLc_Line18(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = null }]);

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.UzanceBalanceWithTheBankingSystem)]
    public async Task CbLc_Line29(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        mockExternalServices.Setup(x => x.CbLc(It.IsAny<CbLcRequest>())).ReturnsAsync
            (new Response<ApiResult<List<CbLcResponse>>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK }, () => new()));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.UzanceBalanceWithTheBankingSystem)]
    [InlineData(EInqueryType.OpeningBalanceOfAVisualLetterOfCreditWithTheBankingSystem)]
    public async Task CbLc_Line51_52(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([
            new() { InqueryType = EInqueryType.UzanceBalanceWithTheBankingSystem },
            new() { InqueryType = EInqueryType.OpeningBalanceOfAVisualLetterOfCreditWithTheBankingSystem }]);
        moq.Context.Setup(x => x.InqueryDatas.AddAsync(It.IsAny<Core.Entities.InqueryData>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockExternalServices.Setup(x => x.CbLc(It.IsAny<CbLcRequest>())).ReturnsAsync
            (new Response<ApiResult<List<CbLcResponse>>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new()
            {
                Data = [new() { LcSettleType = new() { Code = "122" } }, new() { LcSettleType = new() { Code = "121" } }],
                Message = "a",
                MessageEn = "a"
            }));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    // ------------------------- EstelamCheque -------------------------

    [Theory]
    [InlineData(EInqueryType.ReturnedCheckBalance)]
    public async Task EstelamCheque_Line25(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        mockExternalServices.Setup(x => x.EstelamCheque(It.IsAny<EstelamChequeRequest>())).ReturnsAsync
            (new Response<ApiResult<EstelamChequeResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK }, () => new()));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.ReturnedCheckBalance)]
    [InlineData(EInqueryType.NumberOfBouncedChecks)]
    [InlineData(EInqueryType.ReturningChequeAmountInParsian)]
    [InlineData(EInqueryType.ReturningChequeAmountOtherBanks)]
    public async Task EstelamCheque_Line53_54_55_56(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([
            new() { InqueryType = EInqueryType.ReturnedCheckBalance },
            new() { InqueryType = EInqueryType.NumberOfBouncedChecks },
            new() { InqueryType = EInqueryType.ReturningChequeAmountInParsian },
            new() { InqueryType = EInqueryType.ReturningChequeAmountOtherBanks }]);
        moq.Context.Setup(x => x.InqueryDatas.AddAsync(It.IsAny<Core.Entities.InqueryData>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockExternalServices.Setup(x => x.EstelamCheque(It.IsAny<EstelamChequeRequest>())).ReturnsAsync
            (new Response<ApiResult<EstelamChequeResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new()
            {
                Data = new() { BouncedCheques = [new() { BankCode = 54 }, new() { BankCode = 0 }] },
                Message = "a",
                MessageEn = "a"
            }));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    // ------------------------- GetCustomerDebitInTwoLastYear -------------------------

    [Theory]
    [InlineData(EInqueryType.NonCurrentCustomerDebts)]
    public async Task GetCustomerDebitInTwoLastYear_Line24(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        mockExternalServices.Setup(x => x.GetCustomerDebitInTwoLastYear(It.IsAny<GetCustomerDebitInTwoLastYearRequest>())).ReturnsAsync
            (new Response<ApiResult<GetCustomerDebitInTwoLastYearResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK }, () => new()));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.NonCurrentCustomerDebts)]
    public async Task GetCustomerDebitInTwoLastYear_Line28(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.InqueryDatas).ReturnsDbSet
            ([new() { ProposalSchemeId = 1, InqueryType = inqueryType, ExpireDate = DateTime.Now }]);
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet
            ([new() { Id = 1, CustomerId = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet
            ([new() { Id = 1, CorporateCustomer = new() { CorpId = "1" }, ClientNo = "1" }]);

        moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([new() { InqueryType = inqueryType }]);
        moq.Context.Setup(x => x.InqueryDatas.AddAsync(It.IsAny<Core.Entities.InqueryData>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockExternalServices.Setup(x => x.GetCustomerDebitInTwoLastYear(It.IsAny<GetCustomerDebitInTwoLastYearRequest>())).ReturnsAsync
            (new Response<ApiResult<GetCustomerDebitInTwoLastYearResponse>>("a", new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            () => new()
            {
                Data = new() { PrincipalDebit = 100 },
                Message = "a",
                MessageEn = "a"
            }));

        var handler = new GetInqueriesRequestHandler(moq.Context.Object, mockExternalServices.Object, mockLoggerManager.Object);

        request.InqueryType = inqueryType;

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }
}
